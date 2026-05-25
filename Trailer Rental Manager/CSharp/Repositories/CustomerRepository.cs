using System.Data;
using System.Data.SQLite;
using Trailer_Rental_Manager.Operations;

namespace Trailer_Rental_Manager.Repositories
{
    internal static class CustomerRepository
    {
        internal static DataTable GetOverview()
        {
            const string sql = @"
                SELECT
                    CustomerId AS 'Kundennummer',
                    FirstName AS 'Vorname',
                    LastName AS 'Nachname',
                    Gender AS 'Geschlecht',
                    PostalCode AS 'PLZ',
                    Street AS 'Straße',
                    HouseNumber AS 'Hausnummer',
                    City AS 'Ort',
                    IdDocumentNumber AS 'Ausweisnummer',
                    DrivingLicenseIssueDate AS 'Führerscheinausstellungsdatum',
                    DrivingLicenseClass AS 'Führerscheinklasse'
                FROM Customer
                ORDER BY LastName, FirstName;";

            DataTable table = DBOperations.ExecuteDataTable(sql);

            foreach (DataRow row in table.Rows)
            {
                row["Führerscheinausstellungsdatum"] = DBOperations.ToGermanDate(row["Führerscheinausstellungsdatum"].ToString());
            }

            return table;
        }


        internal static DataTable GetExport()
        {
            const string sql = @"
                SELECT
                    CustomerId,
                    FirstName,
                    LastName,
                    Gender,
                    PostalCode,
                    Street,
                    HouseNumber,
                    City,
                    IdDocumentNumber,
                    DrivingLicenseIssueDate,
                    DrivingLicenseClass
                FROM Customer
                ORDER BY CustomerId;";

            return DBOperations.ExecuteDataTable(sql);
        }

        internal static DataTable GetById(int customerId)
        {
            const string sql = "SELECT * FROM Customer WHERE CustomerId = @CustomerId;";
            return DBOperations.ExecuteDataTable(sql, new SQLiteParameter("@CustomerId", customerId));
        }

        internal static DataTable GetComboBoxData()
        {
            const string sql = @"
                SELECT
                    CustomerId,
                    CustomerId || '. ' || FirstName || ' ' || LastName AS DisplayName
                FROM Customer
                ORDER BY LastName, FirstName;";

            return DBOperations.ExecuteDataTable(sql);
        }

        internal static bool Exists(int customerId)
        {
            object result = DBOperations.ExecuteScalar(
                "SELECT COUNT(*) FROM Customer WHERE CustomerId = @CustomerId;",
                new SQLiteParameter("@CustomerId", customerId));

            return System.Convert.ToInt32(result) > 0;
        }

        internal static void Insert(
            string gender,
            string firstName,
            string lastName,
            string postalCode,
            string street,
            string houseNumber,
            string city,
            string idDocumentNumber,
            string drivingLicenseIssueDate,
            string drivingLicenseClass)
        {
            const string sql = @"
                INSERT INTO Customer
                (
                    Gender,
                    FirstName,
                    LastName,
                    PostalCode,
                    Street,
                    HouseNumber,
                    City,
                    IdDocumentNumber,
                    DrivingLicenseIssueDate,
                    DrivingLicenseClass
                )
                VALUES
                (
                    @Gender,
                    @FirstName,
                    @LastName,
                    @PostalCode,
                    @Street,
                    @HouseNumber,
                    @City,
                    @IdDocumentNumber,
                    @DrivingLicenseIssueDate,
                    @DrivingLicenseClass
                );";

            DBOperations.ExecuteNonQuery(
                sql,
                new SQLiteParameter("@Gender", gender),
                new SQLiteParameter("@FirstName", firstName),
                new SQLiteParameter("@LastName", lastName),
                new SQLiteParameter("@PostalCode", DBOperations.ToDbValue(postalCode)),
                new SQLiteParameter("@Street", DBOperations.ToDbValue(street)),
                new SQLiteParameter("@HouseNumber", DBOperations.ToDbValue(houseNumber)),
                new SQLiteParameter("@City", DBOperations.ToDbValue(city)),
                new SQLiteParameter("@IdDocumentNumber", DBOperations.ToDbValue(idDocumentNumber)),
                new SQLiteParameter("@DrivingLicenseIssueDate", DBOperations.ToDbValue(drivingLicenseIssueDate)),
                new SQLiteParameter("@DrivingLicenseClass", DBOperations.ToDbValue(drivingLicenseClass))
            );
        }

        internal static void Update(
            int customerId,
            string gender,
            string firstName,
            string lastName,
            string postalCode,
            string street,
            string houseNumber,
            string city,
            string idDocumentNumber,
            string drivingLicenseIssueDate,
            string drivingLicenseClass)
        {
            const string sql = @"
                UPDATE Customer
                SET
                    Gender = @Gender,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    PostalCode = @PostalCode,
                    Street = @Street,
                    HouseNumber = @HouseNumber,
                    City = @City,
                    IdDocumentNumber = @IdDocumentNumber,
                    DrivingLicenseIssueDate = @DrivingLicenseIssueDate,
                    DrivingLicenseClass = @DrivingLicenseClass
                WHERE CustomerId = @CustomerId;";

            DBOperations.ExecuteNonQuery(
                sql,
                new SQLiteParameter("@CustomerId", customerId),
                new SQLiteParameter("@Gender", gender),
                new SQLiteParameter("@FirstName", firstName),
                new SQLiteParameter("@LastName", lastName),
                new SQLiteParameter("@PostalCode", DBOperations.ToDbValue(postalCode)),
                new SQLiteParameter("@Street", DBOperations.ToDbValue(street)),
                new SQLiteParameter("@HouseNumber", DBOperations.ToDbValue(houseNumber)),
                new SQLiteParameter("@City", DBOperations.ToDbValue(city)),
                new SQLiteParameter("@IdDocumentNumber", DBOperations.ToDbValue(idDocumentNumber)),
                new SQLiteParameter("@DrivingLicenseIssueDate", DBOperations.ToDbValue(drivingLicenseIssueDate)),
                new SQLiteParameter("@DrivingLicenseClass", DBOperations.ToDbValue(drivingLicenseClass))
            );
        }

        internal static void Delete(int customerId)
        {
            const string sql = "DELETE FROM Customer WHERE CustomerId = @CustomerId;";
            DBOperations.ExecuteNonQuery(sql, new SQLiteParameter("@CustomerId", customerId));
        }
    }
}
