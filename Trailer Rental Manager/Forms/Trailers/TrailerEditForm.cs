using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Trailers
{
    public partial class TrailerEditForm : Form
    {
        private readonly int trailerId;

        public TrailerEditForm(int trailerId)
        {
            InitializeComponent();
            this.trailerId = trailerId;
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            TrailerRepository.Delete(Convert.ToInt32(Anhaengernummer_TextBox.Text.Trim()));
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (FormsOperations.ValidateTrailerInput(Anhaengername_TextBox, Typ_TextBox, MaximaleZuladung_TextBox, Hoehe_TextBox, Breite_TextBox, Laenge_TextBox))
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
    }
}
