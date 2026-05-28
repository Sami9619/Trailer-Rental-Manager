using Trailer_Rental_Manager.Forms;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.RentalOrders;
using Trailer_Rental_Manager.Forms.Trailers;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Customers
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
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
                CustomerEditForm customerEditForm = new CustomerEditForm(customerId);
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
            FormsOperations.NavigateTo(this, new HomeForm());
        }

        private void Kunden_AnhaengerButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new TrailerForm());
        }

        private void Kunden_RentalOrdersButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new RentalOrderForm());
        }

        private void Kunden_GarageButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new GarageForm());
        }

        private void Kunden_StatistikButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new StatisticsForm());
        }

        private void KundenDataExportButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = CustomerRepository.GetExport();
            string filePath = FormsOperations.ShowSaveCsvDialog();
            CsvService.ExportToCsv(dataTable, filePath);
        }

        private void KundenDataImportButton_Click(object sender, EventArgs e)
        {
            string filePath = FormsOperations.ShowOpenCsvDialog();
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
