using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
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
                FormsOperations.FillCustomerComboBox(comboBox_Kunden);
                FormsOperations.FillTrailerComboBox(comboBox_Anhaenger);
                SetDatePickerFormat(dateTimePicker_Beginn, dateTimePicker_End);

                rentalOrderIdTextBox.Text = dataTable.Rows[0]["RentalOrderId"].ToString();
                comboBox_Kunden.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["CustomerId"]);
                comboBox_Anhaenger.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["TrailerId"]);
                dateTimePicker_Beginn.Value = DBOperations.FromDatabaseDate(dataTable.Rows[0]["StartDate"].ToString());
                dateTimePicker_End.Value = DBOperations.FromDatabaseDate(dataTable.Rows[0]["EndDate"].ToString());
                Preis_TextBox.Text = dataTable.Rows[0]["Price"].ToString();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            RentalOrderRepository.Delete(rentalOrderId);
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (FormsOperations.ValidateRentalOrderInput(comboBox_Kunden, comboBox_Anhaenger, dateTimePicker_Beginn, dateTimePicker_End, Preis_TextBox, true, true, rentalOrderId))
            {
                string startDate = DBOperations.ToDatabaseDate(dateTimePicker_Beginn.Value);
                string endDate = DBOperations.ToDatabaseDate(dateTimePicker_End.Value);
                int customerId = Convert.ToInt32(comboBox_Kunden.SelectedValue);
                int trailerId = Convert.ToInt32(comboBox_Anhaenger.SelectedValue);
                RentalOrderRepository.Update(rentalOrderId, trailerId, customerId, startDate, endDate, Preis_TextBox.Text.Trim());
                this.Close();
            }
        }

        private static void SetDatePickerFormat(DateTimePicker start, DateTimePicker end)
        {
            start.Format = DateTimePickerFormat.Custom;
            end.Format = DateTimePickerFormat.Custom;
            start.CustomFormat = "dd.MM.yyyy";
            end.CustomFormat = "dd.MM.yyyy";
        }
    }
}
