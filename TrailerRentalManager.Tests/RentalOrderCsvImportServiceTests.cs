using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class RentalOrderCsvImportServiceTests
    {
        [TestMethod]
        public void Import_ExistingRentalOrderId_AssignsNewDatabaseId()
        {
            RunWithDatabase(() =>
            {
                int customerId;
                int trailerId;
                SeedCustomerAndTrailer(out customerId, out trailerId);
                InsertRentalOrderDirectly(1, customerId, trailerId, "2026-05-25", "2026-05-26", 25m);

                DataTable table = CreateRentalOrderTable();
                table.Rows.Add(1, customerId, trailerId, "2026-05-26", "2026-05-27", "700");

                int importedRows = RentalOrderCsvImportService.Import(table);

                Assert.AreEqual(1, importedRows);
                Assert.AreEqual(2, Convert.ToInt32(DBOperations.ExecuteScalar("SELECT COUNT(*) FROM RentalOrder;")));
                Assert.AreEqual(2, Convert.ToInt32(DBOperations.ExecuteScalar("SELECT MAX(RentalOrderId) FROM RentalOrder;")));
            });
        }

        [TestMethod]
        public void Import_MissingCustomerId_FailsWithSpecificValidationError()
        {
            RunWithDatabase(() =>
            {
                TrailerRepository.Insert("Trailer 1", "Box", null, null, null, null);
                int trailerId = Convert.ToInt32(DBOperations.ExecuteScalar("SELECT TrailerId FROM Trailer LIMIT 1;"));

                DataTable table = CreateRentalOrderTable();
                table.Rows.Add(1, 999, trailerId, "2026-05-26", "2026-05-27", "700");

                CsvImportValidationException exception = Assert.ThrowsException<CsvImportValidationException>(
                    () => RentalOrderCsvImportService.Import(table));

                Assert.AreEqual(2, exception.RowNumber);
                Assert.AreEqual("CustomerId", exception.ColumnName);
                StringAssert.Contains(exception.Reason, "Kunde mit ID 999 existiert nicht.");
                Assert.AreEqual(0, Convert.ToInt32(DBOperations.ExecuteScalar("SELECT COUNT(*) FROM RentalOrder;")));
            });
        }

        [TestMethod]
        public void Import_MissingTrailerId_FailsWithSpecificValidationError()
        {
            RunWithDatabase(() =>
            {
                CustomerRepository.Insert("m", "Pat", "Connor", null, null, null, null, null, null, null);
                int customerId = Convert.ToInt32(DBOperations.ExecuteScalar("SELECT CustomerId FROM Customer LIMIT 1;"));

                DataTable table = CreateRentalOrderTable();
                table.Rows.Add(1, customerId, 999, "2026-05-26", "2026-05-27", "700");

                CsvImportValidationException exception = Assert.ThrowsException<CsvImportValidationException>(
                    () => RentalOrderCsvImportService.Import(table));

                Assert.AreEqual(2, exception.RowNumber);
                Assert.AreEqual("TrailerId", exception.ColumnName);
                StringAssert.Contains(exception.Reason, "Anhänger mit ID 999 existiert nicht.");
                Assert.AreEqual(0, Convert.ToInt32(DBOperations.ExecuteScalar("SELECT COUNT(*) FROM RentalOrder;")));
            });
        }

        [TestMethod]
        public void Import_TrailerAlreadyRented_FailsWithSpecificValidationError()
        {
            RunWithDatabase(() =>
            {
                int customerId;
                int trailerId;
                SeedCustomerAndTrailer(out customerId, out trailerId);
                InsertRentalOrderDirectly(null, customerId, trailerId, "2026-05-25", "2026-05-27", 25m);

                DataTable table = CreateRentalOrderTable();
                table.Rows.Add(1, customerId, trailerId, "2026-05-26", "2026-05-28", "700");

                CsvImportValidationException exception = Assert.ThrowsException<CsvImportValidationException>(
                    () => RentalOrderCsvImportService.Import(table));

                Assert.AreEqual(2, exception.RowNumber);
                StringAssert.Contains(exception.Reason, "bereits vermietet");
                Assert.AreEqual(1, Convert.ToInt32(DBOperations.ExecuteScalar("SELECT COUNT(*) FROM RentalOrder;")));
            });
        }

        private static DataTable CreateRentalOrderTable()
        {
            DataTable table = new DataTable();

            foreach (string header in RentalOrderCsvImportService.ExpectedHeader)
            {
                table.Columns.Add(header);
            }

            return table;
        }

        private static void RunWithDatabase(Action assertion)
        {
            string directory = TestDatabase.CreateTemporaryDirectory();
            string databasePath = Path.Combine(directory, "test.db");

            try
            {
                using (DBOperations.UseConnectionString(TestDatabase.CreateConnectionString(databasePath)))
                {
                    assertion();
                }
            }
            finally
            {
                TestDatabase.DeleteDirectory(directory);
            }
        }

        private static void SeedCustomerAndTrailer(out int customerId, out int trailerId)
        {
            CustomerRepository.Insert("m", "Pat", "Connor", null, null, null, null, null, null, null);
            TrailerRepository.Insert("Trailer 1", "Box", null, null, null, null);

            customerId = Convert.ToInt32(DBOperations.ExecuteScalar("SELECT CustomerId FROM Customer LIMIT 1;"));
            trailerId = Convert.ToInt32(DBOperations.ExecuteScalar("SELECT TrailerId FROM Trailer LIMIT 1;"));
        }

        private static void InsertRentalOrderDirectly(
            int? rentalOrderId,
            int customerId,
            int trailerId,
            string startDate,
            string endDate,
            decimal price)
        {
            string columns = rentalOrderId.HasValue
                ? "RentalOrderId, CustomerId, TrailerId, StartDate, EndDate, Price"
                : "CustomerId, TrailerId, StartDate, EndDate, Price";

            string values = rentalOrderId.HasValue
                ? "@RentalOrderId, @CustomerId, @TrailerId, @StartDate, @EndDate, @Price"
                : "@CustomerId, @TrailerId, @StartDate, @EndDate, @Price";

            DBOperations.ExecuteNonQuery(
                "INSERT INTO RentalOrder (" + columns + ") VALUES (" + values + ");",
                new SQLiteParameter("@RentalOrderId", rentalOrderId.HasValue ? (object)rentalOrderId.Value : DBNull.Value),
                new SQLiteParameter("@CustomerId", customerId),
                new SQLiteParameter("@TrailerId", trailerId),
                new SQLiteParameter("@StartDate", startDate),
                new SQLiteParameter("@EndDate", endDate),
                new SQLiteParameter("@Price", price));
        }
    }
}
