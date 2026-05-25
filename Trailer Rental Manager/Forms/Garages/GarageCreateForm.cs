using Trailer_Rental_Manager.Repositories;
using System;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Garages
{
    public partial class GarageCreateForm : Form
    {
        public GarageCreateForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (GarageEditForm.IsGarageInputValid(Strasse_TextBox, Hausnummer_TextBox, PLZ_TextBox, Ort_TextBox, Miete_TextBox))
            {
                GarageRepository.Insert(
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
