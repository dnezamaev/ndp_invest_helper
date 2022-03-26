using ComponentFactory.Krypton.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI
{
    public partial class PortfolioAnalyticsControl : UserControl
    {
        private InvestManager investManager;

        public PortfolioAnalyticsControl(InvestManager investManager)
        {
            InitializeComponent();

            this.investManager = investManager;
        }

        public event Action<Portfolio> CurrentGroupItemChanged;

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

            if (CurrentGroupItemChanged != null)
                CurrentGroupItemChanged(portfolio);
        }

        /// <summary>
        /// Обработчик выделения строк в таблице с группами.
        /// </summary>
        private void dataGridView_Group_SelectionChanged(object sender, EventArgs e)
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
            //richTextBox_Info.Text = $"Выделенные: {partsSum:0.00}%";
        }

        public void FillGroupControls()
        {
            var grouppingResults = investManager.GrouppingResults;
            var currentResult = investManager.CurrentResult;

            var sectorNames = SectorsManager.ById.Keys.ToDictionary(
                x => x,
                x => SectorsManager.ById[x].Name);

            GrouppingResults oldResult = investManager.FirstResult;

            if (investManager.GrouppingResults.Count > 1)
            {
                switch (Settings.ShowDifferenceFrom)
                {
                    case PortfolioDifferenceSource.Origin:
                        oldResult = investManager.FirstResult;
                        break;

                    case PortfolioDifferenceSource.LastDeal:
                        // Предпоследний результат.
                        oldResult = grouppingResults[grouppingResults.Count - 2];
                        break;

                    default:
                        throw new ArgumentException(
                            "Для параметра ShowDifferenceFrom " +
                            "указано недопустимое значение " +
                            Settings.ShowDifferenceFrom);

                }
            }

            FillGroupsDataGridView(dataGridView_GroupsByCountry,
                currentResult.ByCountry, oldResult.ByCountry, CountriesManager.Countries);

            FillGroupsDataGridView(dataGridView_GroupsByCurrency,
                currentResult.ByCurrency, oldResult.ByCurrency, null);

            FillGroupsDataGridView(dataGridView_GroupsBySector,
                currentResult.BySector, oldResult.BySector, sectorNames);

            FillGroupsDataGridView(dataGridView_GroupsByType,
                currentResult.ByType, oldResult.ByType,
                SecuritiesManager.SecTypeFriendlyNames);

            // Select first row and raise events.
            if (dataGridView_GroupsByCountry.Rows.Count != 0)
            {
                dataGridView_GroupsByCountry.Rows[0].Selected = true;

                dataGridView_Group_RowEnter(
                    dataGridView_GroupsByCountry,
                    new DataGridViewCellEventArgs(0, 0)
                    );
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

            //LogAddText(newResult.ToString());
        }
   }
 
    public class PortfolioAnalyticsPage : KryptonPage
    {
        public PortfolioAnalyticsControl Control { get; set; }

        public PortfolioAnalyticsPage(InvestManager investManager)
        {
            Control = new PortfolioAnalyticsControl(investManager);
            Control.Dock = DockStyle.Fill;
            Controls.Add(Control);
        }
    }
}
