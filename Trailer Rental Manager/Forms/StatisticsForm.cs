using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.RentalOrders;
using Trailer_Rental_Manager.Forms.Trailers;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Trailer_Rental_Manager.Forms
{
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            LoadRevenueStatistics();
        }

        private void LoadRevenueStatistics()
        {
            FormsOperations.ClearEverythingFromDataGridView(dataGridViewUmsatzAnhaenger);
            DataTable dataTable = RentalOrderRepository.GetRevenueByTrailer();
            dataGridViewUmsatzAnhaenger.DataSource = dataTable;
            CreateChartAnhaengerStatistik(chartAnhaengerStatistik, dataGridViewUmsatzAnhaenger);

            double totalRevenue = 0;
            foreach (DataGridViewRow row in dataGridViewUmsatzAnhaenger.Rows)
            {
                if (!row.IsNewRow)
                {
                    totalRevenue += Convert.ToDouble(row.Cells[2].Value);
                }
            }

            textBoxGesamterUmsatz.Text = totalRevenue.ToString();
        }

        private void CreateChartAnhaengerStatistik(Chart chartAnhaengerStatistik, DataGridView dataGridViewUmsatzAnhaenger)
        {
            chartAnhaengerStatistik.Series.RemoveAt(0);
            chartAnhaengerStatistik.Series.Add("Umsatz");

            foreach (DataGridViewRow row in dataGridViewUmsatzAnhaenger.Rows)
            {
                if (!row.IsNewRow)
                {
                    chartAnhaengerStatistik.Series["Umsatz"].Points.AddXY(row.Cells[0].Value.ToString(), row.Cells[2].Value);
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                LoadRevenueStatistics();
            }
            else if (tabControl.SelectedIndex == 1)
            {
                FormsOperations.ClearEverythingFromDataGridView(dataGridViewMieteGaragen);
                DataTable dataTable = GarageRepository.GetOverview();
                dataGridViewMieteGaragen.DataSource = dataTable;

                double totalRent = 0;
                foreach (DataGridViewRow row in dataGridViewMieteGaragen.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        totalRent += Convert.ToDouble(row.Cells[5].Value);
                    }
                }

                textBoxGesamteMiete.Text = totalRent.ToString();
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

        private void RentalOrdersButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new RentalOrderForm());
        }

        private void GarageButton_Click(object sender, EventArgs e)
        {
            FormsOperations.NavigateTo(this, new GarageForm());
        }
    }
}
