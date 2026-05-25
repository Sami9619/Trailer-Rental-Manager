using System.Data;
using System.Data.SQLite;
using Trailer_Rental_Manager.Operations;

namespace Trailer_Rental_Manager.Repositories
{
    internal static class TrailerRepository
    {
        internal static DataTable GetOverview()
        {
            const string sql = @"
                SELECT
                    TrailerId AS 'Anhängernummer',
                    TrailerName AS 'Anhängername',
                    TrailerType AS 'Typ',
                    MaxPayload AS 'Maximale Zuladung',
                    Height AS 'Höhe',
                    Width AS 'Breite',
                    Length AS 'Länge'
                FROM Trailer
                ORDER BY TrailerName;";

            return DBOperations.ExecuteDataTable(sql);
        }

        internal static DataTable GetExport()
        {
            const string sql = @"
                SELECT
                    TrailerId,
                    TrailerName,
                    TrailerType,
                    MaxPayload,
                    Height,
                    Width,
                    Length
                FROM Trailer
                ORDER BY TrailerId;";

            return DBOperations.ExecuteDataTable(sql);
        }

        internal static DataTable GetById(int trailerId)
        {
            const string sql = "SELECT * FROM Trailer WHERE TrailerId = @TrailerId;";
            return DBOperations.ExecuteDataTable(sql, new SQLiteParameter("@TrailerId", trailerId));
        }

        internal static DataTable GetComboBoxData()
        {
            const string sql = @"
                SELECT
                    TrailerId,
                    TrailerId || '. ' || TrailerName || '; ' || TrailerType AS DisplayName
                FROM Trailer
                ORDER BY TrailerName;";

            return DBOperations.ExecuteDataTable(sql);
        }

        internal static bool Exists(int trailerId)
        {
            object result = DBOperations.ExecuteScalar(
                "SELECT COUNT(*) FROM Trailer WHERE TrailerId = @TrailerId;",
                new SQLiteParameter("@TrailerId", trailerId));

            return System.Convert.ToInt32(result) > 0;
        }

        internal static void Insert(string trailerName, string trailerType, string maxPayload, string height, string width, string length)
        {
            const string sql = @"
                INSERT INTO Trailer
                (
                    TrailerName,
                    TrailerType,
                    MaxPayload,
                    Height,
                    Width,
                    Length
                )
                VALUES
                (
                    @TrailerName,
                    @TrailerType,
                    @MaxPayload,
                    @Height,
                    @Width,
                    @Length
                );";

            DBOperations.ExecuteNonQuery(
                sql,
                new SQLiteParameter("@TrailerName", trailerName),
                new SQLiteParameter("@TrailerType", trailerType),
                new SQLiteParameter("@MaxPayload", DBOperations.ToDbValue(DBOperations.ParseOptionalDecimal(maxPayload, "Max payload"))),
                new SQLiteParameter("@Height", DBOperations.ToDbValue(DBOperations.ParseOptionalDecimal(height, "Height"))),
                new SQLiteParameter("@Width", DBOperations.ToDbValue(DBOperations.ParseOptionalDecimal(width, "Width"))),
                new SQLiteParameter("@Length", DBOperations.ToDbValue(DBOperations.ParseOptionalDecimal(length, "Length")))
            );
        }

        internal static void Update(int trailerId, string trailerName, string trailerType, string maxPayload, string height, string width, string length)
        {
            const string sql = @"
                UPDATE Trailer
                SET
                    TrailerName = @TrailerName,
                    TrailerType = @TrailerType,
                    MaxPayload = @MaxPayload,
                    Height = @Height,
                    Width = @Width,
                    Length = @Length
                WHERE TrailerId = @TrailerId;";

            DBOperations.ExecuteNonQuery(
                sql,
                new SQLiteParameter("@TrailerId", trailerId),
                new SQLiteParameter("@TrailerName", trailerName),
                new SQLiteParameter("@TrailerType", trailerType),
                new SQLiteParameter("@MaxPayload", DBOperations.ToDbValue(DBOperations.ParseOptionalDecimal(maxPayload, "Max payload"))),
                new SQLiteParameter("@Height", DBOperations.ToDbValue(DBOperations.ParseOptionalDecimal(height, "Height"))),
                new SQLiteParameter("@Width", DBOperations.ToDbValue(DBOperations.ParseOptionalDecimal(width, "Width"))),
                new SQLiteParameter("@Length", DBOperations.ToDbValue(DBOperations.ParseOptionalDecimal(length, "Length")))
            );
        }

        internal static void Delete(int trailerId)
        {
            const string sql = "DELETE FROM Trailer WHERE TrailerId = @TrailerId;";
            DBOperations.ExecuteNonQuery(sql, new SQLiteParameter("@TrailerId", trailerId));
        }
    }
}
