using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using System;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Customers
{
    public partial class CustomerCreateForm : Form
    {
        private bool dateValueChanged;

        public CustomerCreateForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (FormsOperations.ValidateCustomerInput(Geschlecht_TextBox, Vorname_TextBox, Nachname_TextBox))
            {
                string drivingLicenseIssueDate = dateValueChanged
                    ? DBOperations.ToDatabaseDate(FuererscheinDatum_DateTimePicker.Value)
                    : null;

                CustomerRepository.Insert(
                    Geschlecht_TextBox.Text.Trim(),
                    Vorname_TextBox.Text.Trim(),
                    Nachname_TextBox.Text.Trim(),
                    PLZ_TextBox.Text.Trim(),
                    Strasse_TextBox.Text.Trim(),
                    Hausnummer_TextBox.Text.Trim(),
                    Ort_TextBox.Text.Trim(),
                    Ausweisnummer__TextBox.Text.Trim(),
                    drivingLicenseIssueDate,
                    Fuererscheinklasse_TextBox.Text.Trim()
                );

                this.Close();
            }
        }

        private void FuererscheinDatum_DateTimePicker_MouseUp(object sender, MouseEventArgs e)
        {
            dateValueChanged = true;
            FuererscheinDatum_DateTimePicker.CustomFormat = "dd.MM.yyyy";
        }

        private void buttonKeineAngabe_Click(object sender, EventArgs e)
        {
            dateValueChanged = false;
            FuererscheinDatum_DateTimePicker.CustomFormat = " ";
        }
    }
}
