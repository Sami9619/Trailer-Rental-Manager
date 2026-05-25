using System;
using System.Collections.Generic;
using System.Data;
using Trailer_Rental_Manager.Repositories;

namespace Trailer_Rental_Manager.Services
{
    internal sealed class CustomerImportRecord
    {
        internal string Gender { get; set; }

        internal string FirstName { get; set; }

        internal string LastName { get; set; }

        internal string PostalCode { get; set; }

        internal string Street { get; set; }

        internal string HouseNumber { get; set; }

        internal string City { get; set; }

        internal string IdDocumentNumber { get; set; }

        internal string DrivingLicenseIssueDate { get; set; }

        internal string DrivingLicenseClass { get; set; }
    }

    internal static class CustomerCsvImportService
    {
        internal static readonly string[] ExpectedHeader =
        {
            "CustomerId",
            "FirstName",
            "LastName",
            "Gender",
            "PostalCode",
            "Street",
            "HouseNumber",
            "City",
            "IdDocumentNumber",
            "DrivingLicenseIssueDate",
            "DrivingLicenseClass"
        };

        internal static int Import(DataTable dataTable)
        {
            CsvService.ValidateHeader(dataTable, ExpectedHeader, "Kunden");

            List<CustomerImportRecord> records = ValidateRows(dataTable);

            foreach (CustomerImportRecord record in records)
            {
                CustomerRepository.Insert(
                    record.Gender,
                    record.FirstName,
                    record.LastName,
                    record.PostalCode,
                    record.Street,
                    record.HouseNumber,
                    record.City,
                    record.IdDocumentNumber,
                    record.DrivingLicenseIssueDate,
                    record.DrivingLicenseClass);
            }

            return records.Count;
        }

        private static List<CustomerImportRecord> ValidateRows(DataTable dataTable)
        {
            List<CustomerImportRecord> records = new List<CustomerImportRecord>();

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                int csvRowNumber = rowIndex + 2;
                DataRow row = dataTable.Rows[rowIndex];
                string firstName = ReadRequiredValue(row, csvRowNumber, "FirstName", "Vorname ist erforderlich.");
                string lastName = ReadRequiredValue(row, csvRowNumber, "LastName", "Nachname ist erforderlich.");
                string gender = ReadRequiredValue(row, csvRowNumber, "Gender", "Geschlecht ist erforderlich.");

                if (!CustomerValidator.IsValidGender(gender))
                {
                    throw new CsvImportValidationException(csvRowNumber, "Gender", "Geschlecht muss m, w oder d sein.");
                }

                records.Add(new CustomerImportRecord
                {
                    Gender = gender,
                    FirstName = firstName,
                    LastName = lastName,
                    PostalCode = row["PostalCode"].ToString(),
                    Street = row["Street"].ToString(),
                    HouseNumber = row["HouseNumber"].ToString(),
                    City = row["City"].ToString(),
                    IdDocumentNumber = row["IdDocumentNumber"].ToString(),
                    DrivingLicenseIssueDate = row["DrivingLicenseIssueDate"].ToString(),
                    DrivingLicenseClass = row["DrivingLicenseClass"].ToString()
                });
            }

            return records;
        }

        private static string ReadRequiredValue(DataRow row, int rowNumber, string columnName, string reason)
        {
            string value = row[columnName]?.ToString().Trim();

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new CsvImportValidationException(rowNumber, columnName, reason);
            }

            return value;
        }
    }
}
