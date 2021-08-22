using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ndp_invest_helper
{
    public partial class MainForm : Form
    {
        private List<GrouppingResults> grouppingResults = new List<GrouppingResults>();

        private StringBuilder log = new StringBuilder();

        public MainForm()
        {
            InitializeComponent();
        }

        #region Properties

        public Portfolio FirstPortfolio {  get => grouppingResults[0].Portfolio; }

        public Portfolio CurrentPortfolio { get => grouppingResults.Last().Portfolio; }

        public GrouppingResults FirstResult { get => grouppingResults[0]; }

        public GrouppingResults CurrentResult { get => grouppingResults.Last(); }

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

        private void LoadXmlData()
        {
            var settings = Settings.Instance;

            CurrenciesManager.SetRates(settings);

            CountriesManager.ParseXmlFile(settings.Files.CountriesInfo);
            SectorsManager.ParseXmlFile(settings.Files.SectorsInfo);
            SecuritiesManager.ParseXmlFile(settings.Files.SecuritiesInfo);
        }

        private void LoadPortfolio()
        {
            var portfolio = new Portfolio();
            var reports = HandleReportsDirectory(Settings.Instance.Files.ReportsDir);
            foreach (var report in reports)
                portfolio.AddReport(report);

            grouppingResults.Add(new GrouppingResults(portfolio));
        }

        private List<Report> HandleReportsDirectory(string directoryPath)
        {
            var cashReport = new CashReport();
            cashReport.ParseXmlFile(directoryPath + "\\cash.xml");

            var result = new List<Report>();
            result.Add(cashReport);

            foreach (var reportFile in Directory.GetFiles(directoryPath + "\\vtb"))
            {
                var vtbReport = new VtbReport();
                vtbReport.ParseXmlFile(reportFile);
                result.Add(vtbReport);
            }

            return result;
        }

        private void Log(string text)
        {
            if (Settings.WriteLog)
            {
                log.AppendLine(text);
                log.AppendLine();
            }
        }

        private void FillDataGridView(
            DataGridView dataGridView, 
            PortfolioAnalyticsResult newResult,
            PortfolioAnalyticsResult oldResult,
            Dictionary<string, string> keyFriendlyNames)
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

            Log(newResult.ToString());
        }

        private void FillGroupControls()
        {
            var sectorNames = SectorsManager.ById.Keys.ToDictionary(
                x => x,
                x => SectorsManager.ById[x].Name);

            GrouppingResults oldResult = FirstResult;

            if (grouppingResults.Count > 1)
            {
                switch (Settings.Instance.Options.ShowDifferenceFrom)
                {
                    case "origin":
                        oldResult = FirstResult;
                        break;

                    case "last_deal":
                        // Предпоследний результат.
                        oldResult = grouppingResults[grouppingResults.Count - 2];
                        break;

                    default:
                        throw new ArgumentException(
                            "В settings.xml для параметра show_difference_from" +
                            " указано недопустимоео значение" +
                            Settings.Instance.Options.ShowDifferenceFrom);

                }
            }

            FillDataGridView(dataGridView_GroupsByCountry,
                CurrentResult.ByCountry, oldResult.ByCountry, CountriesManager.Countries);

            FillDataGridView(dataGridView_GroupsByCurrency,
                CurrentResult.ByCurrency, oldResult.ByCurrency, null);

            FillDataGridView(dataGridView_GroupsBySector,
                CurrentResult.BySector, oldResult.BySector, sectorNames);

            FillDataGridView(dataGridView_GroupsByType,
                CurrentResult.ByType, oldResult.ByType,
                SecuritiesManager.SecTypeFriendlyNames);
        }

        private void FillBuySellCombos()
        {
            comboBox_BuySell_Security.DataSource = SecuritiesManager.Securities;
            comboBox_BuySell_Currency.DataSource = CurrenciesManager.CurrencyRates.Keys.ToList();
        }

        private void ExecuteTasks()
        {
            var taskFilePath = "task.xml"; 
            var taskManager = new TaskManager(FirstPortfolio);
            var taskOutput = new StringBuilder();
            taskManager.ParseXmlFile(taskFilePath, taskOutput);
            File.WriteAllText(Settings.Instance.Files.TaskOutput, taskOutput.ToString());
            toolStripStatusLabel1.Text = "Задание выполнено, результаты записаны в output.txt";
        }

        #endregion

        #region Event handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripMenuItem_Log.Checked = Settings.WriteLog;

            LoadXmlData();
            LoadPortfolio();
            FillGroupControls();
            FillBuySellCombos();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Settings.WriteLog)
                File.WriteAllText(Settings.LogFile, log.ToString());
        }

        private void dataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            var analyticsResult = dataGridView.Tag as PortfolioAnalyticsResult;
            var key = dataGridView.Rows[e.RowIndex].Tag as string;

            if (key == null)
                return;

            var securities = analyticsResult.Analytics[key].Portfolio.Securities.Keys.ToList();
            securities.Sort((x, y) => x.BestFriendlyName.CompareTo(y.BestFriendlyName));

            listBox_GroupStocks.Items.Clear();
            listBox_GroupStocks.Items.AddRange(securities.ToArray());
        }

        #region Right part

        private void listBox_GroupStocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_BuySell_Security.SelectedItem = listBox_GroupStocks.SelectedItem;
        }

        private void listBox_Deals_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: show groupping after selected deal.
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

            grouppingResults.Add(new GrouppingResults(newPortfolio));

            FillGroupControls();

            listBox_Deals.Items.Add(deal);

            var logText = string.Format(
                "{0} {1} {2} шт. по цене {3:n2} {4} на сумму {5:n2} {4}.",
                sender == buttonBuy ? "Покупка" : "Продажа",
                deal.Security.BestUniqueFriendlyName, deal.Count, deal.Price, 
                deal.Currency, deal.Total
                );

            Log(logText);
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
            ExecuteTasks();
        }

        private void toolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "ndp_invest_helper - бесплатный анализатор диверсификации портфеля с открытым кодом.\n\n" + 
                "Автор - Незамаев Дмитрий (dnezamaev@gmail.com).\n\n" +
                "Подробное описание в файле README.txt.\n\n" +
                "Лицензия GPL3.");
        }

        private void toolStripMenuItem_Log_CheckStateChanged(object sender, EventArgs e)
        {
            Settings.WriteLog = toolStripMenuItem_Log.Checked;
        }

        private void toolStripMenuItem_CancelDeal_Click(object sender, EventArgs e)
        {
            if (grouppingResults.Count < 2)
                return;

            grouppingResults.RemoveAt(grouppingResults.Count - 1);

            listBox_Deals.Items.RemoveAt(listBox_Deals.Items.Count - 1);

            FillGroupControls();
        }

        #endregion

        #endregion
    }
}
