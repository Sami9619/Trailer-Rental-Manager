using Trailer_Rental_Manager.Forms;
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

namespace Trailer_Rental_Manager.Forms.Customers
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        internal void KundenButton_Click(object sender, EventArgs e)
        {
        }

        private void Kunden_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            FormsOperations.ClearEverythingFromDataGridView(dataGridViewKunden);
            FormsOperations.AddButtonToDataGridView(dataGridViewKunden, "", "Bearbeiten");
            dataGridViewKunden.DataSource = CustomerRepository.GetOverview();
        }

        private void dataGridViewKunden_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                int customerId = Convert.ToInt32(dataGridViewKunden.Rows[e.RowIndex].Cells[1].Value);
                CustomerEditForm customerEditForm = new CustomerEditForm(customerId, this);
                customerEditForm.ShowDialog();
                LoadCustomers();
            }
        }

        private void KundeHinzufuegenButton_Click(object sender, EventArgs e)
        {
            CustomerCreateForm customerCreateForm = new CustomerCreateForm();
            customerCreateForm.ShowDialog();
            LoadCustomers();
        }

        private void Kunden_HomeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form home = new HomeForm();
            home.StartPosition = FormStartPosition.Manual;
            home.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            home.Closed += (s, args) => this.Close();
            home.Show();
        }

        private void Kunden_AnhaengerButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form trailers = new TrailerForm();
            trailers.StartPosition = FormStartPosition.Manual;
            trailers.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            trailers.Closed += (s, args) => this.Close();
            trailers.Show();
        }

        private void Kunden_RentalOrdersButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form rentalOrders = new RentalOrderForm();
            rentalOrders.StartPosition = FormStartPosition.Manual;
            rentalOrders.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            rentalOrders.Closed += (s, args) => this.Close();
            rentalOrders.Show();
        }

        private void Kunden_GarageButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form garage = new GarageForm();
            garage.StartPosition = FormStartPosition.Manual;
            garage.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            garage.Closed += (s, args) => this.Close();
            garage.Show();
        }

        private void Kunden_StatistikButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form statistics = new StatisticsForm();
            statistics.StartPosition = FormStartPosition.Manual;
            statistics.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            statistics.Closed += (s, args) => this.Close();
            statistics.Show();
        }

        private void KundenDataExportButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = CustomerRepository.GetExport();
            string filePath = CsvService.ShowSaveDialog();
            CsvService.ExportToCsv(dataTable, filePath);
        }

        private void KundenDataImportButton_Click(object sender, EventArgs e)
        {
            string filePath = CsvService.ShowOpenDialog();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            try
            {
                DataTable dataTable = CsvService.ImportFromCsv(filePath);
                CustomerCsvImportService.Import(dataTable);
                LoadCustomers();
            }
            catch (Exception exception)
            {
                FormsOperations.ShowCsvImportFailed(exception);
            }
        }
    }
}
