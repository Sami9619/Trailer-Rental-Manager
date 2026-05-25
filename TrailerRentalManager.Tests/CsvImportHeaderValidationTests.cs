using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Services;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class CsvImportHeaderValidationTests
    {
        [TestMethod]
        public void CustomerImport_RentalOrderHeader_ThrowsFormatException()
        {
            CsvImportFormatException exception = Assert.ThrowsException<CsvImportFormatException>(
                () => CustomerCsvImportService.Import(CreateTable(RentalOrderCsvImportService.ExpectedHeader)));

            Assert.AreEqual("Kunden", exception.EntityDisplayName);
        }

        [TestMethod]
        public void TrailerImport_RentalOrderHeader_ThrowsFormatException()
        {
            CsvImportFormatException exception = Assert.ThrowsException<CsvImportFormatException>(
                () => TrailerCsvImportService.Import(CreateTable(RentalOrderCsvImportService.ExpectedHeader)));

            Assert.AreEqual("Anhänger", exception.EntityDisplayName);
        }

        [TestMethod]
        public void RentalOrderImport_CustomerHeader_ThrowsFormatException()
        {
            CsvImportFormatException exception = Assert.ThrowsException<CsvImportFormatException>(
                () => RentalOrderCsvImportService.Import(CreateTable(CustomerCsvImportService.ExpectedHeader)));

            Assert.AreEqual("Aufträge", exception.EntityDisplayName);
        }

        [TestMethod]
        public void GarageImport_CustomerHeader_ThrowsFormatException()
        {
            CsvImportFormatException exception = Assert.ThrowsException<CsvImportFormatException>(
                () => GarageCsvImportService.Import(CreateTable(CustomerCsvImportService.ExpectedHeader)));

            Assert.AreEqual("Garagen", exception.EntityDisplayName);
        }

        [TestMethod]
        public void WrongFormatImport_DoesNotSilentlyReturnZero()
        {
            Assert.ThrowsException<CsvImportFormatException>(
                () => CustomerCsvImportService.Import(CreateTable(RentalOrderCsvImportService.ExpectedHeader)));
        }

        [TestMethod]
        public void EmptyCsvImport_ThrowsControlledFormatException()
        {
            CsvImportFormatException exception = Assert.ThrowsException<CsvImportFormatException>(
                () => RentalOrderCsvImportService.Import(new DataTable()));

            Assert.AreEqual("Aufträge", exception.EntityDisplayName);
        }

        [TestMethod]
        public void RentalOrderImport_ExpectedHeaderWithNoRows_ReturnsZero()
        {
            int importedRows = RentalOrderCsvImportService.Import(CreateTable(RentalOrderCsvImportService.ExpectedHeader));

            Assert.AreEqual(0, importedRows);
        }

        private static DataTable CreateTable(string[] headers)
        {
            DataTable table = new DataTable();

            foreach (string header in headers)
            {
                table.Columns.Add(header);
            }

            return table;
        }
    }
}
