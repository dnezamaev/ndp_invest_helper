
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
            this.label_OfficerReportClickerState = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.numericUpDown_AutoClickerStartDelaySec = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.label10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.numericUpDown_AutoClickerDelayMs = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.label8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.button_Misc_StartAutoOfficerOthers = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.button_Misc_StartAutoOfficerShares = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.button_Misc_SelectOthersOfficerReport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.textBox_Misc_SelectOthersOfficerReport = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.button_Misc_SelectSharesOffcerReport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.textBox_Misc_SelectSharesOfficerReport = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.backgroundWorker_OfficerReportFiller = new System.ComponentModel.BackgroundWorker();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_OfficerReportClickerState
            // 
            this.label_OfficerReportClickerState.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label_OfficerReportClickerState.Location = new System.Drawing.Point(3, 406);
            this.label_OfficerReportClickerState.Name = "label_OfficerReportClickerState";
            this.label_OfficerReportClickerState.Size = new System.Drawing.Size(170, 24);
            this.label_OfficerReportClickerState.TabIndex = 31;
            this.label_OfficerReportClickerState.Values.Text = "До запуска осталось ...";
            // 
            // numericUpDown_AutoClickerStartDelaySec
            // 
            this.numericUpDown_AutoClickerStartDelaySec.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.numericUpDown_AutoClickerStartDelaySec.Location = new System.Drawing.Point(3, 356);
            this.numericUpDown_AutoClickerStartDelaySec.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_AutoClickerStartDelaySec.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDown_AutoClickerStartDelaySec.Name = "numericUpDown_AutoClickerStartDelaySec";
            this.numericUpDown_AutoClickerStartDelaySec.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown_AutoClickerStartDelaySec.TabIndex = 30;
            this.numericUpDown_AutoClickerStartDelaySec.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(135, 282);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 24);
            this.label10.TabIndex = 29;
            this.label10.Values.Text = "мс";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(129, 356);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 24);
            this.label9.TabIndex = 28;
            this.label9.Values.Text = "сек";
            // 
            // numericUpDown_AutoClickerDelayMs
            // 
            this.numericUpDown_AutoClickerDelayMs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.numericUpDown_AutoClickerDelayMs.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_AutoClickerDelayMs.Location = new System.Drawing.Point(3, 277);
            this.numericUpDown_AutoClickerDelayMs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_AutoClickerDelayMs.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDown_AutoClickerDelayMs.Name = "numericUpDown_AutoClickerDelayMs";
            this.numericUpDown_AutoClickerDelayMs.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown_AutoClickerDelayMs.TabIndex = 27;
            this.numericUpDown_AutoClickerDelayMs.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(3, 329);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(279, 24);
            this.label8.TabIndex = 26;
            this.label8.Values.Text = "Время на фокусирование Справки БК";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(3, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 24);
            this.label7.TabIndex = 25;
            this.label7.Values.Text = "Задержка автокликера";
            // 
            // button_Misc_StartAutoOfficerOthers
            // 
            this.button_Misc_StartAutoOfficerOthers.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.button_Misc_StartAutoOfficerOthers.Location = new System.Drawing.Point(681, 173);
            this.button_Misc_StartAutoOfficerOthers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Misc_StartAutoOfficerOthers.Name = "button_Misc_StartAutoOfficerOthers";
            this.button_Misc_StartAutoOfficerOthers.Size = new System.Drawing.Size(149, 44);
            this.button_Misc_StartAutoOfficerOthers.TabIndex = 24;
            this.button_Misc_StartAutoOfficerOthers.Values.Text = "Запуск";
            this.button_Misc_StartAutoOfficerOthers.Visible = false;
            // 
            // button_Misc_StartAutoOfficerShares
            // 
            this.button_Misc_StartAutoOfficerShares.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.button_Misc_StartAutoOfficerShares.Location = new System.Drawing.Point(681, 74);
            this.button_Misc_StartAutoOfficerShares.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Misc_StartAutoOfficerShares.Name = "button_Misc_StartAutoOfficerShares";
            this.button_Misc_StartAutoOfficerShares.Size = new System.Drawing.Size(149, 44);
            this.button_Misc_StartAutoOfficerShares.TabIndex = 23;
            this.button_Misc_StartAutoOfficerShares.Values.Text = "Запуск";
            this.button_Misc_StartAutoOfficerShares.Click += new System.EventHandler(this.button_Misc_StartAutoOfficerReport_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(1, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 24);
            this.label6.TabIndex = 22;
            this.label6.Values.Text = "Другие бумаги";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(385, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 24);
            this.label5.TabIndex = 21;
            this.label5.Values.Text = "Отчет для госслужащих";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(3, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 24);
            this.label4.TabIndex = 20;
            this.label4.Values.Text = "Акции из отчета ВТБ";
            // 
            // button_Misc_SelectOthersOfficerReport
            // 
            this.button_Misc_SelectOthersOfficerReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.button_Misc_SelectOthersOfficerReport.Location = new System.Drawing.Point(482, 173);
            this.button_Misc_SelectOthersOfficerReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Misc_SelectOthersOfficerReport.Name = "button_Misc_SelectOthersOfficerReport";
            this.button_Misc_SelectOthersOfficerReport.Size = new System.Drawing.Size(149, 44);
            this.button_Misc_SelectOthersOfficerReport.TabIndex = 19;
            this.button_Misc_SelectOthersOfficerReport.Values.Text = ". . .";
            this.button_Misc_SelectOthersOfficerReport.Visible = false;
            // 
            // textBox_Misc_SelectOthersOfficerReport
            // 
            this.textBox_Misc_SelectOthersOfficerReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.textBox_Misc_SelectOthersOfficerReport.Location = new System.Drawing.Point(3, 180);
            this.textBox_Misc_SelectOthersOfficerReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Misc_SelectOthersOfficerReport.Name = "textBox_Misc_SelectOthersOfficerReport";
            this.textBox_Misc_SelectOthersOfficerReport.Size = new System.Drawing.Size(461, 27);
            this.textBox_Misc_SelectOthersOfficerReport.TabIndex = 18;
            this.textBox_Misc_SelectOthersOfficerReport.Visible = false;
            // 
            // button_Misc_SelectSharesOffcerReport
            // 
            this.button_Misc_SelectSharesOffcerReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.button_Misc_SelectSharesOffcerReport.Location = new System.Drawing.Point(482, 74);
            this.button_Misc_SelectSharesOffcerReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Misc_SelectSharesOffcerReport.Name = "button_Misc_SelectSharesOffcerReport";
            this.button_Misc_SelectSharesOffcerReport.Size = new System.Drawing.Size(149, 44);
            this.button_Misc_SelectSharesOffcerReport.TabIndex = 17;
            this.button_Misc_SelectSharesOffcerReport.Values.Text = ". . .";
            this.button_Misc_SelectSharesOffcerReport.Click += new System.EventHandler(this.button_Misc_SelectGovReport_Click);
            // 
            // textBox_Misc_SelectSharesOfficerReport
            // 
            this.textBox_Misc_SelectSharesOfficerReport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.textBox_Misc_SelectSharesOfficerReport.Location = new System.Drawing.Point(3, 81);
            this.textBox_Misc_SelectSharesOfficerReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Misc_SelectSharesOfficerReport.Name = "textBox_Misc_SelectSharesOfficerReport";
            this.textBox_Misc_SelectSharesOfficerReport.Size = new System.Drawing.Size(461, 27);
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
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.numericUpDown_AutoClickerDelayMs);
            this.kryptonPanel1.Controls.Add(this.label_OfficerReportClickerState);
            this.kryptonPanel1.Controls.Add(this.textBox_Misc_SelectSharesOfficerReport);
            this.kryptonPanel1.Controls.Add(this.numericUpDown_AutoClickerStartDelaySec);
            this.kryptonPanel1.Controls.Add(this.button_Misc_SelectSharesOffcerReport);
            this.kryptonPanel1.Controls.Add(this.label10);
            this.kryptonPanel1.Controls.Add(this.textBox_Misc_SelectOthersOfficerReport);
            this.kryptonPanel1.Controls.Add(this.label9);
            this.kryptonPanel1.Controls.Add(this.button_Misc_SelectOthersOfficerReport);
            this.kryptonPanel1.Controls.Add(this.label4);
            this.kryptonPanel1.Controls.Add(this.label8);
            this.kryptonPanel1.Controls.Add(this.label5);
            this.kryptonPanel1.Controls.Add(this.label7);
            this.kryptonPanel1.Controls.Add(this.label6);
            this.kryptonPanel1.Controls.Add(this.button_Misc_StartAutoOfficerOthers);
            this.kryptonPanel1.Controls.Add(this.button_Misc_StartAutoOfficerShares);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(836, 504);
            this.kryptonPanel1.TabIndex = 32;
            // 
            // OfficerReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "OfficerReportControl";
            this.Size = new System.Drawing.Size(836, 504);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel label_OfficerReportClickerState;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numericUpDown_AutoClickerStartDelaySec;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label10;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label9;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numericUpDown_AutoClickerDelayMs;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label8;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label7;
        private ComponentFactory.Krypton.Toolkit.KryptonButton button_Misc_StartAutoOfficerOthers;
        private ComponentFactory.Krypton.Toolkit.KryptonButton button_Misc_StartAutoOfficerShares;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton button_Misc_SelectOthersOfficerReport;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox textBox_Misc_SelectOthersOfficerReport;
        private ComponentFactory.Krypton.Toolkit.KryptonButton button_Misc_SelectSharesOffcerReport;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox textBox_Misc_SelectSharesOfficerReport;
        private System.ComponentModel.BackgroundWorker backgroundWorker_OfficerReportFiller;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
    }
}
