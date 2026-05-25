using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Services;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class AvailabilityServiceTests
    {
        private static readonly DateTime ExistingStartDate = new DateTime(2026, 5, 26);
        private static readonly DateTime ExistingEndDate = new DateTime(2026, 5, 26);

        [TestMethod]
        public void OverlapsInclusive_FilterSurroundsSameDayRental_ReturnsTrue()
        {
            Assert.IsTrue(AvailabilityService.OverlapsInclusive(ExistingStartDate, ExistingEndDate, new DateTime(2026, 5, 25), new DateTime(2026, 5, 27)));
        }

        [TestMethod]
        public void OverlapsInclusive_FilterStartsOnRentalDate_ReturnsTrue()
        {
            Assert.IsTrue(AvailabilityService.OverlapsInclusive(ExistingStartDate, ExistingEndDate, new DateTime(2026, 5, 26), new DateTime(2026, 5, 27)));
        }

        [TestMethod]
        public void OverlapsInclusive_SameDayFilter_ReturnsTrue()
        {
            Assert.IsTrue(AvailabilityService.OverlapsInclusive(ExistingStartDate, ExistingEndDate, new DateTime(2026, 5, 26), new DateTime(2026, 5, 26)));
        }

        [TestMethod]
        public void OverlapsInclusive_FilterAfterRental_ReturnsFalse()
        {
            Assert.IsFalse(AvailabilityService.OverlapsInclusive(ExistingStartDate, ExistingEndDate, new DateTime(2026, 5, 27), new DateTime(2026, 5, 28)));
        }

        [TestMethod]
        public void OverlapsInclusive_FilterBeforeRental_ReturnsFalse()
        {
            Assert.IsFalse(AvailabilityService.OverlapsInclusive(ExistingStartDate, ExistingEndDate, new DateTime(2026, 5, 24), new DateTime(2026, 5, 25)));
        }
    }
}
