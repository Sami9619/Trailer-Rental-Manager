using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Services;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class RentalOrderValidatorTests
    {
        [TestMethod]
        public void IsDateRangeValid_SameDayRental_ReturnsTrue()
        {
            DateTime rentalDate = new DateTime(2026, 5, 26);

            Assert.IsTrue(RentalOrderValidator.IsDateRangeValid(rentalDate, rentalDate));
        }

        [TestMethod]
        public void IsDateRangeValid_EndDateBeforeStartDate_ReturnsFalse()
        {
            Assert.IsFalse(RentalOrderValidator.IsDateRangeValid(new DateTime(2026, 5, 26), new DateTime(2026, 5, 25)));
        }

        [TestMethod]
        public void HasSelection_MissingSelection_ReturnsFalse()
        {
            Assert.IsFalse(RentalOrderValidator.HasSelection(-1));
        }

        [TestMethod]
        public void IsPriceValid_NegativePrice_ReturnsFalse()
        {
            Assert.IsFalse(RentalOrderValidator.IsPriceValid("-1"));
        }

        [TestMethod]
        public void IsPriceValid_PositivePrice_ReturnsTrue()
        {
            Assert.IsTrue(RentalOrderValidator.IsPriceValid("42.50"));
        }
    }
}
