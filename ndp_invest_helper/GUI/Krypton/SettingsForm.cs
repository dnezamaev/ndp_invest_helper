using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI.Krypton
{
    public partial class SettingsForm : ComponentFactory.Krypton.Toolkit.KryptonForm
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

        public bool AnalyticsFormsReloadRequired { get; set; } = false;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
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

        private void comboBox_DataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentValue = Settings.CommonDataSource;
            var selectedValue = CommonDataSourceMenuItems
                [comboBox_DataSource.SelectedIndex].enumValue;

            if (currentValue != selectedValue)
            {
                DataReloadRequired = true;
                Settings.CommonDataSource = selectedValue;
            }
        }

        private void checkBox_Log_CheckedChanged(object sender, EventArgs e)
        {
            Settings.WriteLog = checkBox_Log.Checked;
        }

        private void comboBox_ShowChangesFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentValue = Settings.ShowDifferenceFrom;
            var selectedValue = PortfolioDifferenceSourceMenuItems
                [comboBox_ShowChangesFrom.SelectedIndex].enumValue;

            if (currentValue != selectedValue)
            {
                AnalyticsFormsReloadRequired = true;
                Settings.ShowDifferenceFrom = selectedValue;
            }
        }
    }
}
