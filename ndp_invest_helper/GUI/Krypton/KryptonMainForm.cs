using System;
using System.Windows.Forms;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Docking;
using System.Linq;

namespace ndp_invest_helper.GUI.Krypton
{
    public partial class KryptonMainForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private InvestManager investManager;

        private PortfolioAnalyticsControl portfolioAnalyticsControl;
        private BuySellControl buySellControl;
        private AssetsControl assetsControl;
        private PortfolioCashControl portfolioCashControl;
        private DealsControl dealsControl;
        private OfficerReportControl officerReportControl;
        private DbEditorControl dbEditorControl;

        public KryptonMainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CommonDataManager.Load();
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
                    CreateOfficerReportPage(),
                    CreateDbEditorPage()
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
                    CreateDealsPage(),
                    CreatePortfolioCashPage()
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

        private PortfolioCashPage CreatePortfolioCashPage()
        {
            var result =  new PortfolioCashPage()
            {
                Text = "Cash",
                TextTitle = "Наличка, вклады...",
                UniqueName = "PortfolioCash"
            };

            portfolioCashControl = result.Control;

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

        private DbEditorPage CreateDbEditorPage()
        {
            var result = new DbEditorPage()
            {
                Text = "База данных",
                TextTitle = "Редактирование базы",
                UniqueName = "DbEditor"
            };

            dbEditorControl = result.Control;

            return result;
        }

        private void InitInvestManager()
        {
            investManager = new InvestManager();
        }

        private void SetEventHandlers()
        {
            portfolioAnalyticsControl.CurrentGroupItemChanged +=
                portf => { assetsControl.FillAssets(portf); };

            assetsControl.AssetSelected +=
                sec => { buySellControl.SelectedSecurity = sec; };

            // Recalculate all if portfolio cash amount changed.
            portfolioCashControl.CashChanged +=
                () => { investManager.RedoAnalytics(true); };

            investManager.Analytics.DealCompleted += InvestManager_DealCompleted;
            investManager.Analytics.LastDealRemoved += InvestManager_LastDealRemoved;
            investManager.Analytics.AllDealsRemoved += InvestManager_AllDealsRemoved;

            investManager.Analytics.AnalyticsResultChanged +=
                () => { FillControls(); };
        }

        private void InvestManager_AllDealsRemoved()
        {
            dealsControl.RemoveAllDeals();
        }

        private void InvestManager_LastDealRemoved()
        {
            dealsControl.RemoveLastDeal();
        }

        private void InvestManager_DealCompleted(Deal deal)
        {
            dealsControl.AddDeal(deal);
        }

        private void FillControls()
        {
            portfolioAnalyticsControl.FillControls();
            buySellControl.FillControls();
            portfolioCashControl.FillControls();
            dbEditorControl.FillControls();
        }

        private void toolStripMenuItem_XmlToSqlite_Click(object sender, EventArgs e)
        {
            CommonDataManager.XmlToSqlite();
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CommonDataManager.LogSave();

            Settings.Save();
        }

        private void toolStripMenuItem_CancelDeal_Click(object sender, EventArgs e)
        {
            investManager.Analytics.RemoveLastDeal();
        }

        private void toolStripMenuItem_RunTask_Click(object sender, EventArgs e)
        {
            investManager.ExecuteTasks();
        }

        private void toolStripMenuItem_Settings_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.ShowDialog();

            if (form.DataReloadRequired)
            {
                CommonDataManager.Load();
                investManager.RedoAnalytics(false);
            }

            if (form.AnalyticsFormsReloadRequired)
            {
                // TODO: refill analytics datagrids
            }
        }
    }
}
