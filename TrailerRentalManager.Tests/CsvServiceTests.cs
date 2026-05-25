using System;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class CsvServiceTests
    {
        [TestMethod]
        public void ExportToCsv_EscapesSemicolon()
        {
            string filePath = CreateTempCsvPath();

            try
            {
                CsvService.ExportToCsv(CreateSingleValueTable("A;B"), filePath);

                string csv = File.ReadAllText(filePath, Encoding.UTF8);
                StringAssert.Contains(csv, "\"A;B\"");
            }
            finally
            {
                DeleteFile(filePath);
            }
        }

        [TestMethod]
        public void ExportToCsv_EscapesQuotes()
        {
            string filePath = CreateTempCsvPath();

            try
            {
                CsvService.ExportToCsv(CreateSingleValueTable("A \"B\""), filePath);

                string csv = File.ReadAllText(filePath, Encoding.UTF8);
                StringAssert.Contains(csv, "\"A \"\"B\"\"\"");
            }
            finally
            {
                DeleteFile(filePath);
            }
        }

        [TestMethod]
        public void ExportToCsv_EscapesLineBreaks()
        {
            string filePath = CreateTempCsvPath();

            try
            {
                CsvService.ExportToCsv(CreateSingleValueTable("A\r\nB"), filePath);

                string csv = File.ReadAllText(filePath, Encoding.UTF8);
                StringAssert.Contains(csv, "\"A\r\nB\"");
            }
            finally
            {
                DeleteFile(filePath);
            }
        }

        [TestMethod]
        public void ExportToCsv_HandlesNullValues()
        {
            string filePath = CreateTempCsvPath();
            DataTable table = new DataTable();
            table.Columns.Add("Value");
            table.Rows.Add(DBNull.Value);

            try
            {
                CsvService.ExportToCsv(table, filePath);

                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                Assert.AreEqual("Value", lines[0]);
                Assert.AreEqual(string.Empty, lines[1]);
            }
            finally
            {
                DeleteFile(filePath);
            }
        }

        [TestMethod]
        public void ImportFromCsv_EmptyFile_ReturnsEmptyTable()
        {
            string filePath = CreateTempCsvPath();

            try
            {
                File.WriteAllText(filePath, string.Empty, Encoding.UTF8);

                DataTable table = CsvService.ImportFromCsv(filePath);

                Assert.AreEqual(0, table.Columns.Count);
                Assert.AreEqual(0, table.Rows.Count);
            }
            finally
            {
                DeleteFile(filePath);
            }
        }

        [TestMethod]
        public void ImportFromCsv_SimpleValidFile_ReturnsRows()
        {
            string filePath = CreateTempCsvPath();

            try
            {
                File.WriteAllText(filePath, "FirstName;LastName\r\nPat;O'Connor\r\n", Encoding.UTF8);

                DataTable table = CsvService.ImportFromCsv(filePath);

                Assert.AreEqual(2, table.Columns.Count);
                Assert.AreEqual(1, table.Rows.Count);
                Assert.AreEqual("Pat", table.Rows[0]["FirstName"]);
                Assert.AreEqual("O'Connor", table.Rows[0]["LastName"]);
            }
            finally
            {
                DeleteFile(filePath);
            }
        }

        [TestMethod]
        public void ImportFromCsv_MissingTrailingField_ThrowsValidationException()
        {
            string filePath = CreateTempCsvPath();
            string directory = TestDatabase.CreateTemporaryDirectory();

            try
            {
                using (DBOperations.UseConnectionString(TestDatabase.CreateConnectionString(Path.Combine(directory, "test.db"))))
                {
                    File.WriteAllText(
                        filePath,
                        "RentalOrderId;CustomerId;TrailerId;StartDate;EndDate;Price\r\n1;1;1;2026-05-26;2026-05-26\r\n",
                        Encoding.UTF8);

                    CsvImportValidationException exception = Assert.ThrowsException<CsvImportValidationException>(
                        () => CsvService.ImportFromCsv(filePath));

                    Assert.AreEqual(2, exception.RowNumber);
                    Assert.AreEqual("Price", exception.ColumnName);
                    Assert.AreEqual(0, Convert.ToInt32(DBOperations.ExecuteScalar("SELECT COUNT(*) FROM RentalOrder;")));
                }
            }
            finally
            {
                DeleteFile(filePath);
                TestDatabase.DeleteDirectory(directory);
            }
        }

        [TestMethod]
        public void RentalOrderCsvImport_ExportedRentalOrder_Roundtrips()
        {
            string directory = TestDatabase.CreateTemporaryDirectory();
            string filePath = CreateTempCsvPath();

            try
            {
                using (DBOperations.UseConnectionString(TestDatabase.CreateConnectionString(Path.Combine(directory, "test.db"))))
                {
                    int customerId;
                    int trailerId;
                    SeedCustomerAndTrailer(out customerId, out trailerId);
                    InsertRentalOrderDirectly(customerId, trailerId, "2026-05-26", "2026-05-26", 42.50m);

                    CsvService.ExportToCsv(RentalOrderRepository.GetExport(), filePath);
                    DBOperations.ExecuteNonQuery("DELETE FROM RentalOrder;");

                    DataTable importedTable = CsvService.ImportFromCsv(filePath);
                    int importedRows = RentalOrderCsvImportService.Import(importedTable);
                    DataTable rentalOrders = RentalOrderRepository.GetExport();

                    Assert.AreEqual(1, importedRows);
                    Assert.AreEqual(1, rentalOrders.Rows.Count);
                    Assert.AreEqual(customerId, Convert.ToInt32(rentalOrders.Rows[0]["CustomerId"]));
                    Assert.AreEqual(trailerId, Convert.ToInt32(rentalOrders.Rows[0]["TrailerId"]));
                    Assert.AreEqual("2026-05-26", rentalOrders.Rows[0]["StartDate"].ToString());
                    Assert.AreEqual("2026-05-26", rentalOrders.Rows[0]["EndDate"].ToString());
                    Assert.AreEqual(42.50m, Convert.ToDecimal(rentalOrders.Rows[0]["Price"]));
                }
            }
            finally
            {
                DeleteFile(filePath);
                TestDatabase.DeleteDirectory(directory);
            }
        }

        [TestMethod]
        public void RentalOrderCsvImport_MissingPrice_DoesNotWriteRows()
        {
            RunRentalOrderImportValidationTest(
                table => table.Rows.Add(DBNull.Value, 1, 1, "2026-05-26", "2026-05-26", string.Empty),
                "Price");
        }

        [TestMethod]
        public void RentalOrderCsvImport_InvalidEndDate_DoesNotWriteRows()
        {
            RunRentalOrderImportValidationTest(
                table => table.Rows.Add(DBNull.Value, 1, 1, "2026-05-26", "4", "25"),
                "EndDate");
        }

        [TestMethod]
        public void RentalOrderCsvImport_InvalidStartDate_DoesNotWriteRows()
        {
            RunRentalOrderImportValidationTest(
                table => table.Rows.Add(DBNull.Value, 1, 1, "2026-05-2", "2026-05-26", "25"),
                "StartDate");
        }

        private static DataTable CreateSingleValueTable(string value)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Value");
            table.Rows.Add(value);
            return table;
        }

        private static DataTable CreateRentalOrderImportTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("RentalOrderId");
            table.Columns.Add("CustomerId");
            table.Columns.Add("TrailerId");
            table.Columns.Add("StartDate");
            table.Columns.Add("EndDate");
            table.Columns.Add("Price");
            return table;
        }

        private static void RunRentalOrderImportValidationTest(Action<DataTable> addInvalidRow, string expectedColumnName)
        {
            string directory = TestDatabase.CreateTemporaryDirectory();

            try
            {
                using (DBOperations.UseConnectionString(TestDatabase.CreateConnectionString(Path.Combine(directory, "test.db"))))
                {
                    int customerId;
                    int trailerId;
                    SeedCustomerAndTrailer(out customerId, out trailerId);

                    DataTable table = CreateRentalOrderImportTable();
                    table.Rows.Add(DBNull.Value, customerId, trailerId, "2026-05-25", "2026-05-25", "25");
                    addInvalidRow(table);

                    CsvImportValidationException exception = Assert.ThrowsException<CsvImportValidationException>(
                        () => RentalOrderCsvImportService.Import(table));

                    Assert.AreEqual(3, exception.RowNumber);
                    Assert.AreEqual(expectedColumnName, exception.ColumnName);
                    Assert.AreEqual(0, Convert.ToInt32(DBOperations.ExecuteScalar("SELECT COUNT(*) FROM RentalOrder;")));
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

        private static void InsertRentalOrderDirectly(int customerId, int trailerId, string startDate, string endDate, decimal price)
        {
            DBOperations.ExecuteNonQuery(
                @"INSERT INTO RentalOrder (CustomerId, TrailerId, StartDate, EndDate, Price)
                  VALUES (@CustomerId, @TrailerId, @StartDate, @EndDate, @Price);",
                new System.Data.SQLite.SQLiteParameter("@CustomerId", customerId),
                new System.Data.SQLite.SQLiteParameter("@TrailerId", trailerId),
                new System.Data.SQLite.SQLiteParameter("@StartDate", startDate),
                new System.Data.SQLite.SQLiteParameter("@EndDate", endDate),
                new System.Data.SQLite.SQLiteParameter("@Price", price));
        }

        private static string CreateTempCsvPath()
        {
            return Path.Combine(Path.GetTempPath(), "TrailerRentalManagerTests_" + Guid.NewGuid().ToString("N") + ".csv");
        }

        private static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
