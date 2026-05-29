using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;

namespace Trailer_Rental_Manager.Services
{
    internal sealed class RentalOrderImportRecord
    {
        internal int CustomerId { get; set; }

        internal int TrailerId { get; set; }

        internal string StartDate { get; set; }

        internal string EndDate { get; set; }

        internal decimal Price { get; set; }
    }

    internal static class RentalOrderCsvImportService
    {
        internal static readonly string[] ExpectedHeader =
        {
            "RentalOrderId",
            "CustomerId",
            "TrailerId",
            "StartDate",
            "EndDate",
            "Price"
        };

        private const string CustomerIdColumn = "CustomerId";
        private const string TrailerIdColumn = "TrailerId";
        private const string StartDateColumn = "StartDate";
        private const string EndDateColumn = "EndDate";
        private const string PriceColumn = "Price";

        /// <summary>
        /// Validates a rental-order CSV completely before inserting rows.
        /// Rental-order CSV uses the same ISO yyyy-MM-dd dates as the database export so export/import roundtrips stay lossless.
        /// </summary>
        internal static int Import(DataTable dataTable)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException(nameof(dataTable));
            }

            CsvService.ValidateHeader(dataTable, ExpectedHeader, "Aufträge");

            List<RentalOrderImportRecord> records = Validate(dataTable);

            if (records.Count == 0)
            {
                return 0;
            }

            RentalOrderRepository.InsertMany(records);
            return records.Count;
        }

        internal static List<RentalOrderImportRecord> Validate(DataTable dataTable)
        {
            List<RentalOrderImportRecord> records = new List<RentalOrderImportRecord>();

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                int csvRowNumber = rowIndex + 2;
                DataRow row = dataTable.Rows[rowIndex];

                int customerId = ReadRequiredInteger(row, csvRowNumber, CustomerIdColumn, "Kundennummer ist erforderlich.");
                int trailerId = ReadRequiredInteger(row, csvRowNumber, TrailerIdColumn, "Anhängernummer ist erforderlich.");
                DateTime startDate = ReadRequiredDate(row, csvRowNumber, StartDateColumn);
                DateTime endDate = ReadRequiredDate(row, csvRowNumber, EndDateColumn);
                decimal price = ReadRequiredPrice(row, csvRowNumber);

                if (!CustomerRepository.Exists(customerId))
                {
                    throw new CsvImportValidationException(csvRowNumber, CustomerIdColumn, "Kunde mit ID " + customerId + " existiert nicht.");
                }

                if (!TrailerRepository.Exists(trailerId))
                {
                    throw new CsvImportValidationException(csvRowNumber, TrailerIdColumn, "Anhänger mit ID " + trailerId + " existiert nicht.");
                }

                if (!RentalOrderValidator.IsDateRangeValid(startDate, endDate))
                {
                    throw new CsvImportValidationException(csvRowNumber, EndDateColumn, "Enddatum darf nicht vor dem Beginndatum liegen.");
                }

                if (RentalOrderRepository.IsTrailerRentedInPeriod(trailerId, startDate, endDate) ||
                    HasImportConflict(records, trailerId, startDate, endDate))
                {
                    throw new CsvImportValidationException(csvRowNumber, string.Empty, "Anhänger " + trailerId + " ist im Zeitraum " + FormatDateRange(startDate, endDate) + " bereits vermietet.");
                }

                records.Add(new RentalOrderImportRecord
                {
                    CustomerId = customerId,
                    TrailerId = trailerId,
                    StartDate = DBOperations.ToDatabaseDate(startDate),
                    EndDate = DBOperations.ToDatabaseDate(endDate),
                    Price = price
                });
            }

            return records;
        }

        private static int ReadRequiredInteger(DataRow row, int rowNumber, string columnName, string missingReason)
        {
            string value = ReadRequiredValue(row, rowNumber, columnName, missingReason);
            int parsedValue;

            if (!int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedValue) || parsedValue <= 0)
            {
                throw new CsvImportValidationException(rowNumber, columnName, "Wert muss eine gültige positive Ganzzahl sein.");
            }

            return parsedValue;
        }

        private static DateTime ReadRequiredDate(DataRow row, int rowNumber, string columnName)
        {
            string value = ReadRequiredValue(row, rowNumber, columnName, "Datum ist erforderlich.");
            DateTime parsedDate;

            if (!DBOperations.TryFromDatabaseDate(value, out parsedDate))
            {
                throw new CsvImportValidationException(rowNumber, columnName, "Datum muss im Format yyyy-MM-dd angegeben sein.");
            }

            return parsedDate;
        }

        private static decimal ReadRequiredPrice(DataRow row, int rowNumber)
        {
            string value = ReadRequiredValue(row, rowNumber, PriceColumn, "Preis ist erforderlich.");
            decimal parsedPrice;

            if (!RentalOrderValidator.TryParsePrice(value, out parsedPrice))
            {
                throw new CsvImportValidationException(rowNumber, PriceColumn, "Preis muss eine gültige Zahl sein.");
            }

            if (parsedPrice < 0)
            {
                throw new CsvImportValidationException(rowNumber, PriceColumn, "Preis darf nicht negativ sein.");
            }

            return parsedPrice;
        }

        private static string ReadRequiredValue(DataRow row, int rowNumber, string columnName, string missingReason)
        {
            string value = row[columnName]?.ToString().Trim();

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new CsvImportValidationException(rowNumber, columnName, missingReason);
            }

            return value;
        }

        private static bool HasImportConflict(
            IEnumerable<RentalOrderImportRecord> records,
            int trailerId,
            DateTime startDate,
            DateTime endDate)
        {
            foreach (RentalOrderImportRecord record in records)
            {
                if (record.TrailerId != trailerId)
                {
                    continue;
                }

                DateTime existingStartDate = DBOperations.FromDatabaseDate(record.StartDate);
                DateTime existingEndDate = DBOperations.FromDatabaseDate(record.EndDate);

                if (existingStartDate.Date < endDate.Date &&
                    existingEndDate.Date > startDate.Date)
                {
                    return true;
                }
            }

            return false;
        }

        private static string FormatDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate.ToString("dd.MM.yyyy", CultureInfo.GetCultureInfo("de-DE")) +
                   " - " +
                   endDate.ToString("dd.MM.yyyy", CultureInfo.GetCultureInfo("de-DE"));
        }
    }
}
