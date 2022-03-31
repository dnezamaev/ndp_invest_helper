
namespace ndp_invest_helper.GUI.Krypton
{
    partial class DealsControl
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
            this.listBox_Deals = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox_Deals
            // 
            this.listBox_Deals.DisplayMember = "FriendlyName";
            this.listBox_Deals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Deals.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Deals.FormattingEnabled = true;
            this.listBox_Deals.ItemHeight = 19;
            this.listBox_Deals.Location = new System.Drawing.Point(0, 0);
            this.listBox_Deals.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox_Deals.Name = "listBox_Deals";
            this.listBox_Deals.Size = new System.Drawing.Size(150, 150);
            this.listBox_Deals.TabIndex = 13;
            // 
            // DealsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBox_Deals);
            this.Name = "DealsControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_Deals;
    }
}
