using Trailer_Rental_Manager.Forms;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.RentalOrders;
using Trailer_Rental_Manager.Forms.Trailers;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Trailer_Rental_Manager.Operations;

namespace Trailer_Rental_Manager.Forms.Garages
{
    public partial class GarageForm : Form
    {
        public GarageForm()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form home = new HomeForm();
            home.StartPosition = FormStartPosition.Manual;
            home.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            home.Closed += (s, args) => this.Close();
            home.Show();
        }

        private void KundenButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form customers = new CustomerForm();
            customers.StartPosition = FormStartPosition.Manual;
            customers.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            customers.Closed += (s, args) => this.Close();
            customers.Show();
        }

        private void AnhaengerButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form trailers = new TrailerForm();
            trailers.StartPosition = FormStartPosition.Manual;
            trailers.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            trailers.Closed += (s, args) => this.Close();
            trailers.Show();
        }

        private void RentalOrdersButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form rentalOrders = new RentalOrderForm();
            rentalOrders.StartPosition = FormStartPosition.Manual;
            rentalOrders.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            rentalOrders.Closed += (s, args) => this.Close();
            rentalOrders.Show();
        }

        private void StatistikButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form statistics = new StatisticsForm();
            statistics.StartPosition = FormStartPosition.Manual;
            statistics.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            statistics.Closed += (s, args) => this.Close();
            statistics.Show();
        }

        private void GarageForm_Load(object sender, EventArgs e)
        {
            LoadGarages();
        }

        private void LoadGarages()
        {
            FormsOperations.ClearEverythingFromDataGridView(dataGridViewGarage);
            FormsOperations.AddButtonToDataGridView(dataGridViewGarage, "", "Bearbeiten");
            dataGridViewGarage.DataSource = GarageRepository.GetOverview();
        }

        private void dataGridViewGarage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                int garageId = Convert.ToInt32(dataGridViewGarage.Rows[e.RowIndex].Cells[1].Value);
                GarageEditForm garageEditForm = new GarageEditForm(garageId, this);
                garageEditForm.ShowDialog();
                LoadGarages();
            }
        }

        private void GarageHinzufuegenButton_Click(object sender, EventArgs e)
        {
            GarageCreateForm garageCreateForm = new GarageCreateForm();
            garageCreateForm.ShowDialog();
            LoadGarages();
        }

        private void GaragenDataImportButton_Click(object sender, EventArgs e)
        {
            string filePath = CsvService.ShowOpenDialog();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            try
            {
                DataTable dataTable = CsvService.ImportFromCsv(filePath);
                GarageCsvImportService.Import(dataTable);
                LoadGarages();
            }
            catch (Exception exception)
            {
                FormsOperations.ShowCsvImportFailed(exception);
            }
        }

        private void GaragenDataExportButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = GarageRepository.GetExport();
            string filePath = CsvService.ShowSaveDialog();
            CsvService.ExportToCsv(dataTable, filePath);
        }
    }
}
