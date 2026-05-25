using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;

namespace Trailer_Rental_Manager.Operations
{
    /// <summary>
    /// Central SQLite access helper. It creates the local database file and schema on first use.
    /// </summary>
    internal static class DBOperations
    {
        private const string DatabaseDateFormat = "yyyy-MM-dd";
        private const string GermanDateFormat = "dd.MM.yyyy";

        private static readonly object DatabaseInitializationLock = new object();
        private static bool databaseInitialized;
        private static string connectionStringOverride;

        /// <summary>
        /// Returns a SQLite connection after ensuring the configured local database file and schema exist.
        /// </summary>
        internal static SQLiteConnection CreateConnection()
        {
            EnsureDatabaseExists();
            return new SQLiteConnection(LoadConnectionString());
        }

        /// <summary>
        /// Temporarily points database operations at a different SQLite connection string.
        /// This is used by integration tests to create isolated database files without touching the local app database.
        /// </summary>
        internal static IDisposable UseConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string must not be empty.", nameof(connectionString));
            }

            lock (DatabaseInitializationLock)
            {
                string previousConnectionStringOverride = connectionStringOverride;
                bool previousDatabaseInitialized = databaseInitialized;

                connectionStringOverride = connectionString;
                databaseInitialized = false;

                return new ConnectionStringScope(previousConnectionStringOverride, previousDatabaseInitialized);
            }
        }

        internal static DataTable ExecuteDataTable(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    AddParameters(command, parameters);

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        internal static object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    AddParameters(command, parameters);
                    return command.ExecuteScalar();
                }
            }
        }

        internal static int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    AddParameters(command, parameters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        internal static int ExecuteNonQuery(
            SQLiteConnection connection,
            SQLiteTransaction transaction,
            string sql,
            params SQLiteParameter[] parameters)
        {
            using (SQLiteCommand command = new SQLiteCommand(sql, connection, transaction))
            {
                AddParameters(command, parameters);
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Runs a group of database writes atomically so failed imports cannot leave partially inserted rows behind.
        /// </summary>
        internal static void ExecuteInTransaction(Action<SQLiteConnection, SQLiteTransaction> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            using (SQLiteConnection connection = CreateConnection())
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        operation(connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        internal static string ToDatabaseDate(DateTime date)
        {
            return date.ToString(DatabaseDateFormat, CultureInfo.InvariantCulture);
        }

        internal static DateTime FromDatabaseDate(string databaseDate)
        {
            return DateTime.ParseExact(databaseDate, DatabaseDateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Validates the exact ISO date format stored in SQLite for day-based rental periods.
        /// </summary>
        internal static bool TryFromDatabaseDate(string databaseDate, out DateTime parsedDate)
        {
            return DateTime.TryParseExact(
                databaseDate,
                DatabaseDateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out parsedDate);
        }

        /// <summary>
        /// Converts stored ISO dates for German UI display without letting corrupted legacy values crash a form.
        /// </summary>
        internal static string ToGermanDate(string databaseDate)
        {
            if (string.IsNullOrWhiteSpace(databaseDate))
            {
                return string.Empty;
            }

            DateTime parsedDate;
            if (!TryFromDatabaseDate(databaseDate, out parsedDate))
            {
                return "Ungültiges Datum: " + databaseDate;
            }

            return parsedDate.ToString(GermanDateFormat, CultureInfo.GetCultureInfo("de-DE"));
        }

        internal static object ToDbValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();
        }

        internal static object ToDbValue(decimal? value)
        {
            return value.HasValue ? (object)value.Value : DBNull.Value;
        }

        /// <summary>
        /// Parses optional numeric UI input for database parameters.
        /// Both current culture and invariant culture are accepted so comma and dot decimal input continue to work.
        /// </summary>
        internal static decimal? ParseOptionalDecimal(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            decimal result;
            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result) ||
                decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            throw new FormatException(fieldName + " must be a valid number.");
        }

        internal static decimal ParseRequiredDecimal(string value, string fieldName)
        {
            decimal? result = ParseOptionalDecimal(value, fieldName);
            if (!result.HasValue)
            {
                throw new FormatException(fieldName + " is required.");
            }

            return result.Value;
        }

        /// <summary>
        /// Applies SQLite parameters in one place so repository queries do not concatenate user input into SQL.
        /// </summary>
        private static void AddParameters(SQLiteCommand command, SQLiteParameter[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }
        }

        /// <summary>
        /// Creates the configured local SQLite database file and schema when they are missing.
        /// The schema creation script is idempotent so first start and later starts use the same path.
        /// </summary>
        private static void EnsureDatabaseExists()
        {
            if (databaseInitialized)
            {
                return;
            }

            lock (DatabaseInitializationLock)
            {
                if (databaseInitialized)
                {
                    return;
                }

                string connectionString = LoadConnectionString();
                SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder(connectionString);
                string databasePath = connectionStringBuilder.DataSource;

                if (string.IsNullOrWhiteSpace(databasePath))
                {
                    throw new ConfigurationErrorsException("The database path is missing in App.config.");
                }

                string fullDatabasePath = Path.GetFullPath(databasePath);
                string databaseDirectory = Path.GetDirectoryName(fullDatabasePath);

                if (!string.IsNullOrEmpty(databaseDirectory) && !Directory.Exists(databaseDirectory))
                {
                    Directory.CreateDirectory(databaseDirectory);
                }

                if (!File.Exists(fullDatabasePath))
                {
                    SQLiteConnection.CreateFile(fullDatabasePath);
                }

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(GetSchemaCreationScript(), connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                databaseInitialized = true;
            }
        }

        private static string GetSchemaCreationScript()
        {
            return @"
PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Customer (
    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
    Gender TEXT NOT NULL CHECK (Gender IN ('w', 'm', 'd')),
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    PostalCode TEXT,
    Street TEXT,
    HouseNumber TEXT,
    City TEXT,
    IdDocumentNumber TEXT,
    DrivingLicenseIssueDate TEXT,
    DrivingLicenseClass TEXT
);

CREATE TABLE IF NOT EXISTS Trailer (
    TrailerId INTEGER PRIMARY KEY AUTOINCREMENT,
    TrailerName TEXT NOT NULL,
    TrailerType TEXT NOT NULL,
    MaxPayload NUMERIC,
    Height NUMERIC,
    Width NUMERIC,
    Length NUMERIC
);

CREATE TABLE IF NOT EXISTS Garage (
    GarageId INTEGER PRIMARY KEY AUTOINCREMENT,
    Street TEXT NOT NULL,
    HouseNumber TEXT NOT NULL,
    PostalCode TEXT NOT NULL,
    City TEXT NOT NULL,
    MonthlyRent NUMERIC NOT NULL CHECK (MonthlyRent >= 0)
);

CREATE TABLE IF NOT EXISTS RentalOrder (
    RentalOrderId INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerId INTEGER NOT NULL,
    TrailerId INTEGER NOT NULL,
    StartDate TEXT NOT NULL CHECK (
        StartDate GLOB '[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'
        AND date(StartDate) = StartDate
    ),
    EndDate TEXT NOT NULL CHECK (
        EndDate GLOB '[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'
        AND date(EndDate) = EndDate
    ),
    Price NUMERIC NOT NULL CHECK (Price >= 0),
    CHECK (EndDate >= StartDate),
    FOREIGN KEY (TrailerId) REFERENCES Trailer(TrailerId) ON DELETE CASCADE,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId) ON DELETE CASCADE
);";
        }

        private static string LoadConnectionString(string id = "Default")
        {
            if (!string.IsNullOrWhiteSpace(connectionStringOverride))
            {
                return connectionStringOverride;
            }

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[id];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException("Connection string '" + id + "' is missing in App.config.");
            }

            return connectionString.ConnectionString;
        }

        private sealed class ConnectionStringScope : IDisposable
        {
            private readonly string previousConnectionStringOverride;
            private readonly bool previousDatabaseInitialized;
            private bool disposed;

            internal ConnectionStringScope(string previousConnectionStringOverride, bool previousDatabaseInitialized)
            {
                this.previousConnectionStringOverride = previousConnectionStringOverride;
                this.previousDatabaseInitialized = previousDatabaseInitialized;
            }

            public void Dispose()
            {
                if (disposed)
                {
                    return;
                }

                lock (DatabaseInitializationLock)
                {
                    connectionStringOverride = previousConnectionStringOverride;
                    databaseInitialized = previousDatabaseInitialized;
                    disposed = true;
                }
            }
        }
    }
}
