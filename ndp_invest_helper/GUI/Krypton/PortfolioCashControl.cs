using ComponentFactory.Krypton.Navigator;

using ndp_invest_helper.DataHandlers;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI.Krypton
{
    public partial class PortfolioCashControl : UserControl
    {
        public event Action CashChanged;

        public PortfolioCashControl()
        {
            InitializeComponent();
        }

        public void FillControls()
        {
            dataGrid_Cash.CellValueChanged -= dataGrid_Cash_CellValueChanged;

            ColumnCurrencyType.DataSource =
                CommonData.Currencies.Items
                .Select(x => x.Code)
                .ToList();

            dataGrid_Cash.Rows.Clear();

            foreach (var item in Settings.Cash)
            {
                dataGrid_Cash.Rows.Add(item.Key, item.Value);
            }

            dataGrid_Cash.CellValueChanged += dataGrid_Cash_CellValueChanged;
        }

        private bool IsRowValid(DataGridViewRow row)
        {
            var currency = row.Cells[0].Value as string;
            var amount = row.Cells[1].Value;

            return 
                !string.IsNullOrEmpty(currency) &&
                amount != null;
        }

        private bool SaveCash()
        {
            var cash = new List<KeyValuePair<string, decimal>>();

            // Ignore last row - used for adding new record.
            for (int i = 0; i < dataGrid_Cash.Rows.Count - 1; i++)
            {
                DataGridViewRow row = dataGrid_Cash.Rows[i];

                // Stop on invalid or partial input.
                if (!IsRowValid(row))
                {
                    return false;
                }

                cash.Add(
                    new KeyValuePair<string, decimal>(
                        (string)row.Cells[0].Value,
                        decimal.Parse(row.Cells[1].Value.ToString()))
                );
            }

            Settings.Cash = cash;

            return true;
        }

        private void dataGrid_Cash_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!SaveCash())
            {
                return;
            }

            if (CashChanged != null)
            {
                CashChanged();
            }
        }
    }

    public class PortfolioCashPage : KryptonPage
    {
        public PortfolioCashControl Control { get; set; }

        public PortfolioCashPage()
        {
            Control = new PortfolioCashControl();
            Control.Dock = DockStyle.Fill;
            Controls.Add(Control);
        }
    }
}
