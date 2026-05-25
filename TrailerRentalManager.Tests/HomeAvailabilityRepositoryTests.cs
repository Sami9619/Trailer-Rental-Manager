using System;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class HomeAvailabilityRepositoryTests
    {
        [TestMethod]
        public void HomeAvailability_FilterSurroundsSameDayRental_ShowsRentedOnly()
        {
            RunWithSameDayRental(() =>
            {
                DataTable rentedTrailers = RentalOrderRepository.GetRentedTrailers(new DateTime(2026, 5, 25), new DateTime(2026, 5, 27));
                DataTable availableTrailers = RentalOrderRepository.GetAvailableTrailers(new DateTime(2026, 5, 25), new DateTime(2026, 5, 27));

                Assert.AreEqual(1, rentedTrailers.Rows.Count);
                Assert.AreEqual(0, availableTrailers.Rows.Count);
            });
        }

        [TestMethod]
        public void HomeAvailability_FilterStartsOnSameDayRental_ShowsRentedOnly()
        {
            RunWithSameDayRental(() =>
            {
                DataTable rentedTrailers = RentalOrderRepository.GetRentedTrailers(new DateTime(2026, 5, 26), new DateTime(2026, 5, 27));
                DataTable availableTrailers = RentalOrderRepository.GetAvailableTrailers(new DateTime(2026, 5, 26), new DateTime(2026, 5, 27));

                Assert.AreEqual(1, rentedTrailers.Rows.Count);
                Assert.AreEqual(0, availableTrailers.Rows.Count);
            });
        }

        [TestMethod]
        public void HomeAvailability_FilterAfterSameDayRental_ShowsAvailableOnly()
        {
            RunWithSameDayRental(() =>
            {
                DataTable rentedTrailers = RentalOrderRepository.GetRentedTrailers(new DateTime(2026, 5, 27), new DateTime(2026, 5, 28));
                DataTable availableTrailers = RentalOrderRepository.GetAvailableTrailers(new DateTime(2026, 5, 27), new DateTime(2026, 5, 28));

                Assert.AreEqual(0, rentedTrailers.Rows.Count);
                Assert.AreEqual(1, availableTrailers.Rows.Count);
            });
        }

        private static void RunWithSameDayRental(Action assertion)
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

                    RentalOrderRepository.Insert(
                        customerId,
                        trailerId,
                        DBOperations.ToDatabaseDate(new DateTime(2026, 5, 26)),
                        DBOperations.ToDatabaseDate(new DateTime(2026, 5, 26)),
                        "25");

                    assertion();
                }
            }
            finally
            {
                TestDatabase.DeleteDirectory(directory);
            }
        }
    }
}
