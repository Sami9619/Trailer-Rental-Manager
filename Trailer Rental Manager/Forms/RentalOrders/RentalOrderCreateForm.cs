using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.RentalOrders
{
    public partial class RentalOrderCreateForm : Form
    {
        private bool startDateSelected = false;
        private bool endDateSelected = false;

        public RentalOrderCreateForm()
        {
            InitializeComponent();
        }

        private void RentalOrderCreateForm_Load(object sender, EventArgs e)
        {
            FillComboBoxKunden(comboBox_Kunden);
            FillComboBoxAnhaenger(comboBox_Anhaenger);
            SetRulesForDateTimePicker(dateTimePicker_Beginn, dateTimePicker_End);
        }

        internal static void SetRulesForDateTimePicker(DateTimePicker dateTimePicker_Beginn, DateTimePicker dateTimePicker_End)
        {
            dateTimePicker_Beginn.MaxDate = new DateTime(9998, 12, 31);
            dateTimePicker_End.MaxDate = new DateTime(9998, 12, 31);
            dateTimePicker_Beginn.MinDate = DateTime.Today;
            dateTimePicker_End.MinDate = DateTime.Today;
            dateTimePicker_Beginn.Format = DateTimePickerFormat.Custom;
            dateTimePicker_End.Format = DateTimePickerFormat.Custom;
            dateTimePicker_Beginn.CustomFormat = " ";
            dateTimePicker_End.CustomFormat = " ";
        }

        internal static void FillComboBoxAnhaenger(ComboBox comboBox_Anhaenger)
        {
            FormsOperations.ClearEverythingFromComboBox(comboBox_Anhaenger);
            DataTable dataTable = TrailerRepository.GetComboBoxData();
            comboBox_Anhaenger.DataSource = dataTable;
            comboBox_Anhaenger.DisplayMember = "DisplayName";
            comboBox_Anhaenger.ValueMember = "TrailerId";
            comboBox_Anhaenger.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Anhaenger.SelectedIndex = -1;
        }

        internal static void FillComboBoxKunden(ComboBox comboBox_Kunden)
        {
            FormsOperations.ClearEverythingFromComboBox(comboBox_Kunden);
            DataTable dataTable = CustomerRepository.GetComboBoxData();
            comboBox_Kunden.DataSource = dataTable;
            comboBox_Kunden.DisplayMember = "DisplayName";
            comboBox_Kunden.ValueMember = "CustomerId";
            comboBox_Kunden.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Kunden.SelectedIndex = -1;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (IsRentalOrderInputValid(comboBox_Kunden, comboBox_Anhaenger, dateTimePicker_Beginn, dateTimePicker_End, Preis_TextBox, startDateSelected, endDateSelected))
            {
                string startDate = DBOperations.ToDatabaseDate(dateTimePicker_Beginn.Value);
                string endDate = DBOperations.ToDatabaseDate(dateTimePicker_End.Value);
                int customerId = Convert.ToInt32(comboBox_Kunden.SelectedValue);
                int trailerId = Convert.ToInt32(comboBox_Anhaenger.SelectedValue);
                RentalOrderRepository.Insert(customerId, trailerId, startDate, endDate, Preis_TextBox.Text.Trim());
                this.Close();
            }
        }

        internal static bool IsRentalOrderInputValid(ComboBox comboBox_Kunden, ComboBox comboBox_Anhaenger, DateTimePicker dateTimePicker_Beginn, DateTimePicker dateTimePicker_End, TextBox preis_TextBox, bool startDateSelected, bool endDateSelected)
        {
            if (!RentalOrderValidator.HasSelection(comboBox_Kunden.SelectedIndex))
            {
                MessageBox.Show("Ein Kunde muss ausgewählt werden!");
                return false;
            }

            if (!RentalOrderValidator.HasSelection(comboBox_Anhaenger.SelectedIndex))
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

            if (!RentalOrderValidator.IsDateRangeValid(dateTimePicker_Beginn.Value, dateTimePicker_End.Value))
            {
                MessageBox.Show("Das Enddatum muss nach dem Beginndatum liegen!");
                return false;
            }

            int trailerId = Convert.ToInt32(comboBox_Anhaenger.SelectedValue);
            if (RentalOrderRepository.IsTrailerRentedInPeriod(trailerId, dateTimePicker_Beginn.Value, dateTimePicker_End.Value))
            {
                MessageBox.Show("Der ausgewählte Anhänger ist in dieser Zeit bereits vermietet!");
                return false;
            }

            if (preis_TextBox.Text.Length == 0)
            {
                MessageBox.Show("Preis darf nicht leer sein!");
                return false;
            }

            decimal price;
            if (!RentalOrderValidator.TryParsePrice(preis_TextBox.Text, out price))
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

        private void dateTimePicker_Beginn_MouseUp(object sender, MouseEventArgs e)
        {
            startDateSelected = true;
            dateTimePicker_Beginn.Format = DateTimePickerFormat.Custom;
            dateTimePicker_Beginn.CustomFormat = "dd.MM.yyyy";

            if (dateTimePicker_End.Value < dateTimePicker_Beginn.Value)
            {
                dateTimePicker_End.Value = dateTimePicker_Beginn.Value;
            }
        }

        private void dateTimePicker_End_MouseUp(object sender, MouseEventArgs e)
        {
            endDateSelected = true;
            dateTimePicker_End.Format = DateTimePickerFormat.Custom;
            dateTimePicker_End.CustomFormat = "dd.MM.yyyy";
        }

        private void dateTimePicker_Beginn_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker_End.MinDate = dateTimePicker_Beginn.Value;
        }
    }
}
