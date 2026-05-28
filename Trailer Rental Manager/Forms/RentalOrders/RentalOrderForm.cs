using Trailer_Rental_Manager.Forms;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.Trailers;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.RentalOrders
{
    public partial class RentalOrderForm : Form
    {
        public RentalOrderForm()
        {
            InitializeComponent();
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

        private void HomeButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new HomeForm());
        }

        private void KundenButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new CustomerForm());
        }

        private void AnhaengerButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new TrailerForm());
        }

        private void GarageButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new GarageForm());
        }

        private void StatistikButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new StatisticsForm());
        }

        private void RentalOrdersDataImportButton_Click(object sender, EventArgs e)
        {
            string filePath = FormsOperations.ShowOpenCsvDialog();
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
            string filePath = FormsOperations.ShowSaveCsvDialog();
            CsvService.ExportToCsv(dataTable, filePath);
        }
    }
}
