using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using System;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.RentalOrders
{
    public partial class RentalOrderCreateForm : Form
    {
        private bool startDateSelected;
        private bool endDateSelected;

        public RentalOrderCreateForm()
        {
            InitializeComponent();
        }

        private void RentalOrderCreateForm_Load(object sender, EventArgs e)
        {
            FormsOperations.FillCustomerComboBox(comboBox_Kunden);
            FormsOperations.FillTrailerComboBox(comboBox_Anhaenger);
            SetDatePickerRulesForCreate(dateTimePicker_Beginn, dateTimePicker_End);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (FormsOperations.ValidateRentalOrderInput(comboBox_Kunden, comboBox_Anhaenger, dateTimePicker_Beginn, dateTimePicker_End, Preis_TextBox, startDateSelected, endDateSelected))
            {
                string startDate = DBOperations.ToDatabaseDate(dateTimePicker_Beginn.Value);
                string endDate = DBOperations.ToDatabaseDate(dateTimePicker_End.Value);
                int customerId = Convert.ToInt32(comboBox_Kunden.SelectedValue);
                int trailerId = Convert.ToInt32(comboBox_Anhaenger.SelectedValue);
                RentalOrderRepository.Insert(customerId, trailerId, startDate, endDate, Preis_TextBox.Text.Trim());
                this.Close();
            }
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

        private static void SetDatePickerRulesForCreate(DateTimePicker start, DateTimePicker end)
        {
            start.MaxDate = new DateTime(9998, 12, 31);
            end.MaxDate = new DateTime(9998, 12, 31);
            start.MinDate = DateTime.Today;
            end.MinDate = DateTime.Today;
            start.Format = DateTimePickerFormat.Custom;
            end.Format = DateTimePickerFormat.Custom;
            start.CustomFormat = " ";
            end.CustomFormat = " ";
        }
    }
}
