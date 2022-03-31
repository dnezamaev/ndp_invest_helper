
namespace ndp_invest_helper.GUI.Krypton
{
    partial class AssetsControl
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
            this.dataGridView_GroupContent = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupContent)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_GroupContent
            // 
            this.dataGridView_GroupContent.AllowUserToAddRows = false;
            this.dataGridView_GroupContent.AllowUserToDeleteRows = false;
            this.dataGridView_GroupContent.AllowUserToResizeRows = false;
            this.dataGridView_GroupContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_GroupContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_GroupContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView_GroupContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_GroupContent.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_GroupContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView_GroupContent.Name = "dataGridView_GroupContent";
            this.dataGridView_GroupContent.ReadOnly = true;
            this.dataGridView_GroupContent.RowHeadersVisible = false;
            this.dataGridView_GroupContent.RowHeadersWidth = 51;
            this.dataGridView_GroupContent.RowTemplate.DefaultCellStyle.Format = "N2";
            this.dataGridView_GroupContent.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dataGridView_GroupContent.RowTemplate.Height = 24;
            this.dataGridView_GroupContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_GroupContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_GroupContent.Size = new System.Drawing.Size(232, 348);
            this.dataGridView_GroupContent.TabIndex = 2;
            this.dataGridView_GroupContent.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_GroupContent_RowEnter);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Бумага";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 25F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Доля";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // AssetsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView_GroupContent);
            this.Name = "AssetsControl";
            this.Size = new System.Drawing.Size(232, 348);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GroupContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_GroupContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}
