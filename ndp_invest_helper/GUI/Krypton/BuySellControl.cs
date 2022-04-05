using ComponentFactory.Krypton.Navigator;
using ndp_invest_helper.DataHandlers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI.Krypton
{
    public partial class BuySellControl : UserControl
    {
        private InvestManager investManager;

        public BuySellControl(InvestManager investManager)
        {
            InitializeComponent();

            this.investManager = investManager;
        }

        public void FillControls()
        {
            comboBox_BuySell_Security.DataSource = SecuritiesManager.Securities;

            // Fill with currencies with rates.
            comboBox_BuySell_Currency.DataSource = null;
            comboBox_BuySell_Currency.DisplayMember = "FriendlyName";
            comboBox_BuySell_Currency.DataSource = CommonData.Currencies.RatesToRub.Keys.ToList();
        }

        /// <summary>
        /// Выбранная в comboBox_BuySellSecurity бумага.
        /// </summary>
        public Security SelectedSecurity
        {
            get 
            { 
                if (comboBox_BuySell_Security.Items.Count == 0
                    || comboBox_BuySell_Security.SelectedItem == null
                    )
                {
                    return null;
                }

                return comboBox_BuySell_Security.SelectedValue as Security;
            }
            set
            {
                comboBox_BuySell_Security.SelectedItem = value;
            }
        }

        /// <summary>
        /// Информация о выбранной в comboBox_BuySellSecurity бумаге из портфеля.
        /// Либо null, если такой бумаги в портфеле CurrentPortfolio нет.
        /// </summary>
        public SecurityInfo SelectedSecurityInfo
        {
            get
            {
                var security = SelectedSecurity;

                if (security == null)
                    return null;

                SecurityInfo secInfo;
                investManager.Analytics.CurrentPortfolio.Securities
                    .TryGetValue(security, out secInfo);

                return secInfo;
            }
        }

        /// <summary>
        /// Выбранная в comboBox_BuySell_Currency валюта.
        /// </summary>
        public Currency SelectedCurrency 
        {
            get
            {
                return (Currency)comboBox_BuySell_Currency.SelectedItem;
            }
        }

        private void buttonBuySell_Click(object sender, EventArgs e)
        {
            if (sender != buttonBuy && sender != buttonSell)
                throw new ArgumentException("Unknown button");

            var deal = new Deal()
            {
                Security = (Security)comboBox_BuySell_Security.SelectedValue,
                Price = numericUpDown_BuySell_Price.Value,
                Currency = comboBox_BuySell_Currency.SelectedValue as Currency,
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

            investManager.Analytics.MakeDeal(deal);
        }

        private void comboBox_BuySellSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var secInfo = SelectedSecurityInfo;
            var selected = SelectedCurrency;

            if (secInfo == null || selected == null)
            {
                return;
            }

            numericUpDown_BuySell_Price.Value =
                secInfo.PriceInCurrency(SelectedCurrency);

            numericUpDown_BuySell_Count.Value = secInfo.Count;
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

            if (secInfo == null || SelectedCurrency == null)
            {
                return;
            }

            numericUpDown_BuySell_Price.Value = 
                secInfo.PriceInCurrency(SelectedCurrency);
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

    }

    public class BuySellPage : KryptonPage
    {
        public BuySellControl Control { get; set; }

        public BuySellPage(InvestManager investManager)
        {
            Control = new BuySellControl(investManager);
            Control.Dock = DockStyle.Fill;
            Controls.Add(Control);
        }
    }
}
