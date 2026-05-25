namespace Trailer_Rental_Manager.Forms.RentalOrders
{
    partial class RentalOrderEditForm
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
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.comboBox_Anhaenger = new System.Windows.Forms.ComboBox();
            this.comboBox_Kunden = new System.Windows.Forms.ComboBox();
            this.dateTimePicker_Beginn = new System.Windows.Forms.DateTimePicker();
            this.Preis_TextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rentalOrderIdTextBox = new System.Windows.Forms.TextBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.CustomFormat = " ";
            this.dateTimePicker_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_End.Location = new System.Drawing.Point(290, 211);
           // this.dateTimePicker_End.MaxDate = new System.DateTime(2023, 2, 11, 0, 0, 0, 0);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(204, 20);
            this.dateTimePicker_End.TabIndex = 93;
            //this.dateTimePicker_End.Value = new System.DateTime(2023, 2, 11, 0, 0, 0, 0);
            // 
            // comboBox_Anhaenger
            // 
            this.comboBox_Anhaenger.FormattingEnabled = true;
            this.comboBox_Anhaenger.Location = new System.Drawing.Point(290, 111);
            this.comboBox_Anhaenger.Name = "comboBox_Anhaenger";
            this.comboBox_Anhaenger.Size = new System.Drawing.Size(204, 21);
            this.comboBox_Anhaenger.TabIndex = 92;
            // 
            // comboBox_Kunden
            // 
            this.comboBox_Kunden.FormattingEnabled = true;
            this.comboBox_Kunden.Location = new System.Drawing.Point(290, 64);
            this.comboBox_Kunden.Name = "comboBox_Kunden";
            this.comboBox_Kunden.Size = new System.Drawing.Size(204, 21);
            this.comboBox_Kunden.TabIndex = 91;
            // 
            // dateTimePicker_Beginn
            // 
            this.dateTimePicker_Beginn.CustomFormat = " ";
            this.dateTimePicker_Beginn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_Beginn.Location = new System.Drawing.Point(290, 162);
            //this.dateTimePicker_Beginn.MaxDate = new System.DateTime(2023, 2, 11, 0, 0, 0, 0);
            this.dateTimePicker_Beginn.Name = "dateTimePicker_Beginn";
            this.dateTimePicker_Beginn.Size = new System.Drawing.Size(204, 20);
            this.dateTimePicker_Beginn.TabIndex = 90;
           // this.dateTimePicker_Beginn.Value = new System.DateTime(2023, 2, 11, 0, 0, 0, 0);
            // 
            // Preis_TextBox
            // 
            this.Preis_TextBox.Location = new System.Drawing.Point(290, 258);
            this.Preis_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Preis_TextBox.Name = "Preis_TextBox";
            this.Preis_TextBox.Size = new System.Drawing.Size(204, 20);
            this.Preis_TextBox.TabIndex = 88;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 258);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 20);
            this.label6.TabIndex = 87;
            this.label6.Text = "Preis*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 212);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 86;
            this.label5.Text = "Enddatum*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 162);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 20);
            this.label4.TabIndex = 85;
            this.label4.Text = "Beginndatum*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 84;
            this.label2.Text = "Anhänger*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 83;
            this.label1.Text = "Kunde*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.TabIndex = 94;
            this.label3.Text = "Auftragsnummer";
            // 
            // rentalOrderIdTextBox
            // 
            this.rentalOrderIdTextBox.Enabled = false;
            this.rentalOrderIdTextBox.Location = new System.Drawing.Point(290, 20);
            this.rentalOrderIdTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.rentalOrderIdTextBox.Name = "rentalOrderIdTextBox";
            this.rentalOrderIdTextBox.Size = new System.Drawing.Size(204, 20);
            this.rentalOrderIdTextBox.TabIndex = 95;
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.Location = new System.Drawing.Point(278, 316);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(111, 35);
            this.DeleteButton.TabIndex = 97;
            this.DeleteButton.Text = "Löschen";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(105, 316);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(111, 35);
            this.SaveButton.TabIndex = 96;
            this.SaveButton.Text = "Speichern";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // RentalOrderEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 386);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.rentalOrderIdTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker_End);
            this.Controls.Add(this.comboBox_Anhaenger);
            this.Controls.Add(this.comboBox_Kunden);
            this.Controls.Add(this.dateTimePicker_Beginn);
            this.Controls.Add(this.Preis_TextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RentalOrderEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auftrag - Bearbeitungsübersicht";
            this.Load += new System.EventHandler(this.RentalOrderEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.ComboBox comboBox_Anhaenger;
        private System.Windows.Forms.ComboBox comboBox_Kunden;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Beginn;
        private System.Windows.Forms.TextBox Preis_TextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox rentalOrderIdTextBox;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button SaveButton;
    }
}