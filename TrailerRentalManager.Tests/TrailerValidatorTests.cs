using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Services;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class TrailerValidatorTests
    {
        [TestMethod]
        public void HasRequiredData_MissingTrailerName_ReturnsFalse()
        {
            Assert.IsFalse(TrailerValidator.HasRequiredData(string.Empty, "Box"));
        }

        [TestMethod]
        public void HasRequiredData_MissingTrailerType_ReturnsFalse()
        {
            Assert.IsFalse(TrailerValidator.HasRequiredData("Trailer 1", string.Empty));
        }

        [TestMethod]
        public void IsOptionalNumberValid_InvalidText_ReturnsFalse()
        {
            Assert.IsFalse(TrailerValidator.IsOptionalNumberValid("abc"));
        }

        [TestMethod]
        public void IsOptionalNumberValid_EmptyText_ReturnsTrue()
        {
            Assert.IsTrue(TrailerValidator.IsOptionalNumberValid(string.Empty));
        }

        [TestMethod]
        public void IsOptionalNumberValid_DotDecimal_ReturnsTrue()
        {
            Assert.IsTrue(TrailerValidator.IsOptionalNumberValid("12.5"));
        }
    }
}
