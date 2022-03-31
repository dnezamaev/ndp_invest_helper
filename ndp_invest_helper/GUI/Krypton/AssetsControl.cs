using ComponentFactory.Krypton.Navigator;
using ndp_invest_helper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI.Krypton
{
    public partial class AssetsControl : UserControl
    {
        public event Action<Security> AssetSelected;

        public AssetsControl()
        {
            InitializeComponent();
        }

        public void FillAssets(Portfolio portfolio)
        {
            var securities = portfolio.Securities.ToList();

            dataGridView_GroupContent.Rows.Clear();

            // Заполняем таблицу с составом группы.
            foreach (var securityKVP in securities)
            {
                var secTotal = securityKVP.Value.Total;
                var portfTotal = portfolio.Total;

                if (portfTotal == 0)
                    continue;

                var rowIndex = dataGridView_GroupContent.Rows.Add();
                var row = dataGridView_GroupContent.Rows[rowIndex];

                row.SetValues(
                    securityKVP.Key.BestUniqueFriendlyName,
                    secTotal / portfTotal * 100);
                row.Tag = securityKVP.Key;
            }

            dataGridView_GroupContent.Sort(
                dataGridView_GroupContent.Columns[1], ListSortDirection.Descending);

            // Select first row and raise events.
            if (dataGridView_GroupContent.Rows.Count != 0)
            {
                dataGridView_GroupContent.ClearSelection();
                dataGridView_GroupContent.Rows[0].Selected = true;

                dataGridView_GroupContent_RowEnter(
                    dataGridView_GroupContent,
                    new DataGridViewCellEventArgs(0, 0)
                    );
            }
        }

        /// <summary>
        /// Get current selected Security object or null if none selected.
        /// </summary>
        public Security SelectedSecurity
        {
            get
            {
                var selectedRowsCollection = dataGridView_GroupContent.SelectedRows;

                if (selectedRowsCollection.Count == 0)
                    return null;

                return (Security)selectedRowsCollection[0].Tag;
            }
        }

        private void dataGridView_GroupContent_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            AssetSelected(SelectedSecurity);
        }
    }

    public class AssetsPage : KryptonPage
    {
        public AssetsControl Control { get; set; }

        public AssetsPage()
        {
            Control = new AssetsControl();
            Control.Dock = DockStyle.Fill;
            Controls.Add(Control);
        }
    }
}
