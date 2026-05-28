using Trailer_Rental_Manager.Forms;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.RentalOrders;
using Trailer_Rental_Manager.Forms.Trailers;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            SetDateTimePickerRules();
            FillRentedTrailers();
            FillAvailableTrailers();
        }

        private void FillRentedTrailers()
        {
            FormsOperations.ClearEverythingFromDataGridView(dataGridViewVermieteteAnhaenger);
            DataTable dataTable = RentalOrderRepository.GetRentedTrailers(BeginnDatum.Value, EndDatum.Value);
            FormatRentedUntilDate(dataTable);
            dataGridViewVermieteteAnhaenger.DataSource = dataTable;
        }

        private void FormatRentedUntilDate(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                row["Vermietet bis"] = DBOperations.ToGermanDate(row["Vermietet bis"].ToString());
            }
        }

        private void FillAvailableTrailers()
        {
            FormsOperations.ClearEverythingFromDataGridView(dataGridViewVerfuegbareAnhaenger);
            DataTable dataTable = RentalOrderRepository.GetAvailableTrailers(BeginnDatum.Value, EndDatum.Value);
            dataGridViewVerfuegbareAnhaenger.DataSource = dataTable;
        }

        private void SetDateTimePickerRules()
        {
            BeginnDatum.Format = DateTimePickerFormat.Custom;
            EndDatum.Format = DateTimePickerFormat.Custom;
            BeginnDatum.CustomFormat = "dd.MM.yyyy";
            EndDatum.CustomFormat = "dd.MM.yyyy";
            BeginnDatum.Value = DateTime.Today;
            EndDatum.Value = DateTime.Today;
        }

        private void Home_KundenButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new CustomerForm());
        }

        private void Home_AnhaengerButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new TrailerForm());
        }

        private void Home_RentalOrdersButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new RentalOrderForm());
        }

        private void Home_GarageButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new GarageForm());
        }

        private void Home_StatistikButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new StatisticsForm());
        }

        private void BeginnDatum_ValueChanged(object sender, EventArgs e)
        {
            if (EndDatum.Value < BeginnDatum.Value)
            {
                EndDatum.Value = BeginnDatum.Value;
            }

            EndDatum.MinDate = BeginnDatum.Value;
            FillAvailableTrailers();
            FillRentedTrailers();
        }

        private void EndDatum_ValueChanged(object sender, EventArgs e)
        {
            FillAvailableTrailers();
            FillRentedTrailers();
        }
    }
}
