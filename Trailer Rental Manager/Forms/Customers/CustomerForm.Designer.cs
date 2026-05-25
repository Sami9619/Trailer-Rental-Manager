namespace Trailer_Rental_Manager.Forms.Customers
{
    partial class CustomerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewKunden = new System.Windows.Forms.DataGridView();
            this.KundeHinzufuegenButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Kunden_GarageButton = new System.Windows.Forms.Button();
            this.Kunden_StatistikButton = new System.Windows.Forms.Button();
            this.Kunden_RentalOrdersButton = new System.Windows.Forms.Button();
            this.Kunden_AnhaengerButton = new System.Windows.Forms.Button();
            this.Kunden_KundenButton = new System.Windows.Forms.Button();
            this.Kunden_HomeButton = new System.Windows.Forms.Button();
            this.KundenDataExportButton = new System.Windows.Forms.Button();
            this.KundenDataImportButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKunden)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(143, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Kunden";
            // 
            // dataGridViewKunden
            // 
            this.dataGridViewKunden.AllowUserToAddRows = false;
            this.dataGridViewKunden.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dataGridViewKunden.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewKunden.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewKunden.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewKunden.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewKunden.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewKunden.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKunden.Location = new System.Drawing.Point(147, 59);
            this.dataGridViewKunden.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewKunden.Name = "dataGridViewKunden";
            this.dataGridViewKunden.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewKunden.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewKunden.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewKunden.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewKunden.RowTemplate.Height = 24;
            this.dataGridViewKunden.Size = new System.Drawing.Size(664, 256);
            this.dataGridViewKunden.TabIndex = 8;
            this.dataGridViewKunden.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewKunden_CellClick);
            // 
            // KundeHinzufuegenButton
            // 
            this.KundeHinzufuegenButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.KundeHinzufuegenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KundeHinzufuegenButton.Location = new System.Drawing.Point(361, 362);
            this.KundeHinzufuegenButton.Margin = new System.Windows.Forms.Padding(2);
            this.KundeHinzufuegenButton.Name = "KundeHinzufuegenButton";
            this.KundeHinzufuegenButton.Size = new System.Drawing.Size(244, 35);
            this.KundeHinzufuegenButton.TabIndex = 11;
            this.KundeHinzufuegenButton.Text = "Kunde Hinzufügen";
            this.KundeHinzufuegenButton.UseVisualStyleBackColor = false;
            this.KundeHinzufuegenButton.Click += new System.EventHandler(this.KundeHinzufuegenButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.Kunden_GarageButton);
            this.panel1.Controls.Add(this.Kunden_StatistikButton);
            this.panel1.Controls.Add(this.Kunden_RentalOrdersButton);
            this.panel1.Controls.Add(this.Kunden_AnhaengerButton);
            this.panel1.Controls.Add(this.Kunden_KundenButton);
            this.panel1.Controls.Add(this.Kunden_HomeButton);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 428);
            this.panel1.TabIndex = 12;
            // 
            // Kunden_GarageButton
            // 
            this.Kunden_GarageButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Kunden_GarageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kunden_GarageButton.Location = new System.Drawing.Point(0, 257);
            this.Kunden_GarageButton.Margin = new System.Windows.Forms.Padding(2);
            this.Kunden_GarageButton.Name = "Kunden_GarageButton";
            this.Kunden_GarageButton.Size = new System.Drawing.Size(129, 35);
            this.Kunden_GarageButton.TabIndex = 5;
            this.Kunden_GarageButton.Text = "Garage";
            this.Kunden_GarageButton.UseVisualStyleBackColor = false;
            this.Kunden_GarageButton.Click += new System.EventHandler(this.Kunden_GarageButton_Click);
            // 
            // Kunden_StatistikButton
            // 
            this.Kunden_StatistikButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Kunden_StatistikButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kunden_StatistikButton.Location = new System.Drawing.Point(0, 313);
            this.Kunden_StatistikButton.Margin = new System.Windows.Forms.Padding(2);
            this.Kunden_StatistikButton.Name = "Kunden_StatistikButton";
            this.Kunden_StatistikButton.Size = new System.Drawing.Size(129, 35);
            this.Kunden_StatistikButton.TabIndex = 4;
            this.Kunden_StatistikButton.Text = "Statistik";
            this.Kunden_StatistikButton.UseVisualStyleBackColor = false;
            this.Kunden_StatistikButton.Click += new System.EventHandler(this.Kunden_StatistikButton_Click);
            // 
            // Kunden_RentalOrdersButton
            // 
            this.Kunden_RentalOrdersButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Kunden_RentalOrdersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kunden_RentalOrdersButton.Location = new System.Drawing.Point(0, 202);
            this.Kunden_RentalOrdersButton.Margin = new System.Windows.Forms.Padding(2);
            this.Kunden_RentalOrdersButton.Name = "Kunden_RentalOrdersButton";
            this.Kunden_RentalOrdersButton.Size = new System.Drawing.Size(129, 35);
            this.Kunden_RentalOrdersButton.TabIndex = 3;
            this.Kunden_RentalOrdersButton.Text = "Aufträge";
            this.Kunden_RentalOrdersButton.UseVisualStyleBackColor = false;
            this.Kunden_RentalOrdersButton.Click += new System.EventHandler(this.Kunden_RentalOrdersButton_Click);
            // 
            // Kunden_AnhaengerButton
            // 
            this.Kunden_AnhaengerButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Kunden_AnhaengerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kunden_AnhaengerButton.Location = new System.Drawing.Point(0, 143);
            this.Kunden_AnhaengerButton.Margin = new System.Windows.Forms.Padding(2);
            this.Kunden_AnhaengerButton.Name = "Kunden_AnhaengerButton";
            this.Kunden_AnhaengerButton.Size = new System.Drawing.Size(129, 35);
            this.Kunden_AnhaengerButton.TabIndex = 2;
            this.Kunden_AnhaengerButton.Text = "Anhänger";
            this.Kunden_AnhaengerButton.UseVisualStyleBackColor = false;
            this.Kunden_AnhaengerButton.Click += new System.EventHandler(this.Kunden_AnhaengerButton_Click);
            // 
            // Kunden_KundenButton
            // 
            this.Kunden_KundenButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Kunden_KundenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kunden_KundenButton.Location = new System.Drawing.Point(0, 84);
            this.Kunden_KundenButton.Margin = new System.Windows.Forms.Padding(2);
            this.Kunden_KundenButton.Name = "Kunden_KundenButton";
            this.Kunden_KundenButton.Size = new System.Drawing.Size(129, 35);
            this.Kunden_KundenButton.TabIndex = 1;
            this.Kunden_KundenButton.Text = "Kunden";
            this.Kunden_KundenButton.UseVisualStyleBackColor = false;
            // 
            // Kunden_HomeButton
            // 
            this.Kunden_HomeButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Kunden_HomeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kunden_HomeButton.Location = new System.Drawing.Point(0, 28);
            this.Kunden_HomeButton.Margin = new System.Windows.Forms.Padding(2);
            this.Kunden_HomeButton.Name = "Kunden_HomeButton";
            this.Kunden_HomeButton.Size = new System.Drawing.Size(129, 35);
            this.Kunden_HomeButton.TabIndex = 0;
            this.Kunden_HomeButton.Text = "Home";
            this.Kunden_HomeButton.UseVisualStyleBackColor = false;
            this.Kunden_HomeButton.Click += new System.EventHandler(this.Kunden_HomeButton_Click);
            // 
            // KundenDataExportButton
            // 
            this.KundenDataExportButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.KundenDataExportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KundenDataExportButton.Location = new System.Drawing.Point(728, 13);
            this.KundenDataExportButton.Margin = new System.Windows.Forms.Padding(2);
            this.KundenDataExportButton.Name = "KundenDataExportButton";
            this.KundenDataExportButton.Size = new System.Drawing.Size(83, 42);
            this.KundenDataExportButton.TabIndex = 13;
            this.KundenDataExportButton.Text = "Export";
            this.KundenDataExportButton.UseVisualStyleBackColor = false;
            this.KundenDataExportButton.Click += new System.EventHandler(this.KundenDataExportButton_Click);
            // 
            // KundenDataImportButton
            // 
            this.KundenDataImportButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.KundenDataImportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KundenDataImportButton.Location = new System.Drawing.Point(632, 13);
            this.KundenDataImportButton.Margin = new System.Windows.Forms.Padding(2);
            this.KundenDataImportButton.Name = "KundenDataImportButton";
            this.KundenDataImportButton.Size = new System.Drawing.Size(83, 42);
            this.KundenDataImportButton.TabIndex = 14;
            this.KundenDataImportButton.Text = "Import";
            this.KundenDataImportButton.UseVisualStyleBackColor = false;
            this.KundenDataImportButton.Click += new System.EventHandler(this.KundenDataImportButton_Click);
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 428);
            this.Controls.Add(this.KundenDataImportButton);
            this.Controls.Add(this.KundenDataExportButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.KundeHinzufuegenButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewKunden);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CustomerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anhänger Management System - Kunden";
            this.Load += new System.EventHandler(this.Kunden_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKunden)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewKunden;
        private System.Windows.Forms.Button KundeHinzufuegenButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Kunden_GarageButton;
        private System.Windows.Forms.Button Kunden_StatistikButton;
        private System.Windows.Forms.Button Kunden_RentalOrdersButton;
        private System.Windows.Forms.Button Kunden_AnhaengerButton;
        private System.Windows.Forms.Button Kunden_KundenButton;
        private System.Windows.Forms.Button Kunden_HomeButton;
        private System.Windows.Forms.Button KundenDataExportButton;
        private System.Windows.Forms.Button KundenDataImportButton;
    }
}
