using Trailer_Rental_Manager.Forms;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.RentalOrders;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Trailer_Rental_Manager.Operations;

namespace Trailer_Rental_Manager.Forms.Trailers
{
    public partial class TrailerForm : Form
    {
        public TrailerForm()
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

        private void RentalOrdersButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form rentalOrders = new RentalOrderForm();
            rentalOrders.StartPosition = FormStartPosition.Manual;
            rentalOrders.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            rentalOrders.Closed += (s, args) => this.Close();
            rentalOrders.Show();
        }

        private void GarageButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form garage = new GarageForm();
            garage.StartPosition = FormStartPosition.Manual;
            garage.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            garage.Closed += (s, args) => this.Close();
            garage.Show();
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

        private void Anhaenger_Load(object sender, EventArgs e)
        {
            LoadTrailers();
        }

        private void LoadTrailers()
        {
            FormsOperations.ClearEverythingFromDataGridView(dataGridViewAnhaenger);
            FormsOperations.AddButtonToDataGridView(dataGridViewAnhaenger, "", "Bearbeiten");
            dataGridViewAnhaenger.DataSource = TrailerRepository.GetOverview();
        }

        private void dataGridViewAnhaenger_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                int trailerId = Convert.ToInt32(dataGridViewAnhaenger.Rows[e.RowIndex].Cells[1].Value);
                TrailerEditForm trailerEditForm = new TrailerEditForm(trailerId, this);
                trailerEditForm.ShowDialog();
                LoadTrailers();
            }
        }

        private void AnhaengerHinzufuegenButton_Click(object sender, EventArgs e)
        {
            TrailerCreateForm trailerCreateForm = new TrailerCreateForm();
            trailerCreateForm.ShowDialog();
            LoadTrailers();
        }

        private void AnhaengerDataImportButton_Click(object sender, EventArgs e)
        {
            string filePath = CsvService.ShowOpenDialog();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            try
            {
                DataTable dataTable = CsvService.ImportFromCsv(filePath);
                TrailerCsvImportService.Import(dataTable);
                LoadTrailers();
            }
            catch (Exception exception)
            {
                FormsOperations.ShowCsvImportFailed(exception);
            }
        }

        private void AnhaengerDataExportButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = TrailerRepository.GetExport();
            string filePath = CsvService.ShowSaveDialog();
            CsvService.ExportToCsv(dataTable, filePath);
        }
    }
}
