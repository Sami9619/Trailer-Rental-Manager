using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.RentalOrders
{
    public partial class RentalOrderEditForm : Form
    {
        private readonly int rentalOrderId;

        public RentalOrderEditForm(int rentalOrderId)
        {
            InitializeComponent();
            this.rentalOrderId = rentalOrderId;
        }

        private void RentalOrderEditForm_Load(object sender, EventArgs e)
        {
            DataTable dataTable = RentalOrderRepository.GetDetailsById(rentalOrderId);

            if (dataTable.Rows.Count > 0)
            {
                RentalOrderCreateForm.FillComboBoxKunden(comboBox_Kunden);
                RentalOrderCreateForm.FillComboBoxAnhaenger(comboBox_Anhaenger);
                SetRulesForDateTimePicker(dateTimePicker_Beginn, dateTimePicker_End);

                rentalOrderIdTextBox.Text = dataTable.Rows[0]["RentalOrderId"].ToString();
                comboBox_Kunden.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["CustomerId"]);
                comboBox_Anhaenger.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["TrailerId"]);
                dateTimePicker_Beginn.Value = DBOperations.FromDatabaseDate(dataTable.Rows[0]["StartDate"].ToString());
                dateTimePicker_End.Value = DBOperations.FromDatabaseDate(dataTable.Rows[0]["EndDate"].ToString());
                Preis_TextBox.Text = dataTable.Rows[0]["Price"].ToString();
            }
        }

        private void SetRulesForDateTimePicker(DateTimePicker dateTimePicker_Beginn, DateTimePicker dateTimePicker_End)
        {
            dateTimePicker_Beginn.Format = DateTimePickerFormat.Custom;
            dateTimePicker_End.Format = DateTimePickerFormat.Custom;
            dateTimePicker_Beginn.CustomFormat = "dd.MM.yyyy";
            dateTimePicker_End.CustomFormat = "dd.MM.yyyy";
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            RentalOrderRepository.Delete(rentalOrderId);
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (IsRentalOrderInputValid(comboBox_Kunden, comboBox_Anhaenger, dateTimePicker_Beginn, dateTimePicker_End, Preis_TextBox))
            {
                string startDate = DBOperations.ToDatabaseDate(dateTimePicker_Beginn.Value);
                string endDate = DBOperations.ToDatabaseDate(dateTimePicker_End.Value);
                int customerId = Convert.ToInt32(comboBox_Kunden.SelectedValue);
                int trailerId = Convert.ToInt32(comboBox_Anhaenger.SelectedValue);
                RentalOrderRepository.Update(rentalOrderId, trailerId, customerId, startDate, endDate, Preis_TextBox.Text.Trim());
                this.Close();
            }
        }

        private bool IsRentalOrderInputValid(ComboBox comboBox_Kunden, ComboBox comboBox_Anhaenger, DateTimePicker dateTimePicker_Beginn, DateTimePicker dateTimePicker_End, TextBox preis_TextBox)
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

            if (!RentalOrderValidator.IsDateRangeValid(dateTimePicker_Beginn.Value, dateTimePicker_End.Value))
            {
                MessageBox.Show("Das Enddatum muss nach dem Beginndatum liegen!");
                return false;
            }

            int trailerId = Convert.ToInt32(comboBox_Anhaenger.SelectedValue);
            if (RentalOrderRepository.IsTrailerRentedInPeriod(trailerId, dateTimePicker_Beginn.Value, dateTimePicker_End.Value, rentalOrderId))
            {
                MessageBox.Show("Der ausgewählte Anhänger ist in diesem Zeitraum bereits vermietet!");
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
    }
}
