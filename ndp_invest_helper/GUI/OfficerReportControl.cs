using ComponentFactory.Krypton.Navigator;

using ndp_invest_helper.ReportHandlers;

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI
{
    public partial class OfficerReportControl : UserControl
    {
        public OfficerReportControl()
        {
            InitializeComponent();
        }

        private void button_Misc_SelectGovReport_Click(object sender, EventArgs e)
        {
            var selectedFile = Utils.SelectFileWithDialog();

            if (selectedFile == null)
                return;

            // TextBox where to store file path.
            TextBox textBoxFilePath;

            if (sender == button_Misc_SelectSharesOffcerReport)
            {
                textBoxFilePath = textBox_Misc_SelectSharesOfficerReport;
            }
            else if (sender == button_Misc_SelectOthersOfficerReport)
            {
                textBoxFilePath = textBox_Misc_SelectOthersOfficerReport;
            }
            else
                throw new NotImplementedException("Неизвестная кнопка.");

            textBoxFilePath.Text = selectedFile;
        }

        private int AutoClickerStartDelaySec
        {
            get => (int)numericUpDown_AutoClickerStartDelaySec.Value;
        }

        private int AutoClickerInputDelayMs
        {
            get => (int)numericUpDown_AutoClickerDelayMs.Value;
        }

        private void button_Misc_StartAutoOfficerReport_Click(object sender, EventArgs e)
        {
            if (sender == button_Misc_StartAutoOfficerOthers)
            {
                MessageBox.Show("Эта функция пока не реализована");
                return;
            }

            if (backgroundWorker_OfficerReportFiller.IsBusy) // already started
            {
                backgroundWorker_OfficerReportFiller.CancelAsync();
                return; // cancel working task
            }

            if 
            ( MessageBox.Show
                (
                $"После закрытия этого сообщения нажмите на любую ячейку " +
                $"в последней строке таблицы 5.1 в течение " +
                $"{AutoClickerStartDelaySec} сек.\n\n" +
                $"Вы готовы приступить?",
                "ВНИМАНИЕ", 
                MessageBoxButtons.YesNo
                )
                != DialogResult.Yes
            )
            {
                return; // user is not ready
            }

            (sender as Button).Text = "Отмена";

            backgroundWorker_OfficerReportFiller.RunWorkerAsync
                (
                new OfficerReportClicker
                    (
                    AutoClickerInputDelayMs,
                    AutoClickerStartDelaySec,
                    OnClickerProgress,
                    CheckTaskCanceled,
                    sender == button_Misc_StartAutoOfficerShares ? 
                        AutoClickerTask.Shares : 
                        AutoClickerTask.Others
                    )
                );
        }

        private void backgroundWorker_OfficerReportFiller_DoWork(object sender, DoWorkEventArgs e)
        {
            var clicker = e.Argument as OfficerReportClicker;
            e.Result = clicker; // clicker is result and argument

            var filePath = textBox_Misc_SelectSharesOfficerReport.Text;

            var officerReport = new VtbOfficerReport();
            officerReport.ParseFile(filePath);

            try
            {
                clicker.Start(officerReport);
            }
            catch (ClickerCancelException)
            {
                backgroundWorker_OfficerReportFiller.CancelAsync();
            }
        }

        private void backgroundWorker_OfficerReportFiller_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((AutoClickerStatus)e.UserState)
            {
                case AutoClickerStatus.FocusingWindow:
                    label_OfficerReportClickerState.Text =
                        $"До запуска осталось " +
                        $"{AutoClickerStartDelaySec - e.ProgressPercentage} сек.";
                    break;
                case AutoClickerStatus.Clicking:
                    label_OfficerReportClickerState.Text =
                        $"Заполнено бумаг: {e.ProgressPercentage}.";
                    break;
            }
        }

        private void backgroundWorker_OfficerReportFiller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var button =
                (e.Result as OfficerReportClicker).Task == AutoClickerTask.Shares ?
                button_Misc_StartAutoOfficerShares :
                button_Misc_StartAutoOfficerOthers;

            button.Text = "Запуск";

            if (e.Cancelled)
            {
                label_OfficerReportClickerState.Text = "Отмена";
                return;
            }

            MessageBox.Show("Заполнение завершено.");
        }

        private void OnClickerProgress(int progress, ReportHandlers.AutoClickerStatus status)
        {
            backgroundWorker_OfficerReportFiller.ReportProgress(progress, status);
        }

        private bool CheckTaskCanceled()
        {
            return backgroundWorker_OfficerReportFiller.CancellationPending;
        }
    }

    public class OfficerReportPage : KryptonPage
    {
        public OfficerReportControl Control { get; set; }

        public OfficerReportPage()
        {
            Control = new OfficerReportControl();
            Control.Dock = DockStyle.Fill;
            Controls.Add(Control);
        }
    }

}
