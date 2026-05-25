namespace Trailer_Rental_Manager.Forms
{
    partial class StatisticsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LeftMenuPanel = new System.Windows.Forms.Panel();
            this.GarageButton = new System.Windows.Forms.Button();
            this.StatistikButton = new System.Windows.Forms.Button();
            this.RentalOrdersButton = new System.Windows.Forms.Button();
            this.AnhaengerButton = new System.Windows.Forms.Button();
            this.KundenButton = new System.Windows.Forms.Button();
            this.HomeButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.AnhaengerStatistik = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewUmsatzAnhaenger = new System.Windows.Forms.DataGridView();
            this.GaragenStatistik = new System.Windows.Forms.TabPage();
            this.chartAnhaengerStatistik = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewMieteGaragen = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxGesamteMiete = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxGesamterUmsatz = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LeftMenuPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.AnhaengerStatistik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUmsatzAnhaenger)).BeginInit();
            this.GaragenStatistik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnhaengerStatistik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMieteGaragen)).BeginInit();
            this.SuspendLayout();
            // 
            // LeftMenuPanel
            // 
            this.LeftMenuPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.LeftMenuPanel.Controls.Add(this.GarageButton);
            this.LeftMenuPanel.Controls.Add(this.StatistikButton);
            this.LeftMenuPanel.Controls.Add(this.RentalOrdersButton);
            this.LeftMenuPanel.Controls.Add(this.AnhaengerButton);
            this.LeftMenuPanel.Controls.Add(this.KundenButton);
            this.LeftMenuPanel.Controls.Add(this.HomeButton);
            this.LeftMenuPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftMenuPanel.Margin = new System.Windows.Forms.Padding(2);
            this.LeftMenuPanel.Name = "LeftMenuPanel";
            this.LeftMenuPanel.Size = new System.Drawing.Size(130, 514);
            this.LeftMenuPanel.TabIndex = 1;
            // 
            // GarageButton
            // 
            this.GarageButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.GarageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GarageButton.Location = new System.Drawing.Point(0, 257);
            this.GarageButton.Margin = new System.Windows.Forms.Padding(2);
            this.GarageButton.Name = "GarageButton";
            this.GarageButton.Size = new System.Drawing.Size(129, 35);
            this.GarageButton.TabIndex = 5;
            this.GarageButton.Text = "Garage";
            this.GarageButton.UseVisualStyleBackColor = false;
            this.GarageButton.Click += new System.EventHandler(this.GarageButton_Click);
            // 
            // StatistikButton
            // 
            this.StatistikButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.StatistikButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatistikButton.Location = new System.Drawing.Point(0, 313);
            this.StatistikButton.Margin = new System.Windows.Forms.Padding(2);
            this.StatistikButton.Name = "StatistikButton";
            this.StatistikButton.Size = new System.Drawing.Size(129, 35);
            this.StatistikButton.TabIndex = 4;
            this.StatistikButton.Text = "Statistik";
            this.StatistikButton.UseVisualStyleBackColor = false;
            // 
            // RentalOrdersButton
            // 
            this.RentalOrdersButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.RentalOrdersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RentalOrdersButton.Location = new System.Drawing.Point(0, 202);
            this.RentalOrdersButton.Margin = new System.Windows.Forms.Padding(2);
            this.RentalOrdersButton.Name = "RentalOrdersButton";
            this.RentalOrdersButton.Size = new System.Drawing.Size(129, 35);
            this.RentalOrdersButton.TabIndex = 3;
            this.RentalOrdersButton.Text = "Aufträge";
            this.RentalOrdersButton.UseVisualStyleBackColor = false;
            this.RentalOrdersButton.Click += new System.EventHandler(this.RentalOrdersButton_Click);
            // 
            // AnhaengerButton
            // 
            this.AnhaengerButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.AnhaengerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnhaengerButton.Location = new System.Drawing.Point(0, 143);
            this.AnhaengerButton.Margin = new System.Windows.Forms.Padding(2);
            this.AnhaengerButton.Name = "AnhaengerButton";
            this.AnhaengerButton.Size = new System.Drawing.Size(129, 35);
            this.AnhaengerButton.TabIndex = 2;
            this.AnhaengerButton.Text = "Anhänger";
            this.AnhaengerButton.UseVisualStyleBackColor = false;
            this.AnhaengerButton.Click += new System.EventHandler(this.AnhaengerButton_Click);
            // 
            // KundenButton
            // 
            this.KundenButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.KundenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KundenButton.Location = new System.Drawing.Point(0, 84);
            this.KundenButton.Margin = new System.Windows.Forms.Padding(2);
            this.KundenButton.Name = "KundenButton";
            this.KundenButton.Size = new System.Drawing.Size(129, 35);
            this.KundenButton.TabIndex = 1;
            this.KundenButton.Text = "Kunden";
            this.KundenButton.UseVisualStyleBackColor = false;
            this.KundenButton.Click += new System.EventHandler(this.KundenButton_Click);
            // 
            // HomeButton
            // 
            this.HomeButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.HomeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HomeButton.Location = new System.Drawing.Point(0, 28);
            this.HomeButton.Margin = new System.Windows.Forms.Padding(2);
            this.HomeButton.Name = "HomeButton";
            this.HomeButton.Size = new System.Drawing.Size(129, 35);
            this.HomeButton.TabIndex = 0;
            this.HomeButton.Text = "Home";
            this.HomeButton.UseVisualStyleBackColor = false;
            this.HomeButton.Click += new System.EventHandler(this.HomeButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.AnhaengerStatistik);
            this.tabControl.Controls.Add(this.GaragenStatistik);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl.Location = new System.Drawing.Point(133, 3);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(692, 511);
            this.tabControl.TabIndex = 2;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // AnhaengerStatistik
            // 
            this.AnhaengerStatistik.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AnhaengerStatistik.Controls.Add(this.label6);
            this.AnhaengerStatistik.Controls.Add(this.textBoxGesamterUmsatz);
            this.AnhaengerStatistik.Controls.Add(this.label7);
            this.AnhaengerStatistik.Controls.Add(this.label1);
            this.AnhaengerStatistik.Controls.Add(this.chartAnhaengerStatistik);
            this.AnhaengerStatistik.Controls.Add(this.label3);
            this.AnhaengerStatistik.Controls.Add(this.dataGridViewUmsatzAnhaenger);
            this.AnhaengerStatistik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnhaengerStatistik.Location = new System.Drawing.Point(4, 34);
            this.AnhaengerStatistik.Name = "AnhaengerStatistik";
            this.AnhaengerStatistik.Padding = new System.Windows.Forms.Padding(3);
            this.AnhaengerStatistik.Size = new System.Drawing.Size(684, 473);
            this.AnhaengerStatistik.TabIndex = 0;
            this.AnhaengerStatistik.Text = "Anhänger";
            this.AnhaengerStatistik.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Umsatz pro Anhänger";
            // 
            // dataGridViewUmsatzAnhaenger
            // 
            this.dataGridViewUmsatzAnhaenger.AllowUserToAddRows = false;
            this.dataGridViewUmsatzAnhaenger.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LightGray;
            this.dataGridViewUmsatzAnhaenger.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewUmsatzAnhaenger.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewUmsatzAnhaenger.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewUmsatzAnhaenger.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUmsatzAnhaenger.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewUmsatzAnhaenger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUmsatzAnhaenger.Location = new System.Drawing.Point(9, 36);
            this.dataGridViewUmsatzAnhaenger.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewUmsatzAnhaenger.Name = "dataGridViewUmsatzAnhaenger";
            this.dataGridViewUmsatzAnhaenger.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUmsatzAnhaenger.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewUmsatzAnhaenger.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewUmsatzAnhaenger.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewUmsatzAnhaenger.RowTemplate.Height = 24;
            this.dataGridViewUmsatzAnhaenger.Size = new System.Drawing.Size(664, 127);
            this.dataGridViewUmsatzAnhaenger.TabIndex = 9;
            // 
            // GaragenStatistik
            // 
            this.GaragenStatistik.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GaragenStatistik.Controls.Add(this.label5);
            this.GaragenStatistik.Controls.Add(this.textBoxGesamteMiete);
            this.GaragenStatistik.Controls.Add(this.label4);
            this.GaragenStatistik.Controls.Add(this.label2);
            this.GaragenStatistik.Controls.Add(this.dataGridViewMieteGaragen);
            this.GaragenStatistik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GaragenStatistik.Location = new System.Drawing.Point(4, 34);
            this.GaragenStatistik.Name = "GaragenStatistik";
            this.GaragenStatistik.Padding = new System.Windows.Forms.Padding(3);
            this.GaragenStatistik.Size = new System.Drawing.Size(684, 473);
            this.GaragenStatistik.TabIndex = 1;
            this.GaragenStatistik.Text = "Garagen";
            this.GaragenStatistik.UseVisualStyleBackColor = true;
            // 
            // chartAnhaengerStatistik
            // 
            this.chartAnhaengerStatistik.BorderlineColor = System.Drawing.Color.Black;
            this.chartAnhaengerStatistik.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "ChartArea1";
            this.chartAnhaengerStatistik.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartAnhaengerStatistik.Legends.Add(legend2);
            this.chartAnhaengerStatistik.Location = new System.Drawing.Point(9, 241);
            this.chartAnhaengerStatistik.Name = "chartAnhaengerStatistik";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Umsatz";
            this.chartAnhaengerStatistik.Series.Add(series2);
            this.chartAnhaengerStatistik.Size = new System.Drawing.Size(664, 223);
            this.chartAnhaengerStatistik.TabIndex = 10;
            this.chartAnhaengerStatistik.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 216);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Diagramm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Miete pro Garage";
            // 
            // dataGridViewMieteGaragen
            // 
            this.dataGridViewMieteGaragen.AllowUserToAddRows = false;
            this.dataGridViewMieteGaragen.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.LightGray;
            this.dataGridViewMieteGaragen.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewMieteGaragen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewMieteGaragen.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewMieteGaragen.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMieteGaragen.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewMieteGaragen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMieteGaragen.Location = new System.Drawing.Point(11, 37);
            this.dataGridViewMieteGaragen.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewMieteGaragen.Name = "dataGridViewMieteGaragen";
            this.dataGridViewMieteGaragen.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMieteGaragen.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewMieteGaragen.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewMieteGaragen.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewMieteGaragen.RowTemplate.Height = 24;
            this.dataGridViewMieteGaragen.Size = new System.Drawing.Size(664, 127);
            this.dataGridViewMieteGaragen.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 193);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Gesamte Miete";
            // 
            // textBoxGesamteMiete
            // 
            this.textBoxGesamteMiete.BackColor = System.Drawing.Color.White;
            this.textBoxGesamteMiete.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxGesamteMiete.Enabled = false;
            this.textBoxGesamteMiete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGesamteMiete.Location = new System.Drawing.Point(155, 193);
            this.textBoxGesamteMiete.Name = "textBoxGesamteMiete";
            this.textBoxGesamteMiete.Size = new System.Drawing.Size(130, 19);
            this.textBoxGesamteMiete.TabIndex = 14;
            this.textBoxGesamteMiete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(301, 193);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "€";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(315, 179);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "€";
            // 
            // textBoxGesamterUmsatz
            // 
            this.textBoxGesamterUmsatz.BackColor = System.Drawing.Color.White;
            this.textBoxGesamterUmsatz.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxGesamterUmsatz.Enabled = false;
            this.textBoxGesamterUmsatz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGesamterUmsatz.Location = new System.Drawing.Point(169, 179);
            this.textBoxGesamterUmsatz.Name = "textBoxGesamterUmsatz";
            this.textBoxGesamterUmsatz.Size = new System.Drawing.Size(130, 19);
            this.textBoxGesamterUmsatz.TabIndex = 17;
            this.textBoxGesamterUmsatz.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 179);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Gesamter Umsatz";
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 514);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.LeftMenuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anhänger Management System - Statistik";
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            this.LeftMenuPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.AnhaengerStatistik.ResumeLayout(false);
            this.AnhaengerStatistik.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUmsatzAnhaenger)).EndInit();
            this.GaragenStatistik.ResumeLayout(false);
            this.GaragenStatistik.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnhaengerStatistik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMieteGaragen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LeftMenuPanel;
        private System.Windows.Forms.Button GarageButton;
        private System.Windows.Forms.Button StatistikButton;
        private System.Windows.Forms.Button RentalOrdersButton;
        private System.Windows.Forms.Button AnhaengerButton;
        private System.Windows.Forms.Button KundenButton;
        private System.Windows.Forms.Button HomeButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage AnhaengerStatistik;
        private System.Windows.Forms.TabPage GaragenStatistik;
        private System.Windows.Forms.DataGridView dataGridViewUmsatzAnhaenger;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAnhaengerStatistik;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewMieteGaragen;
        private System.Windows.Forms.TextBox textBoxGesamteMiete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxGesamterUmsatz;
        private System.Windows.Forms.Label label7;
    }
}