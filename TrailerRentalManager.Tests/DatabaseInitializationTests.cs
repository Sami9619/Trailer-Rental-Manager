using System;
using System.IO;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class DatabaseInitializationTests
    {
        [TestMethod]
        public void CreateConnection_TemporaryDatabase_CreatesRequiredTables()
        {
            string directory = TestDatabase.CreateTemporaryDirectory();
            string databasePath = Path.Combine(directory, "test.db");

            try
            {
                using (DBOperations.UseConnectionString(TestDatabase.CreateConnectionString(databasePath)))
                {
                    object tableCount = DBOperations.ExecuteScalar(@"
                        SELECT COUNT(*)
                        FROM sqlite_master
                        WHERE type = 'table'
                          AND name IN ('Customer', 'Trailer', 'RentalOrder', 'Garage');");

                    Assert.AreEqual(4L, Convert.ToInt64(tableCount));
                }

                Assert.IsTrue(File.Exists(databasePath));
            }
            finally
            {
                TestDatabase.DeleteDirectory(directory);
            }
        }

        [TestMethod]
        public void RentalOrderSchema_InvalidEndDate_RejectsInsert()
        {
            RunWithSeededDatabase((customerId, trailerId) =>
            {
                Assert.ThrowsException<SQLiteException>(() =>
                    InsertRentalOrderDirectly(customerId, trailerId, "2026-05-26", "4"));
            });
        }

        [TestMethod]
        public void RentalOrderSchema_InvalidStartDate_RejectsInsert()
        {
            RunWithSeededDatabase((customerId, trailerId) =>
            {
                Assert.ThrowsException<SQLiteException>(() =>
                    InsertRentalOrderDirectly(customerId, trailerId, "2026-05-2", "2026-05-26"));
            });
        }

        [TestMethod]
        public void RentalOrderSchema_SameDayRental_AcceptsInsert()
        {
            RunWithSeededDatabase((customerId, trailerId) =>
            {
                InsertRentalOrderDirectly(customerId, trailerId, "2026-05-26", "2026-05-26");

                Assert.AreEqual(1, Convert.ToInt32(DBOperations.ExecuteScalar("SELECT COUNT(*) FROM RentalOrder;")));
            });
        }

        [TestMethod]
        public void ToGermanDate_InvalidDate_ReturnsFallbackText()
        {
            Assert.AreEqual("Ungültiges Datum: 4", DBOperations.ToGermanDate("4"));
        }

        private static void RunWithSeededDatabase(Action<int, int> assertion)
        {
            string directory = TestDatabase.CreateTemporaryDirectory();
            string databasePath = Path.Combine(directory, "test.db");

            try
            {
                using (DBOperations.UseConnectionString(TestDatabase.CreateConnectionString(databasePath)))
                {
                    CustomerRepository.Insert("m", "Pat", "Connor", null, null, null, null, null, null, null);
                    TrailerRepository.Insert("Trailer 1", "Box", null, null, null, null);

                    int customerId = Convert.ToInt32(DBOperations.ExecuteScalar("SELECT CustomerId FROM Customer LIMIT 1;"));
                    int trailerId = Convert.ToInt32(DBOperations.ExecuteScalar("SELECT TrailerId FROM Trailer LIMIT 1;"));

                    assertion(customerId, trailerId);
                }
            }
            finally
            {
                TestDatabase.DeleteDirectory(directory);
            }
        }

        private static void InsertRentalOrderDirectly(int customerId, int trailerId, string startDate, string endDate)
        {
            DBOperations.ExecuteNonQuery(
                @"INSERT INTO RentalOrder (CustomerId, TrailerId, StartDate, EndDate, Price)
                  VALUES (@CustomerId, @TrailerId, @StartDate, @EndDate, @Price);",
                new SQLiteParameter("@CustomerId", customerId),
                new SQLiteParameter("@TrailerId", trailerId),
                new SQLiteParameter("@StartDate", startDate),
                new SQLiteParameter("@EndDate", endDate),
                new SQLiteParameter("@Price", 25m));
        }
    }
}
