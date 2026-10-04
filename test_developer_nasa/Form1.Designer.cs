namespace test_developer_nasa
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            BTNCleanDB = new Button();
            tableLayoutPanel6 = new TableLayoutPanel();
            LBInfoAvvicinamenti = new Label();
            panel1 = new Panel();
            approachLabel = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            PieHazard = new ScottPlot.WinForms.FormsPlot();
            UpdateLabel = new Label();
            PieSentry = new ScottPlot.WinForms.FormsPlot();
            DGVCloseAp = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnGetCloseApproach = new Button();
            CBForceUpdate = new CheckBox();
            LBtitologiorno = new Label();
            labelDataFinale = new Label();
            labelDataIniziale = new Label();
            DatePicI = new DateTimePicker();
            DatePicF = new DateTimePicker();
            tabPage2 = new TabPage();
            LBOrbitingBodyPie = new Label();
            PieOrbitingBody = new ScottPlot.WinForms.FormsPlot();
            LBOrbitalClass = new Label();
            PGOrbitClass = new PropertyGrid();
            LBOrbitalData = new Label();
            PGOrbitalData = new PropertyGrid();
            LBAsteroidDownload = new Label();
            tableLayoutPanel8 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            TBHazardous = new TextBox();
            LBNeoReference = new Label();
            TBNeoRefernce = new TextBox();
            LBNasaUrl = new Label();
            LBHazard = new Label();
            LBSentry = new Label();
            TBSentry = new TextBox();
            TBNasaUrl = new RichTextBox();
            LBDiameter = new Label();
            tableLayoutPanel9 = new TableLayoutPanel();
            LBDiameterMin = new Label();
            LBDiameterMax = new Label();
            TBDiameterMin = new TextBox();
            TBDiameterMax = new TextBox();
            tableLayoutPanel10 = new TableLayoutPanel();
            LBMagnitude = new Label();
            TBMagnitude = new TextBox();
            DGVAsteroidClose = new DataGridView();
            CheckNuovoAsteroide = new CheckBox();
            TCAsteroidMode = new test_developer_nasa.CustomObject.CustomTabControl();
            tabPage3 = new TabPage();
            tableLayoutPanel4 = new TableLayoutPanel();
            CBAsteroidi = new ComboBox();
            LBCBAsteroide = new Label();
            tabPage4 = new TabPage();
            tableLayoutPanel5 = new TableLayoutPanel();
            TBNewAsteroid = new TextBox();
            BTNewAsteroide = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGVCloseAp).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGVAsteroidClose).BeginInit();
            TCAsteroidMode.SuspendLayout();
            tabPage3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tabPage4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1564, 743);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(BTNCleanDB);
            tabPage1.Controls.Add(tableLayoutPanel6);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(tableLayoutPanel3);
            tabPage1.Controls.Add(DGVCloseAp);
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1556, 710);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Visualizza Avvicinamenti";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // BTNCleanDB
            // 
            BTNCleanDB.Location = new Point(326, 309);
            BTNCleanDB.Name = "BTNCleanDB";
            BTNCleanDB.Size = new Size(229, 49);
            BTNCleanDB.TabIndex = 9;
            BTNCleanDB.Text = "Svuota Databse Interno";
            BTNCleanDB.UseVisualStyleBackColor = true;
            BTNCleanDB.Click += BTNCleanDB_Click;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Controls.Add(LBInfoAvvicinamenti, 0, 0);
            tableLayoutPanel6.Location = new Point(321, 112);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(230, 82);
            tableLayoutPanel6.TabIndex = 8;
            // 
            // LBInfoAvvicinamenti
            // 
            LBInfoAvvicinamenti.AutoSize = true;
            LBInfoAvvicinamenti.Location = new Point(3, 0);
            LBInfoAvvicinamenti.Name = "LBInfoAvvicinamenti";
            LBInfoAvvicinamenti.Size = new Size(223, 80);
            LBInfoAvvicinamenti.TabIndex = 7;
            LBInfoAvvicinamenti.Text = "Esegui doppio click su un avvicinamento per visualizazare le informazione dell'asteroide in dettagio";
            LBInfoAvvicinamenti.TextAlign = ContentAlignment.MiddleCenter;
            LBInfoAvvicinamenti.Visible = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(approachLabel);
            panel1.Location = new Point(3, 295);
            panel1.Name = "panel1";
            panel1.Size = new Size(315, 63);
            panel1.TabIndex = 6;
            // 
            // approachLabel
            // 
            approachLabel.AutoSize = true;
            approachLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            approachLabel.Location = new Point(0, 0);
            approachLabel.Name = "approachLabel";
            approachLabel.Size = new Size(126, 28);
            approachLabel.TabIndex = 3;
            approachLabel.Text = "tot approach";
            approachLabel.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(PieHazard, 0, 0);
            tableLayoutPanel3.Controls.Add(UpdateLabel, 0, 1);
            tableLayoutPanel3.Controls.Add(PieSentry, 1, 0);
            tableLayoutPanel3.Location = new Point(8, 364);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 83.732254F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 16.26775F));
            tableLayoutPanel3.Size = new Size(547, 341);
            tableLayoutPanel3.TabIndex = 5;
            // 
            // PieHazard
            // 
            PieHazard.Anchor = AnchorStyles.None;
            PieHazard.ImeMode = ImeMode.NoControl;
            PieHazard.Location = new Point(3, 16);
            PieHazard.Name = "PieHazard";
            PieHazard.Size = new Size(267, 252);
            PieHazard.TabIndex = 4;
            PieHazard.Visible = false;
            // 
            // UpdateLabel
            // 
            UpdateLabel.Anchor = AnchorStyles.Left;
            UpdateLabel.AutoSize = true;
            UpdateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UpdateLabel.Location = new Point(3, 299);
            UpdateLabel.Name = "UpdateLabel";
            UpdateLabel.Size = new Size(74, 28);
            UpdateLabel.TabIndex = 2;
            UpdateLabel.Text = "update";
            UpdateLabel.Visible = false;
            // 
            // PieSentry
            // 
            PieSentry.Anchor = AnchorStyles.None;
            PieSentry.ImeMode = ImeMode.NoControl;
            PieSentry.Location = new Point(276, 16);
            PieSentry.Name = "PieSentry";
            PieSentry.Size = new Size(267, 252);
            PieSentry.TabIndex = 5;
            PieSentry.Visible = false;
            // 
            // DGVCloseAp
            // 
            DGVCloseAp.AllowUserToAddRows = false;
            DGVCloseAp.AllowUserToDeleteRows = false;
            DGVCloseAp.AllowUserToOrderColumns = true;
            DGVCloseAp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGVCloseAp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVCloseAp.Location = new Point(561, 3);
            DGVCloseAp.Name = "DGVCloseAp";
            DGVCloseAp.ReadOnly = true;
            DGVCloseAp.RowHeadersWidth = 51;
            DGVCloseAp.Size = new Size(992, 702);
            DGVCloseAp.TabIndex = 1;
            DGVCloseAp.CellMouseDoubleClick += DGVCloseAp_CellMouseDoubleClick;
            DGVCloseAp.ColumnHeaderMouseClick += DGVCloseAp_ColumnHeaderMouseClick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 5);
            tableLayoutPanel1.Controls.Add(LBtitologiorno, 0, 0);
            tableLayoutPanel1.Controls.Add(labelDataFinale, 0, 3);
            tableLayoutPanel1.Controls.Add(labelDataIniziale, 0, 1);
            tableLayoutPanel1.Controls.Add(DatePicI, 0, 2);
            tableLayoutPanel1.Controls.Add(DatePicF, 0, 4);
            tableLayoutPanel1.Location = new Point(5, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 31.8493156F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.6712332F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.6164379F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.7808228F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.3287668F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15.7534246F));
            tableLayoutPanel1.Size = new Size(313, 292);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.9283371F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.0716629F));
            tableLayoutPanel2.Controls.Add(btnGetCloseApproach, 0, 0);
            tableLayoutPanel2.Controls.Add(CBForceUpdate, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 248);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(307, 41);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // btnGetCloseApproach
            // 
            btnGetCloseApproach.Anchor = AnchorStyles.Top;
            btnGetCloseApproach.Location = new Point(23, 3);
            btnGetCloseApproach.Name = "btnGetCloseApproach";
            btnGetCloseApproach.Size = new Size(94, 27);
            btnGetCloseApproach.TabIndex = 5;
            btnGetCloseApproach.Text = "Invio";
            btnGetCloseApproach.UseVisualStyleBackColor = true;
            btnGetCloseApproach.Click += btnGetCloseApproach_Click;
            // 
            // CBForceUpdate
            // 
            CBForceUpdate.Anchor = AnchorStyles.None;
            CBForceUpdate.AutoSize = true;
            CBForceUpdate.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CBForceUpdate.Location = new Point(145, 10);
            CBForceUpdate.Name = "CBForceUpdate";
            CBForceUpdate.Size = new Size(157, 21);
            CBForceUpdate.TabIndex = 5;
            CBForceUpdate.Text = "Forza Aggiornamento";
            CBForceUpdate.UseVisualStyleBackColor = true;
            // 
            // LBtitologiorno
            // 
            LBtitologiorno.AutoSize = true;
            LBtitologiorno.Location = new Point(3, 3);
            LBtitologiorno.Margin = new Padding(3);
            LBtitologiorno.Name = "LBtitologiorno";
            LBtitologiorno.Size = new Size(304, 80);
            LBtitologiorno.TabIndex = 0;
            LBtitologiorno.Text = "Seleziona una data di inizio e una di fine per visualizzare tutti i passaggi di asteroidi vicino alla Terra in quel periodo \r\n(max 7 giorni di distanza)";
            LBtitologiorno.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelDataFinale
            // 
            labelDataFinale.Anchor = AnchorStyles.None;
            labelDataFinale.AutoSize = true;
            labelDataFinale.Location = new Point(115, 174);
            labelDataFinale.Name = "labelDataFinale";
            labelDataFinale.Size = new Size(82, 20);
            labelDataFinale.TabIndex = 7;
            labelDataFinale.Text = "Data finale";
            labelDataFinale.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelDataIniziale
            // 
            labelDataIniziale.Anchor = AnchorStyles.None;
            labelDataIniziale.AutoSize = true;
            labelDataIniziale.Location = new Point(110, 101);
            labelDataIniziale.Name = "labelDataIniziale";
            labelDataIniziale.Size = new Size(92, 20);
            labelDataIniziale.TabIndex = 6;
            labelDataIniziale.Text = "Data Iniziale";
            labelDataIniziale.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DatePicI
            // 
            DatePicI.Dock = DockStyle.Fill;
            DatePicI.Location = new Point(3, 133);
            DatePicI.Name = "DatePicI";
            DatePicI.Size = new Size(307, 27);
            DatePicI.TabIndex = 8;
            DatePicI.Value = new DateTime(2026, 10, 1, 0, 0, 0, 0);
            DatePicI.ValueChanged += DatePicI_ValueChanged;
            // 
            // DatePicF
            // 
            DatePicF.Dock = DockStyle.Fill;
            DatePicF.Location = new Point(3, 212);
            DatePicF.Name = "DatePicF";
            DatePicF.Size = new Size(307, 27);
            DatePicF.TabIndex = 9;
            DatePicF.Value = new DateTime(2026, 10, 1, 0, 0, 0, 0);
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(LBOrbitingBodyPie);
            tabPage2.Controls.Add(PieOrbitingBody);
            tabPage2.Controls.Add(LBOrbitalClass);
            tabPage2.Controls.Add(PGOrbitClass);
            tabPage2.Controls.Add(LBOrbitalData);
            tabPage2.Controls.Add(PGOrbitalData);
            tabPage2.Controls.Add(LBAsteroidDownload);
            tabPage2.Controls.Add(tableLayoutPanel8);
            tabPage2.Controls.Add(DGVAsteroidClose);
            tabPage2.Controls.Add(CheckNuovoAsteroide);
            tabPage2.Controls.Add(TCAsteroidMode);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1556, 710);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Gestione Asteroidi";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // LBOrbitingBodyPie
            // 
            LBOrbitingBodyPie.AutoSize = true;
            LBOrbitingBodyPie.Location = new Point(1047, 332);
            LBOrbitingBodyPie.Name = "LBOrbitingBodyPie";
            LBOrbitingBodyPie.Size = new Size(184, 20);
            LBOrbitingBodyPie.TabIndex = 14;
            LBOrbitingBodyPie.Text = "Orbiting Body Distribution";
            // 
            // PieOrbitingBody
            // 
            PieOrbitingBody.Location = new Point(1036, 345);
            PieOrbitingBody.Name = "PieOrbitingBody";
            PieOrbitingBody.Size = new Size(194, 169);
            PieOrbitingBody.TabIndex = 13;
            // 
            // LBOrbitalClass
            // 
            LBOrbitalClass.AutoSize = true;
            LBOrbitalClass.Location = new Point(1096, 517);
            LBOrbitalClass.Name = "LBOrbitalClass";
            LBOrbitalClass.Size = new Size(92, 20);
            LBOrbitalClass.TabIndex = 12;
            LBOrbitalClass.Text = "Orbital Class";
            // 
            // PGOrbitClass
            // 
            PGOrbitClass.BackColor = SystemColors.Control;
            PGOrbitClass.HelpVisible = false;
            PGOrbitClass.Location = new Point(1003, 540);
            PGOrbitClass.Name = "PGOrbitClass";
            PGOrbitClass.Size = new Size(271, 162);
            PGOrbitClass.TabIndex = 11;
            PGOrbitClass.ToolbarVisible = false;
            // 
            // LBOrbitalData
            // 
            LBOrbitalData.AutoSize = true;
            LBOrbitalData.Location = new Point(1352, 335);
            LBOrbitalData.Name = "LBOrbitalData";
            LBOrbitalData.Size = new Size(151, 20);
            LBOrbitalData.TabIndex = 10;
            LBOrbitalData.Text = "Orbital Asteroid Data";
            // 
            // PGOrbitalData
            // 
            PGOrbitalData.BackColor = SystemColors.Control;
            PGOrbitalData.HelpVisible = false;
            PGOrbitalData.Location = new Point(1280, 360);
            PGOrbitalData.Name = "PGOrbitalData";
            PGOrbitalData.PropertySort = PropertySort.NoSort;
            PGOrbitalData.Size = new Size(266, 342);
            PGOrbitalData.TabIndex = 9;
            PGOrbitalData.ToolbarVisible = false;
            // 
            // LBAsteroidDownload
            // 
            LBAsteroidDownload.AutoSize = true;
            LBAsteroidDownload.Location = new Point(1005, 70);
            LBAsteroidDownload.Name = "LBAsteroidDownload";
            LBAsteroidDownload.Size = new Size(155, 20);
            LBAsteroidDownload.TabIndex = 8;
            LBAsteroidDownload.Text = "Aggiornamento Dati...";
            LBAsteroidDownload.Visible = false;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel8.Controls.Add(tableLayoutPanel7, 0, 0);
            tableLayoutPanel8.Controls.Add(LBDiameter, 0, 1);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel9, 0, 2);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel10, 0, 3);
            tableLayoutPanel8.Location = new Point(1000, 106);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 4;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 109F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel8.Size = new Size(548, 226);
            tableLayoutPanel8.TabIndex = 7;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Controls.Add(TBHazardous, 0, 3);
            tableLayoutPanel7.Controls.Add(LBNeoReference, 0, 0);
            tableLayoutPanel7.Controls.Add(TBNeoRefernce, 0, 1);
            tableLayoutPanel7.Controls.Add(LBNasaUrl, 1, 0);
            tableLayoutPanel7.Controls.Add(LBHazard, 0, 2);
            tableLayoutPanel7.Controls.Add(LBSentry, 1, 2);
            tableLayoutPanel7.Controls.Add(TBSentry, 1, 3);
            tableLayoutPanel7.Controls.Add(TBNasaUrl, 1, 1);
            tableLayoutPanel7.Location = new Point(3, 3);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 4;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel7.Size = new Size(540, 103);
            tableLayoutPanel7.TabIndex = 6;
            // 
            // TBHazardous
            // 
            TBHazardous.Location = new Point(3, 73);
            TBHazardous.Name = "TBHazardous";
            TBHazardous.ReadOnly = true;
            TBHazardous.Size = new Size(264, 27);
            TBHazardous.TabIndex = 7;
            // 
            // LBNeoReference
            // 
            LBNeoReference.Anchor = AnchorStyles.Top;
            LBNeoReference.AutoSize = true;
            LBNeoReference.Location = new Point(73, 0);
            LBNeoReference.Name = "LBNeoReference";
            LBNeoReference.Size = new Size(124, 20);
            LBNeoReference.TabIndex = 0;
            LBNeoReference.Text = "Neo Reference Id";
            // 
            // TBNeoRefernce
            // 
            TBNeoRefernce.Location = new Point(3, 23);
            TBNeoRefernce.Name = "TBNeoRefernce";
            TBNeoRefernce.ReadOnly = true;
            TBNeoRefernce.Size = new Size(264, 27);
            TBNeoRefernce.TabIndex = 1;
            // 
            // LBNasaUrl
            // 
            LBNasaUrl.Anchor = AnchorStyles.Top;
            LBNasaUrl.AutoSize = true;
            LBNasaUrl.Location = new Point(361, 0);
            LBNasaUrl.Name = "LBNasaUrl";
            LBNasaUrl.Size = new Size(87, 20);
            LBNasaUrl.TabIndex = 3;
            LBNasaUrl.Text = "Nasa Jpl Url";
            // 
            // LBHazard
            // 
            LBHazard.Anchor = AnchorStyles.Top;
            LBHazard.AutoSize = true;
            LBHazard.Location = new Point(51, 51);
            LBHazard.Name = "LBHazard";
            LBHazard.Size = new Size(167, 19);
            LBHazard.TabIndex = 4;
            LBHazard.Text = "Is Potentially Hazardous";
            // 
            // LBSentry
            // 
            LBSentry.Anchor = AnchorStyles.Top;
            LBSentry.AutoSize = true;
            LBSentry.Location = new Point(349, 51);
            LBSentry.Name = "LBSentry";
            LBSentry.Size = new Size(112, 19);
            LBSentry.TabIndex = 5;
            LBSentry.Text = "Is Sentry Object";
            // 
            // TBSentry
            // 
            TBSentry.Location = new Point(273, 73);
            TBSentry.Name = "TBSentry";
            TBSentry.ReadOnly = true;
            TBSentry.Size = new Size(264, 27);
            TBSentry.TabIndex = 6;
            // 
            // TBNasaUrl
            // 
            TBNasaUrl.BorderStyle = BorderStyle.None;
            TBNasaUrl.Location = new Point(273, 23);
            TBNasaUrl.Name = "TBNasaUrl";
            TBNasaUrl.ReadOnly = true;
            TBNasaUrl.Size = new Size(264, 25);
            TBNasaUrl.TabIndex = 8;
            TBNasaUrl.Text = "";
            TBNasaUrl.LinkClicked += TBNasaUrl_LinkClicked;
            // 
            // LBDiameter
            // 
            LBDiameter.Anchor = AnchorStyles.Top;
            LBDiameter.AutoSize = true;
            LBDiameter.Location = new Point(187, 109);
            LBDiameter.Name = "LBDiameter";
            LBDiameter.Size = new Size(175, 20);
            LBDiameter.TabIndex = 7;
            LBDiameter.Text = "Estimated Diameter (km)";
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.ColumnCount = 2;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Controls.Add(LBDiameterMin, 0, 0);
            tableLayoutPanel9.Controls.Add(LBDiameterMax, 1, 0);
            tableLayoutPanel9.Controls.Add(TBDiameterMin, 0, 1);
            tableLayoutPanel9.Controls.Add(TBDiameterMax, 1, 1);
            tableLayoutPanel9.Location = new Point(3, 136);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 2;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableLayoutPanel9.Size = new Size(544, 50);
            tableLayoutPanel9.TabIndex = 8;
            // 
            // LBDiameterMin
            // 
            LBDiameterMin.Anchor = AnchorStyles.Top;
            LBDiameterMin.AutoSize = true;
            LBDiameterMin.Location = new Point(119, 0);
            LBDiameterMin.Name = "LBDiameterMin";
            LBDiameterMin.Size = new Size(34, 20);
            LBDiameterMin.TabIndex = 0;
            LBDiameterMin.Text = "Min";
            // 
            // LBDiameterMax
            // 
            LBDiameterMax.Anchor = AnchorStyles.Top;
            LBDiameterMax.AutoSize = true;
            LBDiameterMax.Location = new Point(389, 0);
            LBDiameterMax.Name = "LBDiameterMax";
            LBDiameterMax.Size = new Size(37, 20);
            LBDiameterMax.TabIndex = 1;
            LBDiameterMax.Text = "Max";
            // 
            // TBDiameterMin
            // 
            TBDiameterMin.Location = new Point(3, 23);
            TBDiameterMin.Name = "TBDiameterMin";
            TBDiameterMin.ReadOnly = true;
            TBDiameterMin.Size = new Size(266, 27);
            TBDiameterMin.TabIndex = 2;
            // 
            // TBDiameterMax
            // 
            TBDiameterMax.Location = new Point(275, 23);
            TBDiameterMax.Name = "TBDiameterMax";
            TBDiameterMax.ReadOnly = true;
            TBDiameterMax.Size = new Size(265, 27);
            TBDiameterMax.TabIndex = 3;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.ColumnCount = 2;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.2772636F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.7227364F));
            tableLayoutPanel10.Controls.Add(LBMagnitude, 0, 0);
            tableLayoutPanel10.Controls.Add(TBMagnitude, 1, 0);
            tableLayoutPanel10.Location = new Point(3, 192);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel10.Size = new Size(541, 31);
            tableLayoutPanel10.TabIndex = 9;
            // 
            // LBMagnitude
            // 
            LBMagnitude.Anchor = AnchorStyles.Left;
            LBMagnitude.AutoSize = true;
            LBMagnitude.Location = new Point(3, 5);
            LBMagnitude.Name = "LBMagnitude";
            LBMagnitude.Size = new Size(159, 20);
            LBMagnitude.TabIndex = 0;
            LBMagnitude.Text = "Absolute Magnitude H";
            // 
            // TBMagnitude
            // 
            TBMagnitude.Location = new Point(275, 3);
            TBMagnitude.Name = "TBMagnitude";
            TBMagnitude.ReadOnly = true;
            TBMagnitude.Size = new Size(263, 27);
            TBMagnitude.TabIndex = 3;
            // 
            // DGVAsteroidClose
            // 
            DGVAsteroidClose.AllowUserToAddRows = false;
            DGVAsteroidClose.AllowUserToDeleteRows = false;
            DGVAsteroidClose.AllowUserToOrderColumns = true;
            DGVAsteroidClose.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGVAsteroidClose.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVAsteroidClose.Location = new Point(2, 3);
            DGVAsteroidClose.Name = "DGVAsteroidClose";
            DGVAsteroidClose.ReadOnly = true;
            DGVAsteroidClose.RowHeadersWidth = 51;
            DGVAsteroidClose.Size = new Size(992, 702);
            DGVAsteroidClose.TabIndex = 5;
            DGVAsteroidClose.ColumnHeaderMouseClick += DGVAsteroidClose_ColumnHeaderMouseClick;
            // 
            // CheckNuovoAsteroide
            // 
            CheckNuovoAsteroide.AutoSize = true;
            CheckNuovoAsteroide.Location = new Point(1074, 6);
            CheckNuovoAsteroide.Name = "CheckNuovoAsteroide";
            CheckNuovoAsteroide.Size = new Size(237, 24);
            CheckNuovoAsteroide.TabIndex = 4;
            CheckNuovoAsteroide.Text = "Inserisci Nuovo Asteroide da id";
            CheckNuovoAsteroide.UseVisualStyleBackColor = true;
            CheckNuovoAsteroide.CheckedChanged += CheckNuovoAsteroide_CheckedChanged;
            // 
            // TCAsteroidMode
            // 
            TCAsteroidMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TCAsteroidMode.Controls.Add(tabPage3);
            TCAsteroidMode.Controls.Add(tabPage4);
            TCAsteroidMode.Location = new Point(997, 33);
            TCAsteroidMode.Margin = new Padding(0);
            TCAsteroidMode.Name = "TCAsteroidMode";
            TCAsteroidMode.SelectedIndex = 0;
            TCAsteroidMode.Size = new Size(551, 70);
            TCAsteroidMode.TabIndex = 3;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(tableLayoutPanel4);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(543, 37);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.70642F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.29358F));
            tableLayoutPanel4.Controls.Add(CBAsteroidi, 0, 0);
            tableLayoutPanel4.Controls.Add(LBCBAsteroide, 1, 0);
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 32F));
            tableLayoutPanel4.Size = new Size(545, 34);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // CBAsteroidi
            // 
            CBAsteroidi.DropDownStyle = ComboBoxStyle.DropDownList;
            CBAsteroidi.FormattingEnabled = true;
            CBAsteroidi.Location = new Point(3, 3);
            CBAsteroidi.Name = "CBAsteroidi";
            CBAsteroidi.Size = new Size(363, 28);
            CBAsteroidi.TabIndex = 0;
            CBAsteroidi.SelectedIndexChanged += CBAsteroidi_SelectedIndexChanged;
            CBAsteroidi.Format += CBAsteroidi_Format;
            // 
            // LBCBAsteroide
            // 
            LBCBAsteroide.Anchor = AnchorStyles.Left;
            LBCBAsteroide.AutoSize = true;
            LBCBAsteroide.Location = new Point(372, 7);
            LBCBAsteroide.Name = "LBCBAsteroide";
            LBCBAsteroide.Size = new Size(159, 20);
            LBCBAsteroide.TabIndex = 1;
            LBCBAsteroide.Text = "Seleziona un asteroide";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(tableLayoutPanel5);
            tabPage4.Location = new Point(4, 29);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(543, 37);
            tabPage4.TabIndex = 1;
            tabPage4.Text = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.70642F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.29358F));
            tableLayoutPanel5.Controls.Add(TBNewAsteroid, 0, 0);
            tableLayoutPanel5.Controls.Add(BTNewAsteroide, 1, 0);
            tableLayoutPanel5.Location = new Point(0, 0);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 32F));
            tableLayoutPanel5.Size = new Size(545, 34);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // TBNewAsteroid
            // 
            TBNewAsteroid.Location = new Point(3, 3);
            TBNewAsteroid.Name = "TBNewAsteroid";
            TBNewAsteroid.Size = new Size(363, 27);
            TBNewAsteroid.TabIndex = 2;
            // 
            // BTNewAsteroide
            // 
            BTNewAsteroide.Location = new Point(372, 3);
            BTNewAsteroide.Name = "BTNewAsteroide";
            BTNewAsteroide.Size = new Size(170, 28);
            BTNewAsteroide.TabIndex = 3;
            BTNewAsteroide.Text = "Aggiungi asteroide";
            BTNewAsteroide.UseVisualStyleBackColor = true;
            BTNewAsteroide.Click += BTNewAsteroide_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1564, 743);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Nasa Asteroid API";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGVCloseAp).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            tableLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGVAsteroidClose).EndInit();
            TCAsteroidMode.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tabPage4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TableLayoutPanel tableLayoutPanel1;
        private TabPage tabPage2;
        private Label LBtitologiorno;
        private Button btnGetCloseApproach;
        private Label labelDataIniziale;
        private Label labelDataFinale;
        private DateTimePicker DatePicI;
        private DateTimePicker DatePicF;
        private DataGridView DGVCloseAp;
        private Label UpdateLabel;
        private Label approachLabel;
        private ScottPlot.WinForms.FormsPlot PieHazard;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox CBForceUpdate;
        private TableLayoutPanel tableLayoutPanel3;
        private ComboBox CBAsteroidi;
        private TableLayoutPanel tableLayoutPanel4;
        private Label LBCBAsteroide;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox TBNewAsteroid;
        private Button BTNewAsteroide;
        private CustomObject.CustomTabControl TCAsteroidMode;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private CheckBox CheckNuovoAsteroide;
        private DataGridView DGVAsteroidClose;
        private ScottPlot.WinForms.FormsPlot PieSentry;
        private Panel panel1;
        private Label LBInfoAvvicinamenti;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private Label LBNeoReference;
        private TextBox TBNeoRefernce;
        private Label LBHazard;
        private TableLayoutPanel tableLayoutPanel8;
        private TextBox TBHazardous;
        private Label LBNasaUrl;
        private Label LBSentry;
        private TextBox TBSentry;
        private Label LBDiameter;
        private TableLayoutPanel tableLayoutPanel9;
        private Label LBDiameterMin;
        private Label LBDiameterMax;
        private TextBox TBDiameterMin;
        private TextBox TBDiameterMax;
        private RichTextBox TBNasaUrl;
        private TableLayoutPanel tableLayoutPanel10;
        private Label LBMagnitude;
        private TextBox TBMagnitude;
        private Label LBAsteroidDownload;
        private PropertyGrid PGOrbitalData;
        private Label LBOrbitalClass;
        private PropertyGrid PGOrbitClass;
        private Label LBOrbitalData;
        private ScottPlot.WinForms.FormsPlot PieOrbitingBody;
        private Label LBOrbitingBodyPie;
        private Button BTNCleanDB;
    }
}
