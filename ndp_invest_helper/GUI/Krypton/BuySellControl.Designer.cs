
namespace ndp_invest_helper.GUI.Krypton
{
    partial class BuySellControl
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
            this.numericUpDown_BuySell_Total = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_BuySell_Price = new System.Windows.Forms.NumericUpDown();
            this.comboBox_BuySell_Security = new SergeUtils.EasyCompletionComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_BuySell_Count = new System.Windows.Forms.NumericUpDown();
            this.buttonSell = new System.Windows.Forms.Button();
            this.comboBox_BuySell_Currency = new System.Windows.Forms.ComboBox();
            this.buttonBuy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Total)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Price)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Count)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_BuySell_Total
            // 
            this.numericUpDown_BuySell_Total.DecimalPlaces = 6;
            this.numericUpDown_BuySell_Total.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_BuySell_Total.Location = new System.Drawing.Point(832, 76);
            this.numericUpDown_BuySell_Total.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(724, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "на сумму";
            // 
            // numericUpDown_BuySell_Price
            // 
            this.numericUpDown_BuySell_Price.DecimalPlaces = 6;
            this.numericUpDown_BuySell_Price.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_BuySell_Price.Location = new System.Drawing.Point(458, 75);
            this.numericUpDown_BuySell_Price.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_BuySell_Price.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_BuySell_Price.Name = "numericUpDown_BuySell_Price";
            this.numericUpDown_BuySell_Price.Size = new System.Drawing.Size(149, 30);
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
            this.comboBox_BuySell_Security.Location = new System.Drawing.Point(166, 20);
            this.comboBox_BuySell_Security.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.label1.Location = new System.Drawing.Point(325, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "шт. по цене";
            // 
            // numericUpDown_BuySell_Count
            // 
            this.numericUpDown_BuySell_Count.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_BuySell_Count.Location = new System.Drawing.Point(166, 75);
            this.numericUpDown_BuySell_Count.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_BuySell_Count.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_BuySell_Count.Name = "numericUpDown_BuySell_Count";
            this.numericUpDown_BuySell_Count.Size = new System.Drawing.Size(149, 30);
            this.numericUpDown_BuySell_Count.TabIndex = 7;
            this.numericUpDown_BuySell_Count.ValueChanged += new System.EventHandler(this.numericUpDown_BuySell_Count_ValueChanged);
            // 
            // buttonSell
            // 
            this.buttonSell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSell.Location = new System.Drawing.Point(14, 75);
            this.buttonSell.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSell.Name = "buttonSell";
            this.buttonSell.Size = new System.Drawing.Size(120, 39);
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
            this.comboBox_BuySell_Currency.Location = new System.Drawing.Point(614, 74);
            this.comboBox_BuySell_Currency.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_BuySell_Currency.Name = "comboBox_BuySell_Currency";
            this.comboBox_BuySell_Currency.Size = new System.Drawing.Size(103, 32);
            this.comboBox_BuySell_Currency.TabIndex = 6;
            this.comboBox_BuySell_Currency.SelectedIndexChanged += new System.EventHandler(this.comboBox_BuySell_Currency_SelectedIndexChanged);
            // 
            // buttonBuy
            // 
            this.buttonBuy.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBuy.Location = new System.Drawing.Point(14, 15);
            this.buttonBuy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.Size = new System.Drawing.Size(120, 39);
            this.buttonBuy.TabIndex = 2;
            this.buttonBuy.Text = "Купить";
            this.buttonBuy.UseVisualStyleBackColor = true;
            this.buttonBuy.Click += new System.EventHandler(this.buttonBuySell_Click);
            // 
            // BuySellControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDown_BuySell_Total);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBuy);
            this.Controls.Add(this.numericUpDown_BuySell_Price);
            this.Controls.Add(this.comboBox_BuySell_Currency);
            this.Controls.Add(this.comboBox_BuySell_Security);
            this.Controls.Add(this.buttonSell);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_BuySell_Count);
            this.Name = "BuySellControl";
            this.Size = new System.Drawing.Size(1011, 130);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Total)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Price)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BuySell_Count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown_BuySell_Total;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_BuySell_Price;
        private SergeUtils.EasyCompletionComboBox comboBox_BuySell_Security;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_BuySell_Count;
        private System.Windows.Forms.Button buttonSell;
        private System.Windows.Forms.ComboBox comboBox_BuySell_Currency;
        private System.Windows.Forms.Button buttonBuy;
    }
}
