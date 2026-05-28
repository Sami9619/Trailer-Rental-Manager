using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Garages
{
    public partial class GarageEditForm : Form
    {
        private readonly int garageId;

        public GarageEditForm(int garageId)
        {
            InitializeComponent();
            this.garageId = garageId;
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
            if (FormsOperations.ValidateGarageInput(Strasse_TextBox, Hausnummer_TextBox, PLZ_TextBox, Ort_TextBox, Miete_TextBox))
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
    }
}
