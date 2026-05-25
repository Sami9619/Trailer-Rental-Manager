using System.Data;
using System.Data.SQLite;
using Trailer_Rental_Manager.Operations;

namespace Trailer_Rental_Manager.Repositories
{
    internal static class GarageRepository
    {
        internal static DataTable GetOverview()
        {
            const string sql = @"
                SELECT
                    GarageId AS 'Garagennummer',
                    Street AS 'Straße',
                    HouseNumber AS 'Hausnummer',
                    PostalCode AS 'PLZ',
                    City AS 'Ort',
                    MonthlyRent AS 'Miete'
                FROM Garage
                ORDER BY City, Street;";

            return DBOperations.ExecuteDataTable(sql);
        }

        internal static DataTable GetExport()
        {
            const string sql = @"
                SELECT
                    GarageId,
                    Street,
                    HouseNumber,
                    PostalCode,
                    City,
                    MonthlyRent
                FROM Garage
                ORDER BY GarageId;";

            return DBOperations.ExecuteDataTable(sql);
        }

        internal static DataTable GetById(int garageId)
        {
            const string sql = "SELECT * FROM Garage WHERE GarageId = @GarageId;";
            return DBOperations.ExecuteDataTable(sql, new SQLiteParameter("@GarageId", garageId));
        }

        internal static void Insert(string street, string houseNumber, string postalCode, string city, string monthlyRent)
        {
            const string sql = @"
                INSERT INTO Garage
                (
                    Street,
                    HouseNumber,
                    PostalCode,
                    City,
                    MonthlyRent
                )
                VALUES
                (
                    @Street,
                    @HouseNumber,
                    @PostalCode,
                    @City,
                    @MonthlyRent
                );";

            DBOperations.ExecuteNonQuery(
                sql,
                new SQLiteParameter("@Street", street),
                new SQLiteParameter("@HouseNumber", houseNumber),
                new SQLiteParameter("@PostalCode", postalCode),
                new SQLiteParameter("@City", city),
                new SQLiteParameter("@MonthlyRent", DBOperations.ParseRequiredDecimal(monthlyRent, "Monthly rent"))
            );
        }

        internal static void Update(int garageId, string street, string houseNumber, string postalCode, string city, string monthlyRent)
        {
            const string sql = @"
                UPDATE Garage
                SET
                    Street = @Street,
                    HouseNumber = @HouseNumber,
                    PostalCode = @PostalCode,
                    City = @City,
                    MonthlyRent = @MonthlyRent
                WHERE GarageId = @GarageId;";

            DBOperations.ExecuteNonQuery(
                sql,
                new SQLiteParameter("@GarageId", garageId),
                new SQLiteParameter("@Street", street),
                new SQLiteParameter("@HouseNumber", houseNumber),
                new SQLiteParameter("@PostalCode", postalCode),
                new SQLiteParameter("@City", city),
                new SQLiteParameter("@MonthlyRent", DBOperations.ParseRequiredDecimal(monthlyRent, "Monthly rent"))
            );
        }

        internal static void Delete(int garageId)
        {
            const string sql = "DELETE FROM Garage WHERE GarageId = @GarageId;";
            DBOperations.ExecuteNonQuery(sql, new SQLiteParameter("@GarageId", garageId));
        }
    }
}
