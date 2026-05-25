using Trailer_Rental_Manager.Repositories;
using Trailer_Rental_Manager.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Trailers
{
    public partial class TrailerEditForm : Form
    {
        private readonly int trailerId;
        private readonly TrailerForm trailersForm;

        public TrailerEditForm(int trailerId, TrailerForm trailerForm)
        {
            InitializeComponent();
            this.trailerId = trailerId;
            trailersForm = trailerForm;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            TrailerRepository.Delete(Convert.ToInt32(Anhaengernummer_TextBox.Text.Trim()));
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (IsTrailerInputValid(Anhaengername_TextBox, Typ_TextBox, MaximaleZuladung_TextBox, Hoehe_TextBox, Breite_TextBox, Laenge_TextBox))
            {
                TrailerRepository.Update(
                    Convert.ToInt32(Anhaengernummer_TextBox.Text.Trim()),
                    Anhaengername_TextBox.Text.Trim(),
                    Typ_TextBox.Text.Trim(),
                    MaximaleZuladung_TextBox.Text.Trim(),
                    Hoehe_TextBox.Text.Trim(),
                    Breite_TextBox.Text.Trim(),
                    Laenge_TextBox.Text.Trim()
                );

                this.Close();
            }
        }

        internal static bool IsTrailerInputValid(TextBox anhaengername_TextBox, TextBox typ_TextBox, TextBox maximaleZuladung_TextBox, TextBox hoehe_TextBox, TextBox breite_TextBox, TextBox laenge_TextBox)
        {
            if (anhaengername_TextBox.Text.Length == 0)
            {
                MessageBox.Show("Anhängername darf nicht leer sein!");
                return false;
            }

            if (typ_TextBox.Text.Length == 0)
            {
                MessageBox.Show("Typ darf nicht leer sein!");
                return false;
            }

            if (!TrailerValidator.IsOptionalNumberValid(maximaleZuladung_TextBox.Text))
            {
                MessageBox.Show("Maximale Zuladung darf nur eine Zahl sein!");
                return false;
            }

            if (!TrailerValidator.IsOptionalNumberValid(hoehe_TextBox.Text))
            {
                MessageBox.Show("Höhe darf nur eine Zahl sein!");
                return false;
            }

            if (!TrailerValidator.IsOptionalNumberValid(breite_TextBox.Text))
            {
                MessageBox.Show("Breite darf nur eine Zahl sein!");
                return false;
            }

            if (!TrailerValidator.IsOptionalNumberValid(laenge_TextBox.Text))
            {
                MessageBox.Show("Länge darf nur eine Zahl sein!");
                return false;
            }

            return true;
        }

        private void TrailerEditForm_Load(object sender, EventArgs e)
        {
            DataTable dataTable = TrailerRepository.GetById(trailerId);

            if (dataTable.Rows.Count > 0)
            {
                Anhaengernummer_TextBox.Text = dataTable.Rows[0]["TrailerId"].ToString();
                Anhaengername_TextBox.Text = dataTable.Rows[0]["TrailerName"].ToString();
                Typ_TextBox.Text = dataTable.Rows[0]["TrailerType"].ToString();
                MaximaleZuladung_TextBox.Text = dataTable.Rows[0]["MaxPayload"].ToString();
                Hoehe_TextBox.Text = dataTable.Rows[0]["Height"].ToString();
                Breite_TextBox.Text = dataTable.Rows[0]["Width"].ToString();
                Laenge_TextBox.Text = dataTable.Rows[0]["Length"].ToString();
            }
        }
    }
}
