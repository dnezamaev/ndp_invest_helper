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

        private Portfolio FirstPortfolio {  get => grouppingResults[0].Portfolio; }

        private Portfolio CurrentPortfolio { get => grouppingResults.Last().Portfolio; }

        private GrouppingResults FirstResult { get => grouppingResults[0]; }

        private GrouppingResults CurrentResult { get => grouppingResults.Last(); }

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripMenuItem_Log.Checked = Settings.WriteLog;

            LoadXmlData();
            LoadPortfolio();
            FillGroupControls();
            FillBuySellCombos();
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

        private void buttonBuySell_Click(object sender, EventArgs e)
        {
            var security = (Security)comboBox_BuySell_Security.SelectedValue;
            var price = numericUpDown_BuySell_Price.Value;
            var currency = (string)comboBox_BuySell_Currency.SelectedValue;
            var countDecimal = numericUpDown_BuySell_Count.Value;

            if ( (countDecimal % 1) != 0 )
            {
                MessageBox.Show(string.Format(
                    "Количество должно быть целым числом, а указано {0}", countDecimal));
            }

            var countUlong = (ulong)countDecimal;
            var total = price * countUlong;

            var newPortfolio = new Portfolio(CurrentPortfolio);

            if (sender == buttonBuy)
                newPortfolio.AddSecurity(security, countUlong, price, true, currency);
            else if (sender == buttonSell)
                newPortfolio.RemoveSecurity(security, countUlong, true, currency);
            else
                throw new ArgumentException("Unknown button");

            grouppingResults.Add(new GrouppingResults(newPortfolio));

            FillGroupControls();

            toolStripStatusLabel1.Text = "Сделка совершена.";

            Log(string.Format(
                "{0} {1} {2} шт. по цене {3:n2} на сумму {4:n2}.",
                sender == buttonBuy ? "Покупка" : "Продажа",
                security.BestFriendlyName, countUlong, price, total
                ));
        }

        private void comboBox_BuySellSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var security = (Security)comboBox_BuySell_Security.SelectedValue;
            SecurityInfo secInfo;

            if (CurrentPortfolio.Securities.TryGetValue(security, out secInfo))
            {
                numericUpDown_BuySell_Count.Value = secInfo.Count;
                numericUpDown_BuySell_Price.Value = secInfo.Price;
            }
        }

        private void numericUpDown_BuySell_Count_ValueChanged(object sender, EventArgs e)
        {
            FillBuySellTotalLabel();
        }

        private void FillBuySellTotalLabel()
        {
            label_BuySell_Total.Text = string.Format(
                "{0:n2} {1}",
                numericUpDown_BuySell_Count.Value * numericUpDown_BuySell_Price.Value,
                comboBox_BuySell_Currency.Text);
        }

        private void listBox_GroupStocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_BuySell_Security.SelectedItem = listBox_GroupStocks.SelectedItem;
        }

        private void toolStripMenuItem_RunTask_Click(object sender, EventArgs e)
        {
            ExecuteTasks();
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

        private void toolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "ndp_invest_helper - бесплатный анализатор диверсификации портфеля с открытым кодом.\n" + 
                "Автор - Незамаев Дмитрий (dnezamaev@gmail.com).\n" +
                "Подробное описание в файле README.txt.\n" +
                "Лицензия GPL3.");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Settings.WriteLog)
                File.WriteAllText(Settings.LogFile, log.ToString());
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

            FillGroupControls();
        }
    }
}
