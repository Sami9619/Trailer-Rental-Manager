using System;
using System.Drawing;
using System.Windows.Forms;
using Trailer_Rental_Manager.Repositories;
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

        /// <summary>
        /// Replaces the repetitive hide/show/close pattern used for all top-level form navigation.
        /// </summary>
        internal static void NavigateTo(Form current, Form next)
        {
            current.Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = new Point(current.DesktopLocation.X, current.DesktopLocation.Y);
            next.Closed += (s, args) => current.Close();
            next.Show();
        }

        internal static string ShowOpenCsvDialog()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "CSV Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;

                return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
            }
        }

        internal static string ShowSaveCsvDialog()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;

                return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
            }
        }

        internal static void FillCustomerComboBox(ComboBox comboBox)
        {
            ClearEverythingFromComboBox(comboBox);
            comboBox.DataSource = CustomerRepository.GetComboBoxData();
            comboBox.DisplayMember = "DisplayName";
            comboBox.ValueMember = "CustomerId";
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.SelectedIndex = -1;
        }

        internal static void FillTrailerComboBox(ComboBox comboBox)
        {
            ClearEverythingFromComboBox(comboBox);
            comboBox.DataSource = TrailerRepository.GetComboBoxData();
            comboBox.DisplayMember = "DisplayName";
            comboBox.ValueMember = "TrailerId";
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.SelectedIndex = -1;
        }

        internal static bool ValidateCustomerInput(TextBox genderTextBox, TextBox firstNameTextBox, TextBox lastNameTextBox)
        {
            if (genderTextBox.Text.Length == 0)
            {
                MessageBox.Show("Geschlecht darf nicht leer sein!");
                return false;
            }

            if (!CustomerValidator.IsValidGender(genderTextBox.Text))
            {
                MessageBox.Show("Geschlecht darf nur 'w' (Weiblich), 'm' (männlich) oder 'd' (diverse) sein!");
                return false;
            }

            if (firstNameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Vorname darf nicht leer sein!");
                return false;
            }

            if (lastNameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Nachname darf nicht leer sein!");
                return false;
            }

            return true;
        }

        internal static bool ValidateTrailerInput(
            TextBox nameTextBox,
            TextBox typeTextBox,
            TextBox maxPayloadTextBox,
            TextBox heightTextBox,
            TextBox widthTextBox,
            TextBox lengthTextBox)
        {
            if (nameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Anhängername darf nicht leer sein!");
                return false;
            }

            if (typeTextBox.Text.Length == 0)
            {
                MessageBox.Show("Typ darf nicht leer sein!");
                return false;
            }

            if (!TrailerValidator.IsOptionalNumberValid(maxPayloadTextBox.Text))
            {
                MessageBox.Show("Maximale Zuladung darf nur eine Zahl sein!");
                return false;
            }

            if (!TrailerValidator.IsOptionalNumberValid(heightTextBox.Text))
            {
                MessageBox.Show("Höhe darf nur eine Zahl sein!");
                return false;
            }

            if (!TrailerValidator.IsOptionalNumberValid(widthTextBox.Text))
            {
                MessageBox.Show("Breite darf nur eine Zahl sein!");
                return false;
            }

            if (!TrailerValidator.IsOptionalNumberValid(lengthTextBox.Text))
            {
                MessageBox.Show("Länge darf nur eine Zahl sein!");
                return false;
            }

            return true;
        }

        internal static bool ValidateGarageInput(
            TextBox streetTextBox,
            TextBox houseNumberTextBox,
            TextBox postalCodeTextBox,
            TextBox cityTextBox,
            TextBox monthlyRentTextBox)
        {
            if (streetTextBox.Text.Length == 0)
            {
                MessageBox.Show("Straße darf nicht leer sein!");
                return false;
            }

            if (houseNumberTextBox.Text.Length == 0)
            {
                MessageBox.Show("Hausnummer darf nicht leer sein!");
                return false;
            }

            if (postalCodeTextBox.Text.Length == 0)
            {
                MessageBox.Show("PLZ darf nicht leer sein!");
                return false;
            }

            if (cityTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ort darf nicht leer sein!");
                return false;
            }

            if (monthlyRentTextBox.Text.Length == 0)
            {
                MessageBox.Show("Miete darf nicht leer sein!");
                return false;
            }

            double rent;
            if (!double.TryParse(monthlyRentTextBox.Text.Trim(), out rent))
            {
                MessageBox.Show("Miete darf nur eine Zahl sein!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates rental order form input and shows the first failing message box.
        /// Pass startDateSelected/endDateSelected as false when the date pickers start blank (create mode).
        /// Pass ignoredRentalOrderId when editing an existing order so it is excluded from the conflict check.
        /// </summary>
        internal static bool ValidateRentalOrderInput(
            ComboBox customerComboBox,
            ComboBox trailerComboBox,
            DateTimePicker startPicker,
            DateTimePicker endPicker,
            TextBox priceTextBox,
            bool startDateSelected,
            bool endDateSelected,
            int? ignoredRentalOrderId = null)
        {
            if (!RentalOrderValidator.HasSelection(customerComboBox.SelectedIndex))
            {
                MessageBox.Show("Ein Kunde muss ausgewählt werden!");
                return false;
            }

            if (!RentalOrderValidator.HasSelection(trailerComboBox.SelectedIndex))
            {
                MessageBox.Show("Ein Anhänger muss ausgewählt werden!");
                return false;
            }

            if (!startDateSelected)
            {
                MessageBox.Show("Ein Beginndatum muss ausgewählt werden!");
                return false;
            }

            if (!endDateSelected)
            {
                MessageBox.Show("Ein Enddatum muss ausgewählt werden!");
                return false;
            }

            if (!RentalOrderValidator.IsDateRangeValid(startPicker.Value, endPicker.Value))
            {
                MessageBox.Show("Das Enddatum muss nach dem Beginndatum liegen!");
                return false;
            }

            int trailerId = Convert.ToInt32(trailerComboBox.SelectedValue);
            if (RentalOrderRepository.IsTrailerRentedInPeriod(trailerId, startPicker.Value, endPicker.Value, ignoredRentalOrderId))
            {
                MessageBox.Show("Der ausgewählte Anhänger ist in dieser Zeit bereits vermietet!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(priceTextBox.Text))
            {
                MessageBox.Show("Preis darf nicht leer sein!");
                return false;
            }

            decimal price;
            if (!RentalOrderValidator.TryParsePrice(priceTextBox.Text, out price))
            {
                MessageBox.Show("Preis darf nur eine Zahl sein!");
                return false;
            }

            if (price < 0)
            {
                MessageBox.Show("Preis darf nicht negativ sein!");
                return false;
            }

            return true;
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
