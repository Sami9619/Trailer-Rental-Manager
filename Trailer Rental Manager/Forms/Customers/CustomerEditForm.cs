using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;
using System;
using System.Data;
using System.Windows.Forms;

namespace Trailer_Rental_Manager.Forms.Customers
{
    public partial class CustomerEditForm : Form
    {
        private readonly int customerId;
        private bool drivingLicenseDateCleared;

        public CustomerEditForm(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
        }

        private void CustomerEditForm_Load(object sender, EventArgs e)
        {
            DataTable dataTable = CustomerRepository.GetById(customerId);

            if (dataTable.Rows.Count > 0)
            {
                CustomerEditForm_KundenNummer.Text = dataTable.Rows[0]["CustomerId"].ToString();
                CustomerEditForm_Geschlecht.Text = dataTable.Rows[0]["Gender"].ToString();
                CustomerEditForm_Vorname.Text = dataTable.Rows[0]["FirstName"].ToString();
                CustomerEditForm_Nachname.Text = dataTable.Rows[0]["LastName"].ToString();
                CustomerEditForm_PLZ.Text = dataTable.Rows[0]["PostalCode"].ToString();
                CustomerEditForm_Strasse.Text = dataTable.Rows[0]["Street"].ToString();
                CustomerEditForm_Hausnummer.Text = dataTable.Rows[0]["HouseNumber"].ToString();
                CustomerEditForm_Ort.Text = dataTable.Rows[0]["City"].ToString();
                CustomerEditForm_Ausweisnummer.Text = dataTable.Rows[0]["IdDocumentNumber"].ToString();

                string storedDrivingLicenseIssueDate = dataTable.Rows[0]["DrivingLicenseIssueDate"].ToString();
                if (!string.IsNullOrWhiteSpace(storedDrivingLicenseIssueDate))
                {
                    CustomerEditForm_Fuererscheinausstellungsdatum_Date.Value = DBOperations.FromDatabaseDate(storedDrivingLicenseIssueDate);
                    CustomerEditForm_Fuererscheinausstellungsdatum_Date.CustomFormat = "dd.MM.yyyy";
                }
                else
                {
                    drivingLicenseDateCleared = true;
                    ClearDrivingLicenseIssueDate();
                }

                CustomerEditForm_Fuererscheinklasse.Text = dataTable.Rows[0]["DrivingLicenseClass"].ToString();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (FormsOperations.ValidateCustomerInput(CustomerEditForm_Geschlecht, CustomerEditForm_Vorname, CustomerEditForm_Nachname))
            {
                string drivingLicenseIssueDate = null;

                if (!drivingLicenseDateCleared)
                {
                    drivingLicenseIssueDate = DBOperations.ToDatabaseDate(CustomerEditForm_Fuererscheinausstellungsdatum_Date.Value);
                }

                CustomerRepository.Update(
                    Convert.ToInt32(CustomerEditForm_KundenNummer.Text.Trim()),
                    CustomerEditForm_Geschlecht.Text.Trim(),
                    CustomerEditForm_Vorname.Text.Trim(),
                    CustomerEditForm_Nachname.Text.Trim(),
                    CustomerEditForm_PLZ.Text.Trim(),
                    CustomerEditForm_Strasse.Text.Trim(),
                    CustomerEditForm_Hausnummer.Text.Trim(),
                    CustomerEditForm_Ort.Text.Trim(),
                    CustomerEditForm_Ausweisnummer.Text.Trim(),
                    drivingLicenseIssueDate,
                    CustomerEditForm_Fuererscheinklasse.Text.Trim()
                );

                this.Close();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CustomerRepository.Delete(Convert.ToInt32(CustomerEditForm_KundenNummer.Text.Trim()));
            this.Close();
        }

        private void buttonKeineAngabe_Click(object sender, EventArgs e)
        {
            drivingLicenseDateCleared = true;
            ClearDrivingLicenseIssueDate();
        }

        private void CustomerEditForm_Fuererscheinausstellungsdatum_Date_MouseUp(object sender, MouseEventArgs e)
        {
            drivingLicenseDateCleared = false;
            CustomerEditForm_Fuererscheinausstellungsdatum_Date.Format = DateTimePickerFormat.Custom;
            CustomerEditForm_Fuererscheinausstellungsdatum_Date.CustomFormat = "dd.MM.yyyy";
        }

        private void ClearDrivingLicenseIssueDate()
        {
            CustomerEditForm_Fuererscheinausstellungsdatum_Date.Format = DateTimePickerFormat.Custom;
            CustomerEditForm_Fuererscheinausstellungsdatum_Date.CustomFormat = " ";
        }
    }
}
