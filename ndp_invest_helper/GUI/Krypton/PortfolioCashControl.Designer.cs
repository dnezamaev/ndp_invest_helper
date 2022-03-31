
namespace ndp_invest_helper.GUI.Krypton
{
    partial class PortfolioCashControl
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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.dataGrid_Cash = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.ColumnCurrencyType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnCurrencyValue = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Cash)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.dataGrid_Cash);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(437, 616);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // dataGrid_Cash
            // 
            this.dataGrid_Cash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Cash.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCurrencyType,
            this.ColumnCurrencyValue});
            this.dataGrid_Cash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Cash.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_Cash.Name = "dataGrid_Cash";
            this.dataGrid_Cash.RowHeadersVisible = false;
            this.dataGrid_Cash.RowHeadersWidth = 51;
            this.dataGrid_Cash.RowTemplate.Height = 24;
            this.dataGrid_Cash.Size = new System.Drawing.Size(437, 616);
            this.dataGrid_Cash.TabIndex = 2;
            // 
            // ColumnCurrencyType
            // 
            this.ColumnCurrencyType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnCurrencyType.HeaderText = "Валюта";
            this.ColumnCurrencyType.Items.AddRange(new object[] {
            "RUB",
            "EUR",
            "USD"});
            this.ColumnCurrencyType.MinimumWidth = 6;
            this.ColumnCurrencyType.Name = "ColumnCurrencyType";
            // 
            // ColumnCurrencyValue
            // 
            this.ColumnCurrencyValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnCurrencyValue.DecimalPlaces = 2;
            this.ColumnCurrencyValue.HeaderText = "Значение";
            this.ColumnCurrencyValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.ColumnCurrencyValue.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.ColumnCurrencyValue.MinimumWidth = 6;
            this.ColumnCurrencyValue.Name = "ColumnCurrencyValue";
            this.ColumnCurrencyValue.ThousandsSeparator = true;
            this.ColumnCurrencyValue.Width = 218;
            // 
            // PortfolioCashControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "PortfolioCashControl";
            this.Size = new System.Drawing.Size(437, 616);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Cash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGrid_Cash;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnCurrencyType;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn ColumnCurrencyValue;
    }
}
