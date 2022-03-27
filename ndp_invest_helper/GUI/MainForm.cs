using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ndp_invest_helper.Models;
using ndp_invest_helper.ReportHandlers;

namespace ndp_invest_helper
{
    public partial class MainForm : Form
    {
        private InvestManager investManager;

        public MainForm()
        {
            InitializeComponent();

            investManager = new InvestManager();
            investManager.LoadCommonData();
        }


        private static (CommonDataSources enumValue, string menuItemText)[]
            CommonDataSourceMenuItems = new []
            {
                (CommonDataSources.SqliteDb, "База Sqlite"),
                (CommonDataSources.XmlFiles, "Файлы XML")
            };

        #region Properties


        private List<GrouppingResults> GrouppingResults
        {
            get => investManager.GrouppingResults;
        }

        public Portfolio FirstPortfolio {  get => GrouppingResults[0].Portfolio; }

        public Portfolio CurrentPortfolio { get => GrouppingResults.Last().Portfolio; }

        public GrouppingResults FirstResult { get => GrouppingResults[0]; }

        public GrouppingResults CurrentResult { get => GrouppingResults.Last(); }

        /// <summary>
        /// Выбранная в comboBox_BuySellSecurity бумага.
        /// </summary>
        public Security SelectedSecurity
        {
            get { return comboBox_BuySell_Security.SelectedValue as Security; }
        }

        /// <summary>
        /// Информация о выбранной в comboBox_BuySellSecurity бумаге из портфеля.
        /// Либо null, если такой бумаги в портфеле CurrentPortfolio нет.
        /// </summary>
        public SecurityInfo SelectedSecurityInfo
        {
            get
            {
                if (SelectedSecurity == null)
                    return null;

                SecurityInfo secInfo;
                CurrentPortfolio.Securities.TryGetValue(SelectedSecurity, out secInfo);
                return secInfo;
            }
        }

        /// <summary>
        /// Выбранная в comboBox_BuySell_Currency валюта.
        /// </summary>
        public string SelectedCurrency 
        { 
            get => comboBox_BuySell_Currency.SelectedItem.ToString(); 
        }

        #endregion

        #region Methods

        private void FillFilter()
        {
            var portfolio = CurrentPortfolio;

            foreach (var security in portfolio.Securities)
            {
                listView_Filter.Items.Add(new ListViewItem
                {
                    Tag = security.Key,
                    Text = security.Key.BestUniqueFriendlyName,
                    Checked = true,
                });
            }
        }

        /// <summary>
        /// Заполняет таблицы группировки по разным критериям.
        /// </summary>
        private void FillGroupsDataGridView 
        (
            DataGridView dataGridView, 
            PortfolioAnalyticsResult newResult,
            PortfolioAnalyticsResult oldResult,
            Dictionary<string, string> keyFriendlyNames
        )
        {
            dataGridView.Rows.Clear();

            // Аналитика хранится в Tag у DataGridView для удобства.
            dataGridView.Tag = newResult;

            foreach (var kvp in newResult.Analytics)
            {
                var rowIndex = dataGridView.Rows.Add();
                var row = dataGridView.Rows[rowIndex];

                var key = kvp.Key;
                var value = kvp.Value.Part;
                var diff = 0M;

                var friendlyKey = key;

                if (keyFriendlyNames != null && keyFriendlyNames.ContainsKey(key))
                    friendlyKey = keyFriendlyNames[key];

                // Если это обновленная аналитика и в старой был такой же ключ,
                // то подкрашиваем строки цветом:
                // зеленая при увеличении доли, красная при уменьшении.
                if (oldResult != null && oldResult.Analytics.ContainsKey(key))
                {
                    var oldValue = oldResult.Analytics[key].Part;
                    diff = value - oldValue;

                    if (diff > 0)
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    else if (diff < 0)
                            row.DefaultCellStyle.BackColor = Color.LightPink;
                }

                row.SetValues(friendlyKey, value * 100, diff * 100);
                row.Tag = kvp.Key;
            }

            dataGridView.Sort(
                dataGridView.Columns[1], ListSortDirection.Descending);

            dataGridView.ClearSelection();

            LogAddText(newResult.ToString());
        }

        private void FillGroupControls()
        {
            var sectorNames = SectorsManager.ById.Keys.ToDictionary(
                x => x,
                x => SectorsManager.ById[x].Name);

            GrouppingResults oldResult = FirstResult;

            if (GrouppingResults.Count > 1)
            {
                switch (Settings.ShowDifferenceFrom)
                {
                    case PortfolioDifferenceSource.Origin:
                        oldResult = FirstResult;
                        break;

                    case PortfolioDifferenceSource.LastDeal:
                        // Предпоследний результат.
                        oldResult = GrouppingResults[GrouppingResults.Count - 2];
                        break;

                    default:
                        throw new ArgumentException(
                            "Для параметра ShowDifferenceFrom " +
                            "указано недопустимое значение " +
                            Settings.ShowDifferenceFrom);

                }
            }

            FillGroupsDataGridView(dataGridView_GroupsByCountry,
                CurrentResult.ByCountry, oldResult.ByCountry, CountriesManager.Countries);

            FillGroupsDataGridView(dataGridView_GroupsByCurrency,
                CurrentResult.ByCurrency, oldResult.ByCurrency, null);

            FillGroupsDataGridView(dataGridView_GroupsBySector,
                CurrentResult.BySector, oldResult.BySector, sectorNames);

            FillGroupsDataGridView(dataGridView_GroupsByType,
                CurrentResult.ByType, oldResult.ByType,
                SecuritiesManager.SecTypeFriendlyNames);
        }

        private void FillBuySellCombos()
        {
            comboBox_BuySell_Security.DataSource = SecuritiesManager.Securities;
            comboBox_BuySell_Currency.DataSource = CurrenciesManager.RatesToRub.Keys.ToList();
        }

        private void FillDbEditorCombo()
        {
            comboBox_DbEditor_Security.DataSource = SecuritiesManager.Securities;
        }

        private void HandleBadReportSecurities()
        {
            var unknownSecurities = investManager.UnknownSecurities;
            var incompleteSecurities = investManager.IncompleteSecurities;

            var sb = new StringBuilder();

            if (unknownSecurities.Count != 0)
            {
                sb.AppendLine("Найдены неизвестные бумаги, они будут проигнорированы.");
                foreach (var security in unknownSecurities)
                {
                    sb.AppendLine(security.BestUniqueFriendlyName);
                }
            }

            if (incompleteSecurities.Count != 0)
            {
                sb.AppendLine("Найдены недозаполненные бумаги, они будут проигнорированы.");
                foreach (var security in incompleteSecurities)
                {
                    sb.AppendLine(security.BestUniqueFriendlyName);
                }
            }

            if (unknownSecurities.Count != 0 || incompleteSecurities.Count != 0)
            {
                sb.AppendLine("Рекомендуется дополнить базу Securities.xml.");
                richTextBox_Log.Text += sb.ToString();
                richTextBox_Log.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "!!! ВНИМАНИЕ: обнаружены ошибки, подробности в логе справа.";
                tabControl_Right.SelectedTab = tabPage_Messages;
            }
        }

        private void LogAddText(string text)
        {

        }

        #endregion

        #region Event handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBox_Misc_SelectSharesOfficerReport.Text 
                = @"D:\Job\другое\налоги\декларация_доходов\2021\акции_ВТБ.txt";
            tabControl_Main.SelectedIndex = 2;
            numericUpDown_AutoClickerStartDelaySec.Value = 5;
            button_Misc_StartAutoOfficerReport_Click(button_Misc_StartAutoOfficerShares, null);

            toolStripMenuItem_Log.Checked = Settings.WriteLog;

            // Fill menu items for common data sources.
            toolStripComboBox_CommonDataSource.Items.AddRange
                (CommonDataSourceMenuItems.Select(x => x.menuItemText).ToArray());

            // Select menu item based on settings.
            toolStripComboBox_CommonDataSource.SelectedItem =
                CommonDataSourceMenuItems.First
                (x => x.enumValue == Settings.CommonDataSource)
                .menuItemText;

            //tabControl_Main.SelectedTab = tabPage_Main_DbEditor;

            //try
            //{
                investManager.LoadPortfolio();

                HandleBadReportSecurities();
                FillFilter();
                FillGroupControls();
                FillBuySellCombos();
                FillDbEditorCombo();
//            }
//            catch (Exception exc)
//            {
//                MessageBox.Show(exc.Message, "Произошла ошибка.");
//                Log(exc.ToString());
//                Close();
//            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            investManager.LogSave();

            Settings.Save();
        }

        /// <summary>
        /// Обработчик выбора строки в таблице с группами.
        /// </summary>
        private void dataGridView_Group_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            var analyticsResult = dataGridView.Tag as PortfolioAnalyticsResult;
            var key = dataGridView.Rows[e.RowIndex].Tag as string;

            if (key == null)
                return;

            var portfolio = analyticsResult.Analytics[key].Portfolio;
            var securities = portfolio.Securities.ToList();

            dataGridView_GroupContent.Tag = dataGridView;
            dataGridView_GroupContent.Rows.Clear();

            // Заполняем таблицу с составом группы.
            foreach (var security in securities)
            {
                var rowIndex = dataGridView_GroupContent.Rows.Add();
                var row = dataGridView_GroupContent.Rows[rowIndex];

                row.SetValues(
                    security.Key.BestUniqueFriendlyName,
                    security.Value.Total / portfolio.Total * 100);
                row.Tag = security;
            }

            dataGridView_GroupContent.Sort(
                dataGridView_GroupContent.Columns[1], ListSortDirection.Descending);

            dataGridView_GroupContent.ClearSelection();
        }

        #region Right part

        private void dataGridView_GroupContent_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            var rowTag = dataGridView.Rows[e.RowIndex].Tag;

            if (rowTag == null)
                return;

            var security = ((KeyValuePair<Security, SecurityInfo>)rowTag).Key;
            comboBox_BuySell_Security.SelectedItem = security;
        }

        private void listBox_Deals_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: show groupping after selected deal.
        }

        private void comboBox_DbEditor_Security_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void linkLabel_DbEditor_Countries_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        /// <summary>
        /// Обработчик выделения строк в таблице с группами.
        /// </summary>
        private void dataGridView_Groups_SelectionChanged(object sender, EventArgs e)
        {
            var dataGridView = sender as DataGridView;

            // Считаем сумму по выделенным группам.
            decimal partsSum = 0;
            foreach (DataGridViewRow selectedRow in dataGridView.SelectedRows)
            {
                if (selectedRow.Cells[1].Value == null)
                    return; // Обработчик вызван до заполнения таблицы.

                partsSum += (decimal)selectedRow.Cells[1].Value;
            }
            richTextBox_Info.Text = $"Выделенные: {partsSum:0.00}%";
        }

        private void toolStripComboBox_CommonDataSource_SelectedIndexChanged
            (object sender, EventArgs e)
        {
            Settings.CommonDataSource = CommonDataSourceMenuItems
                [toolStripComboBox_CommonDataSource.SelectedIndex].enumValue;

            investManager.LoadCommonData();
        }

        private void toolStripMenuItem_XmlToSqlite_Click(object sender, EventArgs e)
        {
            investManager.XmlToSqlite();
        }
        #endregion

        #region Buy/Sell

        private void buttonBuySell_Click(object sender, EventArgs e)
        {
            if (sender != buttonBuy && sender != buttonSell)
                throw new ArgumentException("Unknown button");

            var deal = new Deal()
            {
                Security = (Security)comboBox_BuySell_Security.SelectedValue,
                Price = numericUpDown_BuySell_Price.Value,
                Currency = (string)comboBox_BuySell_Currency.SelectedValue,
                Buy = sender == buttonBuy,
                UseCash = true
            };

            var countDecimal = numericUpDown_BuySell_Count.Value;

            if ( (countDecimal % 1) != 0 )
            {
                MessageBox.Show(string.Format(
                    "Количество должно быть целым числом, а указано {0}", countDecimal));
            }

            deal.Count = (ulong)countDecimal;

            var newPortfolio = new Portfolio(CurrentPortfolio);
            newPortfolio.MakeDeal(deal);

            GrouppingResults.Add(new GrouppingResults(newPortfolio));

            FillGroupControls();

            listBox_Deals.Items.Add(deal);

            var logText = string.Format(
                "{0} {1} {2} шт. по цене {3:n2} {4} на сумму {5:n2} {4}.",
                sender == buttonBuy ? "Покупка" : "Продажа",
                deal.Security.BestUniqueFriendlyName, deal.Count, deal.Price, 
                deal.Currency, deal.Total
                );

            LogAddText(logText);
            toolStripStatusLabel1.Text = "Сделка совершена. " + logText;
        }

        private void comboBox_BuySellSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var secInfo = SelectedSecurityInfo;

            if (secInfo != null)
            {
                numericUpDown_BuySell_Price.Value = 
                    secInfo.PriceInCurrency(SelectedCurrency);

                numericUpDown_BuySell_Count.Value = secInfo.Count;
            }
        }

        private void numericUpDown_BuySell_Count_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown_BuySell_Total.Increment = numericUpDown_BuySell_Price.Value;

            numericUpDown_BuySell_Total.Value = 
                numericUpDown_BuySell_Count.Value * numericUpDown_BuySell_Price.Value;
        }

        private void comboBox_BuySell_Currency_SelectedIndexChanged(object sender, EventArgs e)
        {
            var secInfo = SelectedSecurityInfo;

            if (secInfo != null)
            {
                numericUpDown_BuySell_Price.Value = secInfo.PriceInCurrency(
                    comboBox_BuySell_Currency.SelectedItem.ToString());
            }
        }

        private void numericUpDown_BuySell_Total_ValueChanged(object sender, EventArgs e)
        {
            // Округляем количество в меньшую сторону до целого.
            numericUpDown_BuySell_Count.Value =
                (UInt64)(numericUpDown_BuySell_Total.Value 
                / numericUpDown_BuySell_Price.Value);

            // Пересчитываем Итого с учетом округленного количества.
            numericUpDown_BuySell_Total.Value =
                numericUpDown_BuySell_Price.Value 
                * numericUpDown_BuySell_Count.Value;
        }

        #endregion

        #region Main menu

        private void toolStripMenuItem_RunTask_Click(object sender, EventArgs e)
        {
            investManager.ExecuteTasks();
            toolStripStatusLabel1.Text = 
                $"Задание выполнено, результаты записаны в {Settings.TaskOutputFile}";
        }

        private void toolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "ndp_invest_helper - бесплатный анализатор диверсификации портфеля с открытым кодом.\n\n" + 
                "Автор - Незамаев Дмитрий (dnezamaev@gmail.com).\n\n" +
                "Подробное описание в файле README.txt.\n\n" +
                "Лицензия GPL3.\n\n" +
                "Версия " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
        }

        private void toolStripMenuItem_Log_CheckStateChanged(object sender, EventArgs e)
        {
            Settings.WriteLog = toolStripMenuItem_Log.Checked;
        }

        private void toolStripMenuItem_CancelDeal_Click(object sender, EventArgs e)
        {
            if (GrouppingResults.Count < 2)
                return;

            GrouppingResults.RemoveAt(GrouppingResults.Count - 1);

            listBox_Deals.Items.RemoveAt(listBox_Deals.Items.Count - 1);

            FillGroupControls();
        }

        #endregion

        #endregion

        private string SelectFileWithDialog
        (
            string filter = "Text files|*.txt|All files|*.*"
        )
        {
            var file_dialog = new OpenFileDialog();
            file_dialog.Filter = filter;

            if (file_dialog.ShowDialog() != DialogResult.OK)
                return null;

            return file_dialog.FileName;
        }

        private void button_Misc_SelectGovReport_Click(object sender, EventArgs e)
        {
            var selectedFile = SelectFileWithDialog();

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
}
