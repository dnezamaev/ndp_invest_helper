
namespace ndp_invest_helper.GUI.Krypton
{
    partial class OfficerReportControl
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
            this.label_OfficerReportClickerState = new System.Windows.Forms.Label();
            this.numericUpDown_AutoClickerStartDelaySec = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown_AutoClickerDelayMs = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_Misc_StartAutoOfficerOthers = new System.Windows.Forms.Button();
            this.button_Misc_StartAutoOfficerShares = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_Misc_SelectOthersOfficerReport = new System.Windows.Forms.Button();
            this.textBox_Misc_SelectOthersOfficerReport = new System.Windows.Forms.TextBox();
            this.button_Misc_SelectSharesOffcerReport = new System.Windows.Forms.Button();
            this.textBox_Misc_SelectSharesOfficerReport = new System.Windows.Forms.TextBox();
            this.backgroundWorker_OfficerReportFiller = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoClickerStartDelaySec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoClickerDelayMs)).BeginInit();
            this.SuspendLayout();
            // 
            // label_OfficerReportClickerState
            // 
            this.label_OfficerReportClickerState.AutoSize = true;
            this.label_OfficerReportClickerState.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label_OfficerReportClickerState.Location = new System.Drawing.Point(7, 414);
            this.label_OfficerReportClickerState.Name = "label_OfficerReportClickerState";
            this.label_OfficerReportClickerState.Size = new System.Drawing.Size(239, 24);
            this.label_OfficerReportClickerState.TabIndex = 31;
            this.label_OfficerReportClickerState.Text = "До запуска осталось ...";
            // 
            // numericUpDown_AutoClickerStartDelaySec
            // 
            this.numericUpDown_AutoClickerStartDelaySec.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.numericUpDown_AutoClickerStartDelaySec.Location = new System.Drawing.Point(7, 364);
            this.numericUpDown_AutoClickerStartDelaySec.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_AutoClickerStartDelaySec.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDown_AutoClickerStartDelaySec.Name = "numericUpDown_AutoClickerStartDelaySec";
            this.numericUpDown_AutoClickerStartDelaySec.Size = new System.Drawing.Size(120, 30);
            this.numericUpDown_AutoClickerStartDelaySec.TabIndex = 30;
            this.numericUpDown_AutoClickerStartDelaySec.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(139, 290);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 24);
            this.label10.TabIndex = 29;
            this.label10.Text = "мс";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(133, 364);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 24);
            this.label9.TabIndex = 28;
            this.label9.Text = "сек";
            // 
            // numericUpDown_AutoClickerDelayMs
            // 
            this.numericUpDown_AutoClickerDelayMs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.numericUpDown_AutoClickerDelayMs.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_AutoClickerDelayMs.Location = new System.Drawing.Point(7, 285);
            this.numericUpDown_AutoClickerDelayMs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_AutoClickerDelayMs.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDown_AutoClickerDelayMs.Name = "numericUpDown_AutoClickerDelayMs";
            this.numericUpDown_AutoClickerDelayMs.Size = new System.Drawing.Size(120, 30);
            this.numericUpDown_AutoClickerDelayMs.TabIndex = 27;
            this.numericUpDown_AutoClickerDelayMs.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(7, 337);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(385, 24);
            this.label8.TabIndex = 26;
            this.label8.Text = "Время на фокусирование Справки БК";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(7, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(235, 24);
            this.label7.TabIndex = 25;
            this.label7.Text = "Задержка автокликера";
            // 
            // button_Misc_StartAutoOfficerOthers
            // 
            this.button_Misc_StartAutoOfficerOthers.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.button_Misc_StartAutoOfficerOthers.Location = new System.Drawing.Point(685, 181);
            this.button_Misc_StartAutoOfficerOthers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Misc_StartAutoOfficerOthers.Name = "button_Misc_StartAutoOfficerOthers";
            this.button_Misc_StartAutoOfficerOthers.Size = new System.Drawing.Size(149, 44);
            this.button_Misc_StartAutoOfficerOthers.TabIndex = 24;
            this.button_Misc_StartAutoOfficerOthers.Text = "Запуск";
            this.button_Misc_StartAutoOfficerOthers.UseVisualStyleBackColor = true;
            this.button_Misc_StartAutoOfficerOthers.Visible = false;
            // 
            // button_Misc_StartAutoOfficerShares
            // 
            this.button_Misc_StartAutoOfficerShares.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.button_Misc_StartAutoOfficerShares.Location = new System.Drawing.Point(685, 82);
            this.button_Misc_StartAutoOfficerShares.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Misc_StartAutoOfficerShares.Name = "button_Misc_StartAutoOfficerShares";
            this.button_Misc_StartAutoOfficerShares.Size = new System.Drawing.Size(149, 44);
            this.button_Misc_StartAutoOfficerShares.TabIndex = 23;
            this.button_Misc_StartAutoOfficerShares.Text = "Запуск";
            this.button_Misc_StartAutoOfficerShares.UseVisualStyleBackColor = true;
            this.button_Misc_StartAutoOfficerShares.Click += new System.EventHandler(this.button_Misc_StartAutoOfficerReport_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(5, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 24);
            this.label6.TabIndex = 22;
            this.label6.Text = "Другие бумаги";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(389, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(250, 24);
            this.label5.TabIndex = 21;
            this.label5.Text = "Отчет для госслужащих";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(7, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 24);
            this.label4.TabIndex = 20;
            this.label4.Text = "Акции из отчета ВТБ";
            // 
            // button_Misc_SelectOthersOfficerReport
            // 
            this.button_Misc_SelectOthersOfficerReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.button_Misc_SelectOthersOfficerReport.Location = new System.Drawing.Point(486, 181);
            this.button_Misc_SelectOthersOfficerReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Misc_SelectOthersOfficerReport.Name = "button_Misc_SelectOthersOfficerReport";
            this.button_Misc_SelectOthersOfficerReport.Size = new System.Drawing.Size(149, 44);
            this.button_Misc_SelectOthersOfficerReport.TabIndex = 19;
            this.button_Misc_SelectOthersOfficerReport.Text = ". . .";
            this.button_Misc_SelectOthersOfficerReport.UseVisualStyleBackColor = true;
            this.button_Misc_SelectOthersOfficerReport.Visible = false;
            // 
            // textBox_Misc_SelectOthersOfficerReport
            // 
            this.textBox_Misc_SelectOthersOfficerReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.textBox_Misc_SelectOthersOfficerReport.Location = new System.Drawing.Point(7, 188);
            this.textBox_Misc_SelectOthersOfficerReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Misc_SelectOthersOfficerReport.Name = "textBox_Misc_SelectOthersOfficerReport";
            this.textBox_Misc_SelectOthersOfficerReport.Size = new System.Drawing.Size(461, 30);
            this.textBox_Misc_SelectOthersOfficerReport.TabIndex = 18;
            this.textBox_Misc_SelectOthersOfficerReport.Visible = false;
            // 
            // button_Misc_SelectSharesOffcerReport
            // 
            this.button_Misc_SelectSharesOffcerReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.button_Misc_SelectSharesOffcerReport.Location = new System.Drawing.Point(486, 82);
            this.button_Misc_SelectSharesOffcerReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Misc_SelectSharesOffcerReport.Name = "button_Misc_SelectSharesOffcerReport";
            this.button_Misc_SelectSharesOffcerReport.Size = new System.Drawing.Size(149, 44);
            this.button_Misc_SelectSharesOffcerReport.TabIndex = 17;
            this.button_Misc_SelectSharesOffcerReport.Text = ". . .";
            this.button_Misc_SelectSharesOffcerReport.UseVisualStyleBackColor = true;
            this.button_Misc_SelectSharesOffcerReport.Click += new System.EventHandler(this.button_Misc_SelectGovReport_Click);
            // 
            // textBox_Misc_SelectSharesOfficerReport
            // 
            this.textBox_Misc_SelectSharesOfficerReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.textBox_Misc_SelectSharesOfficerReport.Location = new System.Drawing.Point(7, 89);
            this.textBox_Misc_SelectSharesOfficerReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Misc_SelectSharesOfficerReport.Name = "textBox_Misc_SelectSharesOfficerReport";
            this.textBox_Misc_SelectSharesOfficerReport.Size = new System.Drawing.Size(461, 30);
            this.textBox_Misc_SelectSharesOfficerReport.TabIndex = 16;
            // 
            // backgroundWorker_OfficerReportFiller
            // 
            this.backgroundWorker_OfficerReportFiller.WorkerReportsProgress = true;
            this.backgroundWorker_OfficerReportFiller.WorkerSupportsCancellation = true;
            this.backgroundWorker_OfficerReportFiller.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_OfficerReportFiller_DoWork);
            this.backgroundWorker_OfficerReportFiller.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_OfficerReportFiller_ProgressChanged);
            this.backgroundWorker_OfficerReportFiller.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_OfficerReportFiller_RunWorkerCompleted);
            // 
            // OfficerReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_OfficerReportClickerState);
            this.Controls.Add(this.numericUpDown_AutoClickerStartDelaySec);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDown_AutoClickerDelayMs);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_Misc_StartAutoOfficerOthers);
            this.Controls.Add(this.button_Misc_StartAutoOfficerShares);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_Misc_SelectOthersOfficerReport);
            this.Controls.Add(this.textBox_Misc_SelectOthersOfficerReport);
            this.Controls.Add(this.button_Misc_SelectSharesOffcerReport);
            this.Controls.Add(this.textBox_Misc_SelectSharesOfficerReport);
            this.Name = "OfficerReportControl";
            this.Size = new System.Drawing.Size(850, 470);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoClickerStartDelaySec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoClickerDelayMs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_OfficerReportClickerState;
        private System.Windows.Forms.NumericUpDown numericUpDown_AutoClickerStartDelaySec;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown_AutoClickerDelayMs;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_Misc_StartAutoOfficerOthers;
        private System.Windows.Forms.Button button_Misc_StartAutoOfficerShares;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_Misc_SelectOthersOfficerReport;
        private System.Windows.Forms.TextBox textBox_Misc_SelectOthersOfficerReport;
        private System.Windows.Forms.Button button_Misc_SelectSharesOffcerReport;
        private System.Windows.Forms.TextBox textBox_Misc_SelectSharesOfficerReport;
        private System.ComponentModel.BackgroundWorker backgroundWorker_OfficerReportFiller;
    }
}
