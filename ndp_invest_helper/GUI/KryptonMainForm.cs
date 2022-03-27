using System;
using System.Windows.Forms;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Docking;
using System.Linq;

namespace ndp_invest_helper.GUI
{
    public partial class KryptonMainForm : Form
    {
        private InvestManager investManager;

        private PortfolioAnalyticsControl portfolioAnalyticsControl;
        private BuySellControl buySellControl;
        private AssetsControl assetsControl;
        private DealsControl dealsControl;
        private OfficerReportControl officerReportControl;

        public KryptonMainForm()
        {
            InitializeComponent();
        }

        private void KryptonMainForm_Load(object sender, EventArgs e)
        {
            InitInvestManager();
            InitControls();
            SetEventHandlers();
            FillControls();
        }

        private void InitControls()
        {
            var w = kryptonDockingManager.ManageWorkspace("Workspace", kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl("Control", kryptonPanelMain, w);
            kryptonDockingManager.ManageFloating("Floating", this);

            // Main space (fills center).
            kryptonDockingManager.AddToWorkspace
            (
                "Workspace", 
                new KryptonPage[] 
                {
                    CreatePortfolioAnalyticsPage(),
                    CreateOfficerReportPage()
                }
            );

            // Bottom docking pages.
            kryptonDockingManager.AddDockspace
            (
                "Control", 
                DockingEdge.Bottom, 
                new KryptonPage[] 
                { 
                    CreateBuySellPage()
                }
            );

            // Right docking pages.
            kryptonDockingManager.AddDockspace
            (
                "Control", 
                DockingEdge.Right, 
                new KryptonPage[] 
                {
                    CreateAssetsPage(),
                    CreateDealsPage()
                }
            );
        }

        private PortfolioAnalyticsPage CreatePortfolioAnalyticsPage()
        {
            var result = new PortfolioAnalyticsPage(investManager)
            {
                Text = "Аналитика",
                TextTitle = "Аналитика портфеля",
                UniqueName = "PortfolioAnalytics"
            };

            portfolioAnalyticsControl = result.Control;

            return result;
        }

        private BuySellPage CreateBuySellPage()
        {
            var result = new BuySellPage(investManager)
            {
                Text = "Купля/продажа",
                TextTitle = "Эмуляция покупки и продажи",
                UniqueName = "BuySellPage"
            };

            buySellControl = result.Control;

            return result;
        }

        private DealsPage CreateDealsPage()
        {
            var result = new DealsPage()
            {
                Text = "Сделки",
                TextTitle = "Сделки",
                UniqueName = "Deals"
            };

            dealsControl = result.Control;

            return result;
        }

        private AssetsPage CreateAssetsPage()
        {
            var result =  new AssetsPage()
            {
                Text = "Состав",
                TextTitle = "Состав группы",
                UniqueName = "Assets"
            };

            assetsControl = result.Control;

            return result;
        }

        private OfficerReportPage CreateOfficerReportPage()
        {
            var result = new OfficerReportPage()
            {
                Text = "Госслужба",
                TextTitle = "Госслужба",
                UniqueName = "OfficerReport"
            };

            officerReportControl = result.Control;

            return result;
        }

        private void InitInvestManager()
        {
            investManager = new InvestManager();
            investManager.Reload();
        }

        private void SetEventHandlers()
        {
            portfolioAnalyticsControl.CurrentGroupItemChanged +=
                portf => { assetsControl.FillAssets(portf); };

            assetsControl.AssetSelected +=
                sec => { buySellControl.SelectedSecurity = sec; };

            investManager.DealCompleted += InvestManager_DealCompleted;
            investManager.LastDealRemoved += InvestManager_LastDealRemoved;
            investManager.AllDealsRemoved += InvestManager_AllDealsRemoved;
        }

        private void InvestManager_AllDealsRemoved()
        {
            FillControls();
            dealsControl.RemoveAllDeals();
        }

        private void InvestManager_LastDealRemoved()
        {
            FillControls();
            dealsControl.RemoveLastDeal();
        }

        private void InvestManager_DealCompleted(Deal deal)
        {
            FillControls();
            dealsControl.AddDeal(deal);
        }

        private void FillControls()
        {
            portfolioAnalyticsControl.FillGroupControls();
            buySellControl.FillBuySellCombos();
        }

        private void toolStripMenuItem_XmlToSqlite_Click(object sender, EventArgs e)
        {
            investManager.XmlToSqlite();
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

        private void KryptonMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            investManager.LogSave();

            Settings.Save();
        }

        private void toolStripMenuItem_CancelDeal_Click(object sender, EventArgs e)
        {
            investManager.RemoveLastDeal();
        }

        private void toolStripMenuItem_RunTask_Click(object sender, EventArgs e)
        {
            investManager.ExecuteTasks();
        }

        private void toolStripMenuItem_Settings_Click(object sender, EventArgs e)
        {
            var form = new KryptonSettingsForm();
            form.ShowDialog();

            if (form.DataReloadRequired)
            {
                // TODO: recalculate analytics with deals
                investManager.Reload();
            }

            if (form.ShowChangesFromChanged)
            {
                // TODO: refill analytics datagrids
            }
        }
    }
}
