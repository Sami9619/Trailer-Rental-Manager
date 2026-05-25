using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Trailer_Rental_Manager.Repositories;

namespace Trailer_Rental_Manager.Services
{
    internal sealed class GarageImportRecord
    {
        internal string Street { get; set; }

        internal string HouseNumber { get; set; }

        internal string PostalCode { get; set; }

        internal string City { get; set; }

        internal string MonthlyRent { get; set; }
    }

    internal static class GarageCsvImportService
    {
        internal static readonly string[] ExpectedHeader =
        {
            "GarageId",
            "Street",
            "HouseNumber",
            "PostalCode",
            "City",
            "MonthlyRent"
        };

        internal static int Import(DataTable dataTable)
        {
            CsvService.ValidateHeader(dataTable, ExpectedHeader, "Garagen");

            List<GarageImportRecord> records = ValidateRows(dataTable);

            foreach (GarageImportRecord record in records)
            {
                GarageRepository.Insert(
                    record.Street,
                    record.HouseNumber,
                    record.PostalCode,
                    record.City,
                    record.MonthlyRent);
            }

            return records.Count;
        }

        private static List<GarageImportRecord> ValidateRows(DataTable dataTable)
        {
            List<GarageImportRecord> records = new List<GarageImportRecord>();

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                int csvRowNumber = rowIndex + 2;
                DataRow row = dataTable.Rows[rowIndex];
                string street = ReadRequiredValue(row, csvRowNumber, "Street", "Straße ist erforderlich.");
                string houseNumber = ReadRequiredValue(row, csvRowNumber, "HouseNumber", "Hausnummer ist erforderlich.");
                string postalCode = ReadRequiredValue(row, csvRowNumber, "PostalCode", "PLZ ist erforderlich.");
                string city = ReadRequiredValue(row, csvRowNumber, "City", "Ort ist erforderlich.");
                string monthlyRent = ReadRequiredValue(row, csvRowNumber, "MonthlyRent", "Miete ist erforderlich.");

                decimal parsedMonthlyRent;
                if (!TryParseDecimal(monthlyRent, out parsedMonthlyRent) || parsedMonthlyRent < 0)
                {
                    throw new CsvImportValidationException(csvRowNumber, "MonthlyRent", "Miete muss eine gültige positive Zahl sein.");
                }

                records.Add(new GarageImportRecord
                {
                    Street = street,
                    HouseNumber = houseNumber,
                    PostalCode = postalCode,
                    City = city,
                    MonthlyRent = monthlyRent
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

        private static bool TryParseDecimal(string value, out decimal parsedValue)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out parsedValue) ||
                   decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out parsedValue);
        }
    }
}
