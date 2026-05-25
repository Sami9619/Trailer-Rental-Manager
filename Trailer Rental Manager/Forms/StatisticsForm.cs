using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Forms.Customers;
using Trailer_Rental_Manager.Forms.Garages;
using Trailer_Rental_Manager.Forms.RentalOrders;
using Trailer_Rental_Manager.Forms.Trailers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Trailer_Rental_Manager.Operations;

namespace Trailer_Rental_Manager.Forms
{
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
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

        private void GarageButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form garage = new GarageForm();
            garage.StartPosition = FormStartPosition.Manual;
            garage.Location = new Point(this.DesktopLocation.X, this.DesktopLocation.Y);
            garage.Closed += (s, args) => this.Close();
            garage.Show();
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
    }
}
