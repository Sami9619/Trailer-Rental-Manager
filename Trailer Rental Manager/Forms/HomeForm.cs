using Trailer_Rental_Manager.Forms;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.RentalOrders;
using Trailer_Rental_Manager.Forms.Trailers;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using System;
using System.Data;
using System.Drawing;
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
            this.Hide();
            Form customers = new CustomerForm();
            customers.StartPosition = FormStartPosition.Manual;
            customers.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            customers.Closed += (s, args) => this.Close();
            customers.Show();
        }

        private void Home_AnhaengerButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form trailers = new TrailerForm();
            trailers.StartPosition = FormStartPosition.Manual;
            trailers.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            trailers.Closed += (s, args) => this.Close();
            trailers.Show();
        }

        private void Home_RentalOrdersButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form rentalOrders = new RentalOrderForm();
            rentalOrders.StartPosition = FormStartPosition.Manual;
            rentalOrders.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            rentalOrders.Closed += (s, args) => this.Close();
            rentalOrders.Show();
        }

        private void Home_GarageButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form garage = new GarageForm();
            garage.StartPosition = FormStartPosition.Manual;
            garage.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            garage.Closed += (s, args) => this.Close();
            garage.Show();
        }

        private void Home_StatistikButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form statistics = new StatisticsForm();
            statistics.StartPosition = FormStartPosition.Manual;
            statistics.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            statistics.Closed += (s, args) => this.Close();
            statistics.Show();
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
