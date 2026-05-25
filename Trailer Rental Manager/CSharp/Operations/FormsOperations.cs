using System;
using System.Windows.Forms;
using Trailer_Rental_Manager.Services;

namespace Trailer_Rental_Manager.Operations
{
    internal static class FormsOperations
    {
        internal static void ClearEverythingFromDataGridView(DataGridView dataGridView)
        {
            dataGridView.DataSource = null;
            dataGridView.Columns.Clear();
        }

        internal static void AddButtonToDataGridView(DataGridView dataGridView, string headerName, string buttonText)
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = headerName,
                Text = buttonText,
                Name = buttonText,
                UseColumnTextForButtonValue = true,
                Width = 50
            };

            dataGridView.Columns.Add(buttonColumn);
        }

        internal static void ClearEverythingFromComboBox(ComboBox comboBox)
        {
            comboBox.DataSource = null;
            comboBox.Items.Clear();
            comboBox.SelectedIndex = -1;
            comboBox.Text = string.Empty;
        }

        internal static void ShowCsvImportFailed(Exception exception)
        {
            MessageBox.Show(
                BuildCsvImportFailedMessage(exception),
                "CSV-Import",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static string BuildCsvImportFailedMessage(Exception exception)
        {
            CsvImportFormatException formatException = exception as CsvImportFormatException;
            if (formatException != null)
            {
                return "CSV-Import fehlgeschlagen.\r\n" +
                       formatException.Message + "\r\n" +
                       "Es wurden keine Daten importiert.";
            }

            CsvImportValidationException validationException = exception as CsvImportValidationException;
            string detail;

            if (validationException != null)
            {
                string columnText = string.IsNullOrWhiteSpace(validationException.ColumnName)
                    ? string.Empty
                    : ", Spalte '" + validationException.ColumnName + "'";

                detail = "Zeile " + validationException.RowNumber + columnText + ": " + validationException.Reason;
            }
            else
            {
                detail = "Die Datei konnte nicht importiert werden. Bitte prüfen Sie Pflichtfelder und Werte.";
            }

            return "CSV-Import fehlgeschlagen.\r\n" +
                   detail + "\r\n" +
                   "Es wurden keine Daten importiert.";
        }
    }
}
