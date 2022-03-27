using ComponentFactory.Krypton.Navigator;
using ndp_invest_helper.Models;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI
{
    public partial class DealsControl : UserControl
    {
        public DealsControl()
        {
            InitializeComponent();
        }

        public void AddDeal(Deal deal)
        {
            listBox_Deals.Items.Add(deal);
        }

        public void RemoveAllDeals()
        {
            listBox_Deals.Items.Clear();
        }

        public void RemoveLastDeal()
        {
            var count = listBox_Deals.Items.Count;

            if (count == 0)
                return;

            listBox_Deals.Items.RemoveAt(count - 1);
        }
    }

    public class DealsPage : KryptonPage
    {
        public DealsControl Control { get; set; }

        public DealsPage()
        {
            Control = new DealsControl();
            Control.Dock = DockStyle.Fill;
            Controls.Add(Control);
        }
    }

}
