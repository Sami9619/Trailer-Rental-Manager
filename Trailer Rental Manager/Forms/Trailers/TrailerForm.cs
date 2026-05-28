using Trailer_Rental_Manager.Forms;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.RentalOrders;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Trailers
{
    public partial class TrailerForm : Form
    {
        public TrailerForm()
        {
            InitializeComponent();
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
                TrailerEditForm trailerEditForm = new TrailerEditForm(trailerId);
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

        private void HomeButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new HomeForm());
        }

        private void KundenButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new CustomerForm());
        }

        private void RentalOrdersButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new RentalOrderForm());
        }

        private void GarageButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new GarageForm());
        }

        private void StatistikButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new StatisticsForm());
        }

        private void AnhaengerDataImportButton_Click(object sender, EventArgs e)
        {
            string filePath = FormsOperations.ShowOpenCsvDialog();
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
            string filePath = FormsOperations.ShowSaveCsvDialog();
            CsvService.ExportToCsv(dataTable, filePath);
        }
    }
}
