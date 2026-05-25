using Trailer_Rental_Manager.Repositories;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Garages
{
    public partial class GarageEditForm : Form
    {
        private readonly int garageId;
        private readonly GarageForm garagesForm;

        public GarageEditForm(int garageId, GarageForm garageForm)
        {
            InitializeComponent();
            this.garageId = garageId;
            garagesForm = garageForm;
        }

        private void GarageEditForm_Load(object sender, EventArgs e)
        {
            DataTable dataTable = GarageRepository.GetById(garageId);

            if (dataTable.Rows.Count > 0)
            {
                Garagennummer_TextBox.Text = dataTable.Rows[0]["GarageId"].ToString();
                Strasse_TextBox.Text = dataTable.Rows[0]["Street"].ToString();
                Hausnummer_TextBox.Text = dataTable.Rows[0]["HouseNumber"].ToString();
                PLZ_TextBox.Text = dataTable.Rows[0]["PostalCode"].ToString();
                Ort_TextBox.Text = dataTable.Rows[0]["City"].ToString();
                Miete_TextBox.Text = dataTable.Rows[0]["MonthlyRent"].ToString();
            }
        }

        private void KundenBearbeitungsUebersicht_DeleteButton_Click(object sender, EventArgs e)
        {
            GarageRepository.Delete(garageId);
            this.Close();
        }

        private void KundenBearbeitungsUebersicht_SaveButton_Click(object sender, EventArgs e)
        {
            if (IsGarageInputValid(Strasse_TextBox, Hausnummer_TextBox, PLZ_TextBox, Ort_TextBox, Miete_TextBox))
            {
                GarageRepository.Update(
                    garageId,
                    Strasse_TextBox.Text.Trim(),
                    Hausnummer_TextBox.Text.Trim(),
                    PLZ_TextBox.Text.Trim(),
                    Ort_TextBox.Text.Trim(),
                    Miete_TextBox.Text.Trim()
                );

                this.Close();
            }
        }

        internal static bool IsGarageInputValid(TextBox strasse_TextBox, TextBox hausnummer_TextBox, TextBox pLZ_TextBox, TextBox ort_TextBox, TextBox miete_TextBox)
        {
            if (strasse_TextBox.Text.Length == 0)
            {
                MessageBox.Show("Straße darf nicht leer sein!");
                return false;
            }

            if (hausnummer_TextBox.Text.Length == 0)
            {
                MessageBox.Show("Hausnummer darf nicht leer sein!");
                return false;
            }

            if (pLZ_TextBox.Text.Length == 0)
            {
                MessageBox.Show("PLZ darf nicht leer sein!");
                return false;
            }

            if (ort_TextBox.Text.Length == 0)
            {
                MessageBox.Show("Ort darf nicht leer sein!");
                return false;
            }

            if (miete_TextBox.Text.Length == 0)
            {
                MessageBox.Show("Miete darf nicht leer sein!");
                return false;
            }

            if (miete_TextBox.Text.Trim().Length > 0 && !double.TryParse(miete_TextBox.Text.Trim(), out double rent))
            {
                MessageBox.Show("Miete darf nur eine Zahl sein!");
                return false;
            }

            return true;
        }
    }
}
