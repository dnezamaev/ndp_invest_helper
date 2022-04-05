
namespace ndp_invest_helper.GUI.Krypton
{
    partial class DbEditorDiversityControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonSplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.dataGrid_Parts = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dataGrid_Keys = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.KeysColumn_Key = new System.Windows.Forms.DataGridViewLinkColumn();
            this.KeysColumn_Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.radioButton_Assets = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioButton_Sectors = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioButton_Countries = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioButton_Currencies = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.radioButton_Issuer = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioButton_Securities = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.comboBox_SecurityIssuer = new SergeUtils.EasyCompletionComboBox();
            this.kryptonPage4 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPanel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.PartsColumn_Key = new System.Windows.Forms.DataGridViewLinkColumn();
            this.PartsColumn_Value = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Parts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Keys)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(3, 238);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.dataGrid_Parts);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.dataGrid_Keys);
            this.kryptonSplitContainer1.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(702, 405);
            this.kryptonSplitContainer1.SplitterDistance = 345;
            this.kryptonSplitContainer1.TabIndex = 15;
            // 
            // dataGrid_Parts
            // 
            this.dataGrid_Parts.AllowUserToAddRows = false;
            this.dataGrid_Parts.AllowUserToDeleteRows = false;
            this.dataGrid_Parts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Parts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartsColumn_Key,
            this.PartsColumn_Value});
            this.dataGrid_Parts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Parts.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_Parts.MultiSelect = false;
            this.dataGrid_Parts.Name = "dataGrid_Parts";
            this.dataGrid_Parts.RowHeadersVisible = false;
            this.dataGrid_Parts.RowHeadersWidth = 51;
            this.dataGrid_Parts.RowTemplate.Height = 24;
            this.dataGrid_Parts.Size = new System.Drawing.Size(345, 405);
            this.dataGrid_Parts.TabIndex = 0;
            this.dataGrid_Parts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Parts_CellContentClick);
            this.dataGrid_Parts.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Parts_CellValueChanged);
            // 
            // dataGrid_Keys
            // 
            this.dataGrid_Keys.AllowUserToAddRows = false;
            this.dataGrid_Keys.AllowUserToDeleteRows = false;
            this.dataGrid_Keys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Keys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KeysColumn_Key,
            this.KeysColumn_Text});
            this.dataGrid_Keys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Keys.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_Keys.MultiSelect = false;
            this.dataGrid_Keys.Name = "dataGrid_Keys";
            this.dataGrid_Keys.ReadOnly = true;
            this.dataGrid_Keys.RowHeadersVisible = false;
            this.dataGrid_Keys.RowHeadersWidth = 51;
            this.dataGrid_Keys.RowTemplate.Height = 24;
            this.dataGrid_Keys.Size = new System.Drawing.Size(352, 405);
            this.dataGrid_Keys.TabIndex = 1;
            this.dataGrid_Keys.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Keys_CellContentClick);
            // 
            // KeysColumn_Key
            // 
            this.KeysColumn_Key.ActiveLinkColor = System.Drawing.Color.Blue;
            this.KeysColumn_Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.KeysColumn_Key.FillWeight = 25F;
            this.KeysColumn_Key.HeaderText = "Ключ";
            this.KeysColumn_Key.MinimumWidth = 6;
            this.KeysColumn_Key.Name = "KeysColumn_Key";
            this.KeysColumn_Key.ReadOnly = true;
            this.KeysColumn_Key.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KeysColumn_Key.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.KeysColumn_Key.TrackVisitedState = false;
            this.KeysColumn_Key.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // KeysColumn_Text
            // 
            this.KeysColumn_Text.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.KeysColumn_Text.FillWeight = 75F;
            this.KeysColumn_Text.HeaderText = "Название";
            this.KeysColumn_Text.MinimumWidth = 6;
            this.KeysColumn_Text.Name = "KeysColumn_Text";
            this.KeysColumn_Text.ReadOnly = true;
            this.KeysColumn_Text.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.radioButton_Assets);
            this.kryptonPanel2.Controls.Add(this.radioButton_Sectors);
            this.kryptonPanel2.Controls.Add(this.radioButton_Countries);
            this.kryptonPanel2.Controls.Add(this.radioButton_Currencies);
            this.kryptonPanel2.Location = new System.Drawing.Point(3, 100);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(568, 32);
            this.kryptonPanel2.TabIndex = 14;
            // 
            // radioButton_Assets
            // 
            this.radioButton_Assets.Location = new System.Drawing.Point(354, 3);
            this.radioButton_Assets.Name = "radioButton_Assets";
            this.radioButton_Assets.Size = new System.Drawing.Size(132, 24);
            this.radioButton_Assets.TabIndex = 15;
            this.radioButton_Assets.Values.Text = "Состав активов";
            this.radioButton_Assets.CheckedChanged += new System.EventHandler(this.radioButton_DivItem_CheckedChanged);
            // 
            // radioButton_Sectors
            // 
            this.radioButton_Sectors.Location = new System.Drawing.Point(179, 3);
            this.radioButton_Sectors.Name = "radioButton_Sectors";
            this.radioButton_Sectors.Size = new System.Drawing.Size(164, 24);
            this.radioButton_Sectors.TabIndex = 14;
            this.radioButton_Sectors.Values.Text = "Сектора экономики";
            this.radioButton_Sectors.CheckedChanged += new System.EventHandler(this.radioButton_DivItem_CheckedChanged);
            // 
            // radioButton_Countries
            // 
            this.radioButton_Countries.Location = new System.Drawing.Point(92, 3);
            this.radioButton_Countries.Name = "radioButton_Countries";
            this.radioButton_Countries.Size = new System.Drawing.Size(76, 24);
            this.radioButton_Countries.TabIndex = 13;
            this.radioButton_Countries.Values.Text = "Страны";
            this.radioButton_Countries.CheckedChanged += new System.EventHandler(this.radioButton_DivItem_CheckedChanged);
            // 
            // radioButton_Currencies
            // 
            this.radioButton_Currencies.Checked = true;
            this.radioButton_Currencies.Location = new System.Drawing.Point(3, 3);
            this.radioButton_Currencies.Name = "radioButton_Currencies";
            this.radioButton_Currencies.Size = new System.Drawing.Size(78, 24);
            this.radioButton_Currencies.TabIndex = 12;
            this.radioButton_Currencies.Values.Text = "Валюты";
            this.radioButton_Currencies.CheckedChanged += new System.EventHandler(this.radioButton_DivItem_CheckedChanged);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.radioButton_Issuer);
            this.kryptonPanel1.Controls.Add(this.radioButton_Securities);
            this.kryptonPanel1.Location = new System.Drawing.Point(3, 3);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(173, 32);
            this.kryptonPanel1.TabIndex = 12;
            // 
            // radioButton_Issuer
            // 
            this.radioButton_Issuer.Location = new System.Drawing.Point(81, 3);
            this.radioButton_Issuer.Name = "radioButton_Issuer";
            this.radioButton_Issuer.Size = new System.Drawing.Size(83, 24);
            this.radioButton_Issuer.TabIndex = 13;
            this.radioButton_Issuer.Values.Text = "Эмитент";
            this.radioButton_Issuer.CheckedChanged += new System.EventHandler(this.radioButton_SecurityIssuer_CheckedChanged);
            // 
            // radioButton_Securities
            // 
            this.radioButton_Securities.Checked = true;
            this.radioButton_Securities.Location = new System.Drawing.Point(3, 3);
            this.radioButton_Securities.Name = "radioButton_Securities";
            this.radioButton_Securities.Size = new System.Drawing.Size(72, 24);
            this.radioButton_Securities.TabIndex = 12;
            this.radioButton_Securities.Values.Text = "Бумага";
            this.radioButton_Securities.CheckedChanged += new System.EventHandler(this.radioButton_SecurityIssuer_CheckedChanged);
            // 
            // comboBox_SecurityIssuer
            // 
            this.comboBox_SecurityIssuer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_SecurityIssuer.DisplayMember = "FullName";
            this.comboBox_SecurityIssuer.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_SecurityIssuer.FormattingEnabled = true;
            this.comboBox_SecurityIssuer.Location = new System.Drawing.Point(1, 50);
            this.comboBox_SecurityIssuer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_SecurityIssuer.MatchingMethod = SergeUtils.StringMatchingMethod.SubString;
            this.comboBox_SecurityIssuer.Name = "comboBox_SecurityIssuer";
            this.comboBox_SecurityIssuer.Size = new System.Drawing.Size(706, 31);
            this.comboBox_SecurityIssuer.TabIndex = 9;
            this.comboBox_SecurityIssuer.SelectedIndexChanged += new System.EventHandler(this.comboBox_SecurityIssuer_SelectedIndexChanged);
            // 
            // kryptonPage4
            // 
            this.kryptonPage4.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage4.Flags = 65534;
            this.kryptonPage4.LastVisibleSet = true;
            this.kryptonPage4.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage4.Name = "kryptonPage4";
            this.kryptonPage4.Size = new System.Drawing.Size(100, 100);
            this.kryptonPage4.Text = "kryptonPage4";
            this.kryptonPage4.ToolTipTitle = "Page ToolTip";
            this.kryptonPage4.UniqueName = "CC32A7A41B374CDA4CBE9CBB733537F0";
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel3.Controls.Add(this.kryptonSplitContainer1);
            this.kryptonPanel3.Controls.Add(this.kryptonPanel1);
            this.kryptonPanel3.Controls.Add(this.kryptonPanel2);
            this.kryptonPanel3.Controls.Add(this.comboBox_SecurityIssuer);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(710, 646);
            this.kryptonPanel3.TabIndex = 2;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel1.Location = new System.Drawing.Point(6, 168);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(450, 64);
            this.kryptonLabel1.TabIndex = 16;
            this.kryptonLabel1.Values.Text = "Для добавления ключа нажмите на ссылку в правой таблице.\r\nДля удаления - в левой " +
    "таблице.\r\nДоля редактируется прямо в ячейке.\r\n";
            // 
            // PartsColumn_Key
            // 
            this.PartsColumn_Key.ActiveLinkColor = System.Drawing.Color.Blue;
            this.PartsColumn_Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartsColumn_Key.FillWeight = 25F;
            this.PartsColumn_Key.HeaderText = "Ключ";
            this.PartsColumn_Key.MinimumWidth = 6;
            this.PartsColumn_Key.Name = "PartsColumn_Key";
            this.PartsColumn_Key.ReadOnly = true;
            this.PartsColumn_Key.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PartsColumn_Key.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PartsColumn_Key.TrackVisitedState = false;
            this.PartsColumn_Key.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // PartsColumn_Value
            // 
            this.PartsColumn_Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartsColumn_Value.DecimalPlaces = 2;
            this.PartsColumn_Value.FillWeight = 75F;
            this.PartsColumn_Value.HeaderText = "Доля %";
            this.PartsColumn_Value.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PartsColumn_Value.MinimumWidth = 6;
            this.PartsColumn_Value.Name = "PartsColumn_Value";
            this.PartsColumn_Value.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PartsColumn_Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PartsColumn_Value.Width = 258;
            // 
            // DbEditorDiversityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel3);
            this.Name = "DbEditorDiversityControl";
            this.Size = new System.Drawing.Size(710, 646);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Parts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Keys)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private SergeUtils.EasyCompletionComboBox comboBox_SecurityIssuer;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioButton_Issuer;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioButton_Securities;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioButton_Assets;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioButton_Sectors;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioButton_Countries;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioButton_Currencies;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGrid_Parts;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage4;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGrid_Keys;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.DataGridViewLinkColumn KeysColumn_Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeysColumn_Text;
        private System.Windows.Forms.DataGridViewLinkColumn PartsColumn_Key;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn PartsColumn_Value;
    }
}
