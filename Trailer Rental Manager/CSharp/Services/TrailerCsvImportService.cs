using System;
using System.Collections.Generic;
using System.Data;
using Trailer_Rental_Manager.Repositories;

namespace Trailer_Rental_Manager.Services
{
    internal sealed class TrailerImportRecord
    {
        internal string TrailerName { get; set; }

        internal string TrailerType { get; set; }

        internal string MaxPayload { get; set; }

        internal string Height { get; set; }

        internal string Width { get; set; }

        internal string Length { get; set; }
    }

    internal static class TrailerCsvImportService
    {
        internal static readonly string[] ExpectedHeader =
        {
            "TrailerId",
            "TrailerName",
            "TrailerType",
            "MaxPayload",
            "Height",
            "Width",
            "Length"
        };

        internal static int Import(DataTable dataTable)
        {
            CsvService.ValidateHeader(dataTable, ExpectedHeader, "Anhänger");

            List<TrailerImportRecord> records = ValidateRows(dataTable);

            foreach (TrailerImportRecord record in records)
            {
                TrailerRepository.Insert(
                    record.TrailerName,
                    record.TrailerType,
                    record.MaxPayload,
                    record.Height,
                    record.Width,
                    record.Length);
            }

            return records.Count;
        }

        private static List<TrailerImportRecord> ValidateRows(DataTable dataTable)
        {
            List<TrailerImportRecord> records = new List<TrailerImportRecord>();

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                int csvRowNumber = rowIndex + 2;
                DataRow row = dataTable.Rows[rowIndex];
                string trailerName = ReadRequiredValue(row, csvRowNumber, "TrailerName", "Anhängername ist erforderlich.");
                string trailerType = ReadRequiredValue(row, csvRowNumber, "TrailerType", "Anhängertyp ist erforderlich.");
                string maxPayload = row["MaxPayload"].ToString();
                string height = row["Height"].ToString();
                string width = row["Width"].ToString();
                string length = row["Length"].ToString();

                if (!TrailerValidator.AreOptionalNumericFieldsValid(maxPayload, height, width, length))
                {
                    throw new CsvImportValidationException(csvRowNumber, string.Empty, "Numerische Anhängerfelder müssen gültige Zahlen sein.");
                }

                records.Add(new TrailerImportRecord
                {
                    TrailerName = trailerName,
                    TrailerType = trailerType,
                    MaxPayload = maxPayload,
                    Height = height,
                    Width = width,
                    Length = length
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
