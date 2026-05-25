using Trailer_Rental_Manager.Repositories;
using System;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Trailers
{
    public partial class TrailerCreateForm : Form
    {
        public TrailerCreateForm()
        {
            InitializeComponent();
        }

        private void HinzufuegenButton_Click(object sender, EventArgs e)
        {
            if (TrailerEditForm.IsTrailerInputValid(Anhaengername_TextBox, Typ_TextBox, MaxZuladung_TextBox, Hoehe_TextBox, Breite_TextBox, Laenge_TextBox))
            {
                TrailerRepository.Insert(
                    Anhaengername_TextBox.Text.Trim(),
                    Typ_TextBox.Text.Trim(),
                    MaxZuladung_TextBox.Text.Trim(),
                    Hoehe_TextBox.Text.Trim(),
                    Breite_TextBox.Text.Trim(),
                    Laenge_TextBox.Text.Trim()
                );

                this.Close();
            }
        }
    }
}
