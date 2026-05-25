using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Services
{
    internal sealed class CsvImportValidationException : Exception
    {
        internal CsvImportValidationException(int rowNumber, string columnName, string reason)
            : base(CreateMessage(rowNumber, columnName, reason))
        {
            RowNumber = rowNumber;
            ColumnName = columnName;
            Reason = reason;
        }

        internal int RowNumber { get; }

        internal string ColumnName { get; }

        internal string Reason { get; }

        private static string CreateMessage(int rowNumber, string columnName, string reason)
        {
            string columnText = string.IsNullOrWhiteSpace(columnName)
                ? string.Empty
                : ", column '" + columnName + "'";

            return "CSV row " + rowNumber + columnText + ": " + reason;
        }
    }

    internal sealed class CsvImportFormatException : Exception
    {
        internal CsvImportFormatException(string entityDisplayName)
            : base("Die Datei hat nicht das erwartete Format für " + entityDisplayName + ".")
        {
            EntityDisplayName = entityDisplayName;
        }

        internal string EntityDisplayName { get; }
    }

    internal static class CsvService
    {
        private const char Separator = ';';
        private const char Quote = '"';

        internal static string ShowSaveDialog()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                return saveFileDialog.ShowDialog() == DialogResult.OK
                    ? saveFileDialog.FileName
                    : null;
            }
        }

        internal static string ShowOpenDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                return openFileDialog.ShowDialog() == DialogResult.OK
                    ? openFileDialog.FileName
                    : null;
            }
        }

        /// <summary>
        /// Writes a semicolon-separated UTF-8 CSV file and escapes values that contain separators, quotes, or line breaks.
        /// </summary>
        internal static void ExportToCsv(DataTable dataTable, string filePath)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException(nameof(dataTable));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            using (StreamWriter writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                string headerLine = string.Join(
                    Separator.ToString(),
                    dataTable.Columns.Cast<DataColumn>().Select(column => EscapeValue(column.ColumnName))
                );

                writer.WriteLine(headerLine);

                foreach (DataRow row in dataTable.Rows)
                {
                    string line = string.Join(
                        Separator.ToString(),
                        dataTable.Columns.Cast<DataColumn>().Select(column => EscapeValue(row[column]?.ToString()))
                    );

                    writer.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Reads a semicolon-separated UTF-8 CSV file into a DataTable.
        /// Rows must have the same column count as the header so malformed imports fail before database writes begin.
        /// </summary>
        internal static DataTable ImportFromCsv(string filePath)
        {
            DataTable dataTable = new DataTable();

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                return dataTable;
            }

            List<List<string>> records = ReadRecords(filePath);

            if (records.Count == 0)
            {
                return dataTable;
            }

            foreach (string header in records[0])
            {
                string columnName = string.IsNullOrWhiteSpace(header) ? "Column" + dataTable.Columns.Count : header;
                dataTable.Columns.Add(GetUniqueColumnName(dataTable, columnName));
            }

            for (int recordIndex = 1; recordIndex < records.Count; recordIndex++)
            {
                List<string> record = records[recordIndex];

                if (record.Count == 1 && string.IsNullOrWhiteSpace(record[0]))
                {
                    continue;
                }

                if (record.Count < dataTable.Columns.Count)
                {
                    string missingColumnName = dataTable.Columns[record.Count].ColumnName;
                    throw new CsvImportValidationException(recordIndex + 1, missingColumnName, "Feld fehlt.");
                }

                if (record.Count > dataTable.Columns.Count)
                {
                    throw new CsvImportValidationException(recordIndex + 1, string.Empty, "Zu viele Spalten.");
                }

                DataRow row = dataTable.NewRow();

                for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                {
                    row[columnIndex] = record[columnIndex];
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        internal static void ValidateHeader(DataTable dataTable, string[] expectedHeader, string entityDisplayName)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException(nameof(dataTable));
            }

            if (expectedHeader == null)
            {
                throw new ArgumentNullException(nameof(expectedHeader));
            }

            if (dataTable.Columns.Count != expectedHeader.Length)
            {
                throw new CsvImportFormatException(entityDisplayName);
            }

            for (int index = 0; index < expectedHeader.Length; index++)
            {
                if (!string.Equals(dataTable.Columns[index].ColumnName, expectedHeader[index], StringComparison.Ordinal))
                {
                    throw new CsvImportFormatException(entityDisplayName);
                }
            }
        }

        /// <summary>
        /// Escapes one CSV field using double quotes when the value contains CSV control characters.
        /// Embedded quotes are doubled according to common CSV rules.
        /// </summary>
        private static string EscapeValue(string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            bool requiresQuotes = value.Contains(Separator.ToString()) ||
                                  value.Contains(Quote.ToString()) ||
                                  value.Contains("\r") ||
                                  value.Contains("\n");

            string escapedValue = value.Replace(Quote.ToString(), new string(Quote, 2));
            return requiresQuotes ? Quote + escapedValue + Quote : escapedValue;
        }

        /// <summary>
        /// Parses CSV records while preserving semicolons and line breaks inside quoted fields.
        /// This parser intentionally covers the export format used by the application instead of adding a CSV dependency.
        /// </summary>
        private static List<List<string>> ReadRecords(string filePath)
        {
            List<List<string>> records = new List<List<string>>();
            List<string> currentRecord = new List<string>();
            StringBuilder currentField = new StringBuilder();
            bool insideQuotes = false;

            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, true))
            {
                while (reader.Peek() >= 0)
                {
                    char current = (char)reader.Read();

                    if (current == Quote)
                    {
                        if (insideQuotes && reader.Peek() == Quote)
                        {
                            reader.Read();
                            currentField.Append(Quote);
                        }
                        else
                        {
                            insideQuotes = !insideQuotes;
                        }
                    }
                    else if (current == Separator && !insideQuotes)
                    {
                        currentRecord.Add(currentField.ToString());
                        currentField.Clear();
                    }
                    else if ((current == '\n' || current == '\r') && !insideQuotes)
                    {
                        if (current == '\r' && reader.Peek() == '\n')
                        {
                            reader.Read();
                        }

                        currentRecord.Add(currentField.ToString());
                        currentField.Clear();
                        records.Add(currentRecord);
                        currentRecord = new List<string>();
                    }
                    else
                    {
                        currentField.Append(current);
                    }
                }
            }

            if (currentField.Length > 0 || currentRecord.Count > 0)
            {
                currentRecord.Add(currentField.ToString());
                records.Add(currentRecord);
            }

            return records;
        }

        private static string GetUniqueColumnName(DataTable dataTable, string requestedName)
        {
            string columnName = requestedName;
            int suffix = 1;

            while (dataTable.Columns.Contains(columnName))
            {
                columnName = requestedName + "_" + suffix;
                suffix++;
            }

            return columnName;
        }
    }
}
