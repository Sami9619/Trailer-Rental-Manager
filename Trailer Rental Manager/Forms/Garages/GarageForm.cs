using Trailer_Rental_Manager.Forms;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.RentalOrders;
using Trailer_Rental_Manager.Forms.Trailers;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Garages
{
    public partial class GarageForm : Form
    {
        public GarageForm()
        {
            InitializeComponent();
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
                GarageEditForm garageEditForm = new GarageEditForm(garageId);
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

        private void RentalOrdersButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new RentalOrderForm());
        }

        private void StatistikButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new StatisticsForm());
        }

        private void GaragenDataImportButton_Click(object sender, EventArgs e)
        {
            string filePath = FormsOperations.ShowOpenCsvDialog();
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
            string filePath = FormsOperations.ShowSaveCsvDialog();
            CsvService.ExportToCsv(dataTable, filePath);
        }
    }
}
