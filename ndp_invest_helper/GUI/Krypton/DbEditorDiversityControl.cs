using ndp_invest_helper.Models;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI.Krypton
{
    public partial class DbEditorDiversityControl : UserControl
    {
        public DbEditorDiversityControl()
        {
            InitializeComponent();
        }

        public void FillControls()
        {
            FillSecurityIssuerCombo();
            FillDataGridParts();
            FillDataGridKeys();
        }

        public IDiversified SelectedSecurityOrIssuer
        {
            get => comboBox_SecurityIssuer.SelectedItem as IDiversified;
        }

        public Dictionary<DiversityItem, decimal> SelectedParts
        {
            get 
            {
                var result = new Dictionary<DiversityItem, decimal>();

                var rows = dataGrid_Parts.SelectedRows;

                if (rows.Count == 0)
                {
                    return result;
                }

                foreach (DataGridViewRow row in rows)
                {
                    var kvp = (KeyValuePair<DiversityItem, decimal>)row.Tag;
                    result.Add(kvp.Key, kvp.Value);
                }

                return result;
            }
        }

        private void radioButton_SecurityIssuer_CheckedChanged(object sender, EventArgs e)
        {
            FillSecurityIssuerCombo();
        }

        private void FillSecurityIssuerCombo()
        {
            if (radioButton_Issuer.Checked)
            {
                comboBox_SecurityIssuer.DataSource = null;
                comboBox_SecurityIssuer.DisplayMember = "NameRus";
                comboBox_SecurityIssuer.DataSource = 
                    new List<Issuer>(SecuritiesManager.Issuers);
            }
            else
            {
                comboBox_SecurityIssuer.DataSource = null;
                comboBox_SecurityIssuer.DisplayMember = "FullName";
                comboBox_SecurityIssuer.DataSource = 
                    new List<Security>(SecuritiesManager.Securities);
            }
        }

        private void radioButton_Currencies_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGridParts();
            FillDataGridKeys();
        }

        private void FillDataGridParts()
        {
            if (SelectedSecurityOrIssuer == null)
            {
                return;
            }

            Dictionary<DiversityItem, decimal> parts;

            dataGrid_Parts.Rows.Clear();

            if (radioButton_Currencies.Checked)
            {
                parts = SelectedSecurityOrIssuer.Currencies;
            }
            else if (radioButton_Countries.Checked)
            {
                parts = SelectedSecurityOrIssuer.Countries;
            }
            else if (radioButton_Sectors.Checked)
            {
                parts = SelectedSecurityOrIssuer.Sectors;
            }
            else
            {
                throw new NotImplementedException();
            }

            foreach (var item in parts)
            {
                var rowIndex = dataGrid_Parts.Rows.Add();
                var row = dataGrid_Parts.Rows[rowIndex];

                row.SetValues(item.Key.FriendlyName, item.Value * 100);
                row.Tag = item;

                row.Cells[0].ToolTipText = "Нажмите для удаления";
            }

            dataGrid_Parts.Tag = parts;
        }

        private void FillDataGridKeys()
        {
            var selectedParts = SelectedParts;

            dataGrid_Keys.Rows.Clear();

            List<DiversityItem> keys;

            if (radioButton_Currencies.Checked)
            {
                keys = CommonData.Currencies.Items;
            }
            else if (radioButton_Countries.Checked)
            {
                keys = CommonData.Countries.Items;
            }
            else if (radioButton_Sectors.Checked)
            {
                keys = CommonData.Sectors.Items;
            }
            else
            {
                throw new NotImplementedException();
            }

            foreach (var item in keys)
            {
                var rowIndex = dataGrid_Keys.Rows.Add();
                var row = dataGrid_Keys.Rows[rowIndex];

                row.SetValues(item.Code, item.FriendlyName);
                row.Tag = item;

                row.Cells[0].ToolTipText = "Нажмите для добавления";
            }

            dataGrid_Keys.Tag = keys;
        }

    }
}
