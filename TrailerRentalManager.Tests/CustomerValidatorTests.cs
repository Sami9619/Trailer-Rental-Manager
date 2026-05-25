using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Services;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class CustomerValidatorTests
    {
        [TestMethod]
        public void HasRequiredNameParts_MissingFirstName_ReturnsFalse()
        {
            Assert.IsFalse(CustomerValidator.HasRequiredNameParts(string.Empty, "Connor"));
        }

        [TestMethod]
        public void HasRequiredNameParts_MissingLastName_ReturnsFalse()
        {
            Assert.IsFalse(CustomerValidator.HasRequiredNameParts("Pat", string.Empty));
        }

        [TestMethod]
        public void IsValidGender_AllowedValues_ReturnsTrue()
        {
            Assert.IsTrue(CustomerValidator.IsValidGender("m"));
            Assert.IsTrue(CustomerValidator.IsValidGender("w"));
            Assert.IsTrue(CustomerValidator.IsValidGender("d"));
        }

        [TestMethod]
        public void IsValidGender_UnexpectedValue_ReturnsFalse()
        {
            Assert.IsFalse(CustomerValidator.IsValidGender("x"));
        }

        [TestMethod]
        public void IsValid_ApostropheInName_ReturnsTrue()
        {
            Assert.IsTrue(CustomerValidator.IsValid("m", "Pat", "O'Connor"));
        }
    }
}
