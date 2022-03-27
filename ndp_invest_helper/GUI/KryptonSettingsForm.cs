using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI
{
    public partial class KryptonSettingsForm : Form
    {
        private static (CommonDataSources enumValue, string menuItemText)[]
            CommonDataSourceMenuItems = new []
            {
                (CommonDataSources.SqliteDb, "База Sqlite"),
                (CommonDataSources.XmlFiles, "Файлы XML")
            };

        private static (PortfolioDifferenceSource enumValue, string menuItemText)[]
            PortfolioDifferenceSourceMenuItems = new []
            {
                (PortfolioDifferenceSource.LastDeal, "Последняя сделка"),
                (PortfolioDifferenceSource.Origin, "Исходный портфель")
            };

        public bool DataReloadRequired { get; set; } = false;

        public bool ShowChangesFromChanged { get; set; } = false;

        public KryptonSettingsForm()
        {
            InitializeComponent();
        }

        private void KryptonSettingsForm_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitCash()
        {
            ColumnCurrencyType.DataSource =
                CurrenciesManager.Currencies
                .Select(x => x.Code)
                .ToList();

            foreach (var item in Settings.Cash)
            {
                dataGrid_Cash.Rows.Add(item.Key, item.Value);
            }
        }

        private void InitControls()
        {
            InitCash();

            checkBox_Log.Checked = Settings.WriteLog;

            // Fill menu items for common data sources.
            comboBox_DataSource.Items.AddRange
                (CommonDataSourceMenuItems.Select(x => x.menuItemText).ToArray());

            // Select menu item based on settings.
            comboBox_DataSource.SelectedItem =
                CommonDataSourceMenuItems.First
                (x => x.enumValue == Settings.CommonDataSource)
                .menuItemText;

            // Same with PortfolioDifferenceSource.
            comboBox_ShowChangesFrom.Items.AddRange
                (PortfolioDifferenceSourceMenuItems.Select(x => x.menuItemText).ToArray());

            // Select menu item based on settings.
            comboBox_ShowChangesFrom.SelectedItem =
                   PortfolioDifferenceSourceMenuItems.First
                   (x => x.enumValue == Settings.ShowDifferenceFrom)
                   .menuItemText;
        }

        private void SaveCash()
        {
            var cash = new List<KeyValuePair<string, decimal>>();

            foreach (DataGridViewRow item in dataGrid_Cash.Rows)
            {
                if (
                    string.IsNullOrEmpty(item.Cells[0].Value as string) ||
                    item.Cells[1].Value == null )
                {
                    continue;
                }

                cash.Add(
                    new KeyValuePair<string, decimal>(
                        (string)item.Cells[0].Value,
                        decimal.Parse(item.Cells[1].Value.ToString()))
                );
            }

            Settings.Cash = cash;
        }

        private void KryptonSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveCash();
        }

        private void comboBox_DataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataReloadRequired = true;

            Settings.CommonDataSource = CommonDataSourceMenuItems
                [comboBox_DataSource.SelectedIndex].enumValue;
        }

        private void checkBox_Log_CheckedChanged(object sender, EventArgs e)
        {
            Settings.WriteLog = checkBox_Log.Checked;
        }

        private void comboBox_ShowChangesFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.ShowDifferenceFrom = PortfolioDifferenceSourceMenuItems
                [comboBox_ShowChangesFrom.SelectedIndex].enumValue;
        }
    }
}
