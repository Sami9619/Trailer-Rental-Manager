using Trailer_Rental_Manager.Forms;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.Trailers;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.RentalOrders
{
    public partial class RentalOrderForm : Form
    {
        public RentalOrderForm()
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

        private void RentalOrderForm_Load(object sender, EventArgs e)
        {
            LoadRentalOrders();
        }

        private void LoadRentalOrders()
        {
            FormsOperations.ClearEverythingFromDataGridView(rentalOrdersDataGridView);
            FormsOperations.AddButtonToDataGridView(rentalOrdersDataGridView, "", "Bearbeiten");
            DataTable dataTable = RentalOrderRepository.GetOverview();
            FormatDatesInOrdersGrid(dataTable);
            rentalOrdersDataGridView.DataSource = dataTable;
        }

        private void FormatDatesInOrdersGrid(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                row["Beginndatum"] = DBOperations.ToGermanDate(row["Beginndatum"].ToString());
                row["Enddatum"] = DBOperations.ToGermanDate(row["Enddatum"].ToString());
            }
        }

        private void AddRentalOrderButton_Click(object sender, EventArgs e)
        {
            RentalOrderCreateForm rentalOrderCreateForm = new RentalOrderCreateForm();
            rentalOrderCreateForm.ShowDialog();
            LoadRentalOrders();
        }

        private void RentalOrdersDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                int rentalOrderId = Convert.ToInt32(rentalOrdersDataGridView.Rows[e.RowIndex].Cells[1].Value);
                RentalOrderEditForm rentalOrderEditForm = new RentalOrderEditForm(rentalOrderId);
                rentalOrderEditForm.ShowDialog();
                LoadRentalOrders();
            }
        }

        private void RentalOrdersDataImportButton_Click(object sender, EventArgs e)
        {
            string filePath = CsvService.ShowOpenDialog();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            try
            {
                DataTable dataTable = CsvService.ImportFromCsv(filePath);
                RentalOrderCsvImportService.Import(dataTable);
                LoadRentalOrders();
            }
            catch (Exception exception)
            {
                FormsOperations.ShowCsvImportFailed(exception);
            }
        }

        private void RentalOrdersDataExportButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = RentalOrderRepository.GetExport();
            string filePath = CsvService.ShowSaveDialog();
            CsvService.ExportToCsv(dataTable, filePath);
        }
    }
}
