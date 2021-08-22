namespace ndp_invest_helper
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_GroupsByCountry = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn_CountryKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn_CountryValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn_CountryDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonBuy = new System.Windows.Forms.Button();
            this.buttonSell = new System.Windows.Forms.Button();
            this.comboBox_BuySell_Currency = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_BuySell_Price = new System.Windows.Forms.NumericUpDown();
            this.comboBox_BuySell_Security = new SergeUtils.EasyCompletionComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_BuySell_Count = new System.Windows.Forms.NumericUpDown();
            this.dataGridView_GroupsByCurrency = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn_CurrencyKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn_CurrencyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn_CurrencyDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_GroupsBySector = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn_SectorKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn_SectorValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn_SectorDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_GroupsByType = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn_TypeKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn_TypeValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn_TypeDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listBox_GroupStocks = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_RunTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_CancelDeal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Log = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl_Right = new System.Windows.Forms.TabControl();
            this.tabPage_GroupContent = new System.Windows.Forms.TabPage();
            this.tabPage_Deals = new System.Windows.Forms.TabPage();
            this.listBox_Deals = new System.Windows.Forms.ListBox();
            this.numericUpDown_BuySell_Total = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupsByCountry)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Price)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Count)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupsByCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupsBySector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupsByType)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl_Right.SuspendLayout();
            this.tabPage_GroupContent.SuspendLayout();
            this.tabPage_Deals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Total)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_GroupsByCountry
            // 
            this.dataGridView_GroupsByCountry.AllowUserToAddRows = false;
            this.dataGridView_GroupsByCountry.AllowUserToDeleteRows = false;
            this.dataGridView_GroupsByCountry.AllowUserToResizeRows = false;
            this.dataGridView_GroupsByCountry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_GroupsByCountry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView_GroupsByCountry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_GroupsByCountry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn_CountryKey,
            this.dataGridViewTextBoxColumn_CountryValue,
            this.dataGridViewTextBoxColumn_CountryDifference});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_GroupsByCountry.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView_GroupsByCountry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_GroupsByCountry.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_GroupsByCountry.Name = "dataGridView_GroupsByCountry";
            this.dataGridView_GroupsByCountry.ReadOnly = true;
            this.dataGridView_GroupsByCountry.RowHeadersVisible = false;
            this.dataGridView_GroupsByCountry.RowHeadersWidth = 51;
            this.dataGridView_GroupsByCountry.RowTemplate.DefaultCellStyle.Format = "N2";
            this.dataGridView_GroupsByCountry.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dataGridView_GroupsByCountry.RowTemplate.Height = 24;
            this.dataGridView_GroupsByCountry.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_GroupsByCountry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_GroupsByCountry.Size = new System.Drawing.Size(364, 319);
            this.dataGridView_GroupsByCountry.TabIndex = 0;
            this.dataGridView_GroupsByCountry.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_RowEnter);
            // 
            // dataGridViewTextBoxColumn_CountryKey
            // 
            this.dataGridViewTextBoxColumn_CountryKey.FillWeight = 50F;
            this.dataGridViewTextBoxColumn_CountryKey.HeaderText = "Страна";
            this.dataGridViewTextBoxColumn_CountryKey.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_CountryKey.Name = "dataGridViewTextBoxColumn_CountryKey";
            this.dataGridViewTextBoxColumn_CountryKey.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn_CountryValue
            // 
            this.dataGridViewTextBoxColumn_CountryValue.FillWeight = 25F;
            this.dataGridViewTextBoxColumn_CountryValue.HeaderText = "Доля";
            this.dataGridViewTextBoxColumn_CountryValue.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_CountryValue.Name = "dataGridViewTextBoxColumn_CountryValue";
            this.dataGridViewTextBoxColumn_CountryValue.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn_CountryDifference
            // 
            this.dataGridViewTextBoxColumn_CountryDifference.FillWeight = 25F;
            this.dataGridViewTextBoxColumn_CountryDifference.HeaderText = "Изменение";
            this.dataGridViewTextBoxColumn_CountryDifference.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_CountryDifference.Name = "dataGridViewTextBoxColumn_CountryDifference";
            this.dataGridViewTextBoxColumn_CountryDifference.ReadOnly = true;
            // 
            // buttonBuy
            // 
            this.buttonBuy.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBuy.Location = new System.Drawing.Point(6, 21);
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.Size = new System.Drawing.Size(120, 40);
            this.buttonBuy.TabIndex = 2;
            this.buttonBuy.Text = "Купить";
            this.buttonBuy.UseVisualStyleBackColor = true;
            this.buttonBuy.Click += new System.EventHandler(this.buttonBuySell_Click);
            // 
            // buttonSell
            // 
            this.buttonSell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSell.Location = new System.Drawing.Point(6, 81);
            this.buttonSell.Name = "buttonSell";
            this.buttonSell.Size = new System.Drawing.Size(120, 40);
            this.buttonSell.TabIndex = 3;
            this.buttonSell.Text = "Продать";
            this.buttonSell.UseVisualStyleBackColor = true;
            this.buttonSell.Click += new System.EventHandler(this.buttonBuySell_Click);
            // 
            // comboBox_BuySell_Currency
            // 
            this.comboBox_BuySell_Currency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BuySell_Currency.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_BuySell_Currency.FormattingEnabled = true;
            this.comboBox_BuySell_Currency.Location = new System.Drawing.Point(605, 80);
            this.comboBox_BuySell_Currency.Name = "comboBox_BuySell_Currency";
            this.comboBox_BuySell_Currency.Size = new System.Drawing.Size(103, 32);
            this.comboBox_BuySell_Currency.TabIndex = 6;
            this.comboBox_BuySell_Currency.SelectedIndexChanged += new System.EventHandler(this.comboBox_BuySell_Currency_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericUpDown_BuySell_Total);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown_BuySell_Price);
            this.groupBox1.Controls.Add(this.comboBox_BuySell_Security);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDown_BuySell_Count);
            this.groupBox1.Controls.Add(this.buttonSell);
            this.groupBox1.Controls.Add(this.comboBox_BuySell_Currency);
            this.groupBox1.Controls.Add(this.buttonBuy);
            this.groupBox1.Location = new System.Drawing.Point(8, 683);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(998, 134);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(715, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "на сумму";
            // 
            // numericUpDown_BuySell_Price
            // 
            this.numericUpDown_BuySell_Price.DecimalPlaces = 6;
            this.numericUpDown_BuySell_Price.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_BuySell_Price.Location = new System.Drawing.Point(449, 81);
            this.numericUpDown_BuySell_Price.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_BuySell_Price.Name = "numericUpDown_BuySell_Price";
            this.numericUpDown_BuySell_Price.Size = new System.Drawing.Size(150, 30);
            this.numericUpDown_BuySell_Price.TabIndex = 9;
            this.numericUpDown_BuySell_Price.ThousandsSeparator = true;
            this.numericUpDown_BuySell_Price.ValueChanged += new System.EventHandler(this.numericUpDown_BuySell_Count_ValueChanged);
            // 
            // comboBox_BuySell_Security
            // 
            this.comboBox_BuySell_Security.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_BuySell_Security.DisplayMember = "FullName";
            this.comboBox_BuySell_Security.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_BuySell_Security.FormattingEnabled = true;
            this.comboBox_BuySell_Security.Location = new System.Drawing.Point(157, 26);
            this.comboBox_BuySell_Security.MatchingMethod = SergeUtils.StringMatchingMethod.SubString;
            this.comboBox_BuySell_Security.Name = "comboBox_BuySell_Security";
            this.comboBox_BuySell_Security.Size = new System.Drawing.Size(835, 31);
            this.comboBox_BuySell_Security.TabIndex = 8;
            this.comboBox_BuySell_Security.SelectedIndexChanged += new System.EventHandler(this.comboBox_BuySellSecurity_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(316, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "шт. по цене";
            // 
            // numericUpDown_BuySell_Count
            // 
            this.numericUpDown_BuySell_Count.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_BuySell_Count.Location = new System.Drawing.Point(157, 81);
            this.numericUpDown_BuySell_Count.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_BuySell_Count.Name = "numericUpDown_BuySell_Count";
            this.numericUpDown_BuySell_Count.Size = new System.Drawing.Size(150, 30);
            this.numericUpDown_BuySell_Count.TabIndex = 7;
            this.numericUpDown_BuySell_Count.ValueChanged += new System.EventHandler(this.numericUpDown_BuySell_Count_ValueChanged);
            // 
            // dataGridView_GroupsByCurrency
            // 
            this.dataGridView_GroupsByCurrency.AllowUserToAddRows = false;
            this.dataGridView_GroupsByCurrency.AllowUserToDeleteRows = false;
            this.dataGridView_GroupsByCurrency.AllowUserToResizeRows = false;
            this.dataGridView_GroupsByCurrency.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_GroupsByCurrency.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView_GroupsByCurrency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_GroupsByCurrency.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn_CurrencyKey,
            this.dataGridViewTextBoxColumn_CurrencyValue,
            this.dataGridViewTextBoxColumn_CurrencyDifference});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_GroupsByCurrency.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView_GroupsByCurrency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_GroupsByCurrency.Location = new System.Drawing.Point(3, 328);
            this.dataGridView_GroupsByCurrency.Name = "dataGridView_GroupsByCurrency";
            this.dataGridView_GroupsByCurrency.ReadOnly = true;
            this.dataGridView_GroupsByCurrency.RowHeadersVisible = false;
            this.dataGridView_GroupsByCurrency.RowHeadersWidth = 51;
            this.dataGridView_GroupsByCurrency.RowTemplate.DefaultCellStyle.Format = "N2";
            this.dataGridView_GroupsByCurrency.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dataGridView_GroupsByCurrency.RowTemplate.Height = 24;
            this.dataGridView_GroupsByCurrency.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_GroupsByCurrency.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_GroupsByCurrency.Size = new System.Drawing.Size(364, 319);
            this.dataGridView_GroupsByCurrency.TabIndex = 8;
            this.dataGridView_GroupsByCurrency.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_RowEnter);
            // 
            // dataGridViewTextBoxColumn_CurrencyKey
            // 
            this.dataGridViewTextBoxColumn_CurrencyKey.FillWeight = 50F;
            this.dataGridViewTextBoxColumn_CurrencyKey.HeaderText = "Валюта";
            this.dataGridViewTextBoxColumn_CurrencyKey.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_CurrencyKey.Name = "dataGridViewTextBoxColumn_CurrencyKey";
            this.dataGridViewTextBoxColumn_CurrencyKey.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn_CurrencyValue
            // 
            this.dataGridViewTextBoxColumn_CurrencyValue.FillWeight = 25F;
            this.dataGridViewTextBoxColumn_CurrencyValue.HeaderText = "Доля";
            this.dataGridViewTextBoxColumn_CurrencyValue.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_CurrencyValue.Name = "dataGridViewTextBoxColumn_CurrencyValue";
            this.dataGridViewTextBoxColumn_CurrencyValue.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn_CurrencyDifference
            // 
            this.dataGridViewTextBoxColumn_CurrencyDifference.FillWeight = 25F;
            this.dataGridViewTextBoxColumn_CurrencyDifference.HeaderText = "Изменение";
            this.dataGridViewTextBoxColumn_CurrencyDifference.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_CurrencyDifference.Name = "dataGridViewTextBoxColumn_CurrencyDifference";
            this.dataGridViewTextBoxColumn_CurrencyDifference.ReadOnly = true;
            // 
            // dataGridView_GroupsBySector
            // 
            this.dataGridView_GroupsBySector.AllowUserToAddRows = false;
            this.dataGridView_GroupsBySector.AllowUserToDeleteRows = false;
            this.dataGridView_GroupsBySector.AllowUserToResizeRows = false;
            this.dataGridView_GroupsBySector.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_GroupsBySector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridView_GroupsBySector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_GroupsBySector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn_SectorKey,
            this.dataGridViewTextBoxColumn_SectorValue,
            this.dataGridViewTextBoxColumn_SectorDifference});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_GroupsBySector.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridView_GroupsBySector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_GroupsBySector.Location = new System.Drawing.Point(373, 3);
            this.dataGridView_GroupsBySector.Name = "dataGridView_GroupsBySector";
            this.dataGridView_GroupsBySector.ReadOnly = true;
            this.dataGridView_GroupsBySector.RowHeadersVisible = false;
            this.dataGridView_GroupsBySector.RowHeadersWidth = 51;
            this.dataGridView_GroupsBySector.RowTemplate.DefaultCellStyle.Format = "N2";
            this.dataGridView_GroupsBySector.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dataGridView_GroupsBySector.RowTemplate.Height = 24;
            this.dataGridView_GroupsBySector.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_GroupsBySector.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_GroupsBySector.Size = new System.Drawing.Size(364, 319);
            this.dataGridView_GroupsBySector.TabIndex = 9;
            this.dataGridView_GroupsBySector.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_RowEnter);
            // 
            // dataGridViewTextBoxColumn_SectorKey
            // 
            this.dataGridViewTextBoxColumn_SectorKey.FillWeight = 50F;
            this.dataGridViewTextBoxColumn_SectorKey.HeaderText = "Отрасль";
            this.dataGridViewTextBoxColumn_SectorKey.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_SectorKey.Name = "dataGridViewTextBoxColumn_SectorKey";
            this.dataGridViewTextBoxColumn_SectorKey.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn_SectorValue
            // 
            this.dataGridViewTextBoxColumn_SectorValue.FillWeight = 25F;
            this.dataGridViewTextBoxColumn_SectorValue.HeaderText = "Доля";
            this.dataGridViewTextBoxColumn_SectorValue.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_SectorValue.Name = "dataGridViewTextBoxColumn_SectorValue";
            this.dataGridViewTextBoxColumn_SectorValue.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn_SectorDifference
            // 
            this.dataGridViewTextBoxColumn_SectorDifference.FillWeight = 25F;
            this.dataGridViewTextBoxColumn_SectorDifference.HeaderText = "Изменение";
            this.dataGridViewTextBoxColumn_SectorDifference.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_SectorDifference.Name = "dataGridViewTextBoxColumn_SectorDifference";
            this.dataGridViewTextBoxColumn_SectorDifference.ReadOnly = true;
            // 
            // dataGridView_GroupsByType
            // 
            this.dataGridView_GroupsByType.AllowUserToAddRows = false;
            this.dataGridView_GroupsByType.AllowUserToDeleteRows = false;
            this.dataGridView_GroupsByType.AllowUserToResizeRows = false;
            this.dataGridView_GroupsByType.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_GroupsByType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridView_GroupsByType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_GroupsByType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn_TypeKey,
            this.dataGridViewTextBoxColumn_TypeValue,
            this.dataGridViewTextBoxColumn_TypeDifference});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_GroupsByType.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridView_GroupsByType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_GroupsByType.Location = new System.Drawing.Point(373, 328);
            this.dataGridView_GroupsByType.Name = "dataGridView_GroupsByType";
            this.dataGridView_GroupsByType.ReadOnly = true;
            this.dataGridView_GroupsByType.RowHeadersVisible = false;
            this.dataGridView_GroupsByType.RowHeadersWidth = 51;
            this.dataGridView_GroupsByType.RowTemplate.DefaultCellStyle.Format = "N2";
            this.dataGridView_GroupsByType.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dataGridView_GroupsByType.RowTemplate.Height = 24;
            this.dataGridView_GroupsByType.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_GroupsByType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_GroupsByType.Size = new System.Drawing.Size(364, 319);
            this.dataGridView_GroupsByType.TabIndex = 10;
            this.dataGridView_GroupsByType.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_RowEnter);
            // 
            // dataGridViewTextBoxColumn_TypeKey
            // 
            this.dataGridViewTextBoxColumn_TypeKey.FillWeight = 50F;
            this.dataGridViewTextBoxColumn_TypeKey.HeaderText = "Тип актива";
            this.dataGridViewTextBoxColumn_TypeKey.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_TypeKey.Name = "dataGridViewTextBoxColumn_TypeKey";
            this.dataGridViewTextBoxColumn_TypeKey.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn_TypeValue
            // 
            this.dataGridViewTextBoxColumn_TypeValue.FillWeight = 25F;
            this.dataGridViewTextBoxColumn_TypeValue.HeaderText = "Доля";
            this.dataGridViewTextBoxColumn_TypeValue.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_TypeValue.Name = "dataGridViewTextBoxColumn_TypeValue";
            this.dataGridViewTextBoxColumn_TypeValue.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn_TypeDifference
            // 
            this.dataGridViewTextBoxColumn_TypeDifference.FillWeight = 25F;
            this.dataGridViewTextBoxColumn_TypeDifference.HeaderText = "Изменение";
            this.dataGridViewTextBoxColumn_TypeDifference.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn_TypeDifference.Name = "dataGridViewTextBoxColumn_TypeDifference";
            this.dataGridViewTextBoxColumn_TypeDifference.ReadOnly = true;
            // 
            // listBox_GroupStocks
            // 
            this.listBox_GroupStocks.DisplayMember = "BestUniqueFriendlyName";
            this.listBox_GroupStocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_GroupStocks.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_GroupStocks.FormattingEnabled = true;
            this.listBox_GroupStocks.ItemHeight = 19;
            this.listBox_GroupStocks.Location = new System.Drawing.Point(3, 3);
            this.listBox_GroupStocks.Name = "listBox_GroupStocks";
            this.listBox_GroupStocks.Size = new System.Drawing.Size(254, 615);
            this.listBox_GroupStocks.TabIndex = 11;
            this.listBox_GroupStocks.SelectedIndexChanged += new System.EventHandler(this.listBox_GroupStocks_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_RunTask,
            this.toolStripMenuItem_CancelDeal,
            this.toolStripMenuItem_settings,
            this.toolStripMenuItem_About});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1022, 28);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_RunTask
            // 
            this.toolStripMenuItem_RunTask.Name = "toolStripMenuItem_RunTask";
            this.toolStripMenuItem_RunTask.Size = new System.Drawing.Size(158, 24);
            this.toolStripMenuItem_RunTask.Text = "Выполнить task.xml";
            this.toolStripMenuItem_RunTask.Click += new System.EventHandler(this.toolStripMenuItem_RunTask_Click);
            // 
            // toolStripMenuItem_CancelDeal
            // 
            this.toolStripMenuItem_CancelDeal.Name = "toolStripMenuItem_CancelDeal";
            this.toolStripMenuItem_CancelDeal.Size = new System.Drawing.Size(140, 24);
            this.toolStripMenuItem_CancelDeal.Text = "Отменить сделку";
            this.toolStripMenuItem_CancelDeal.Click += new System.EventHandler(this.toolStripMenuItem_CancelDeal_Click);
            // 
            // toolStripMenuItem_settings
            // 
            this.toolStripMenuItem_settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Log});
            this.toolStripMenuItem_settings.Name = "toolStripMenuItem_settings";
            this.toolStripMenuItem_settings.Size = new System.Drawing.Size(98, 24);
            this.toolStripMenuItem_settings.Text = "Настройки";
            // 
            // toolStripMenuItem_Log
            // 
            this.toolStripMenuItem_Log.CheckOnClick = true;
            this.toolStripMenuItem_Log.Name = "toolStripMenuItem_Log";
            this.toolStripMenuItem_Log.Size = new System.Drawing.Size(187, 26);
            this.toolStripMenuItem_Log.Text = "Вести журнал";
            this.toolStripMenuItem_Log.CheckStateChanged += new System.EventHandler(this.toolStripMenuItem_Log_CheckStateChanged);
            // 
            // toolStripMenuItem_About
            // 
            this.toolStripMenuItem_About.Name = "toolStripMenuItem_About";
            this.toolStripMenuItem_About.Size = new System.Drawing.Size(118, 24);
            this.toolStripMenuItem_About.Text = "О программе";
            this.toolStripMenuItem_About.Click += new System.EventHandler(this.toolStripMenuItem_About_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 820);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1022, 26);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(112, 20);
            this.toolStripStatusLabel1.Text = "Готов к работе";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_GroupsByCountry, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_GroupsBySector, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_GroupsByCurrency, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_GroupsByType, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 31);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(740, 650);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // tabControl_Right
            // 
            this.tabControl_Right.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_Right.Controls.Add(this.tabPage_GroupContent);
            this.tabControl_Right.Controls.Add(this.tabPage_Deals);
            this.tabControl_Right.Location = new System.Drawing.Point(754, 31);
            this.tabControl_Right.Name = "tabControl_Right";
            this.tabControl_Right.SelectedIndex = 0;
            this.tabControl_Right.Size = new System.Drawing.Size(268, 650);
            this.tabControl_Right.TabIndex = 15;
            // 
            // tabPage_GroupContent
            // 
            this.tabPage_GroupContent.Controls.Add(this.listBox_GroupStocks);
            this.tabPage_GroupContent.Location = new System.Drawing.Point(4, 25);
            this.tabPage_GroupContent.Name = "tabPage_GroupContent";
            this.tabPage_GroupContent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_GroupContent.Size = new System.Drawing.Size(260, 621);
            this.tabPage_GroupContent.TabIndex = 0;
            this.tabPage_GroupContent.Text = "Состав";
            this.tabPage_GroupContent.UseVisualStyleBackColor = true;
            // 
            // tabPage_Deals
            // 
            this.tabPage_Deals.Controls.Add(this.listBox_Deals);
            this.tabPage_Deals.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Deals.Name = "tabPage_Deals";
            this.tabPage_Deals.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Deals.Size = new System.Drawing.Size(260, 621);
            this.tabPage_Deals.TabIndex = 1;
            this.tabPage_Deals.Text = "Сделки";
            this.tabPage_Deals.UseVisualStyleBackColor = true;
            // 
            // listBox_Deals
            // 
            this.listBox_Deals.DisplayMember = "FriendlyName";
            this.listBox_Deals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Deals.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Deals.FormattingEnabled = true;
            this.listBox_Deals.ItemHeight = 19;
            this.listBox_Deals.Location = new System.Drawing.Point(3, 3);
            this.listBox_Deals.Name = "listBox_Deals";
            this.listBox_Deals.Size = new System.Drawing.Size(254, 615);
            this.listBox_Deals.TabIndex = 12;
            this.listBox_Deals.SelectedIndexChanged += new System.EventHandler(this.listBox_Deals_SelectedIndexChanged);
            // 
            // numericUpDown_BuySell_Total
            // 
            this.numericUpDown_BuySell_Total.DecimalPlaces = 6;
            this.numericUpDown_BuySell_Total.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_BuySell_Total.Location = new System.Drawing.Point(823, 82);
            this.numericUpDown_BuySell_Total.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_BuySell_Total.Name = "numericUpDown_BuySell_Total";
            this.numericUpDown_BuySell_Total.Size = new System.Drawing.Size(169, 30);
            this.numericUpDown_BuySell_Total.TabIndex = 12;
            this.numericUpDown_BuySell_Total.ThousandsSeparator = true;
            this.numericUpDown_BuySell_Total.ValueChanged += new System.EventHandler(this.numericUpDown_BuySell_Total_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 846);
            this.Controls.Add(this.tabControl_Right);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "ndp_invest_helper - помощник в мире инвестиций";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupsByCountry)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Price)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Count)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupsByCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupsBySector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupsByType)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl_Right.ResumeLayout(false);
            this.tabPage_GroupContent.ResumeLayout(false);
            this.tabPage_Deals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Total)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_GroupsByCountry;
        private System.Windows.Forms.Button buttonBuy;
        private System.Windows.Forms.Button buttonSell;
        private System.Windows.Forms.ComboBox comboBox_BuySell_Currency;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_BuySell_Count;
        private SergeUtils.EasyCompletionComboBox comboBox_BuySell_Security;
        private System.Windows.Forms.NumericUpDown numericUpDown_BuySell_Price;
        private System.Windows.Forms.DataGridView dataGridView_GroupsByCurrency;
        private System.Windows.Forms.DataGridView dataGridView_GroupsBySector;
        private System.Windows.Forms.DataGridView dataGridView_GroupsByType;
        private System.Windows.Forms.ListBox listBox_GroupStocks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_RunTask;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_About;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_settings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Log;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_CountryKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_CountryValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_CountryDifference;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_CurrencyKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_CurrencyValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_CurrencyDifference;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_SectorKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_SectorValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_SectorDifference;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_TypeKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_TypeValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_TypeDifference;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CancelDeal;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl_Right;
        private System.Windows.Forms.TabPage tabPage_GroupContent;
        private System.Windows.Forms.TabPage tabPage_Deals;
        private System.Windows.Forms.ListBox listBox_Deals;
        private System.Windows.Forms.NumericUpDown numericUpDown_BuySell_Total;
    }
}

