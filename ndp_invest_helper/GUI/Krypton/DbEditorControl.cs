using ComponentFactory.Krypton.Navigator;

using ndp_invest_helper.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI.Krypton
{
    public partial class DbEditorControl : UserControl
    {
        public DbEditorControl()
        {
            InitializeComponent();
        }

        public void FillControls()
        {
            FillSecurityIssuerCombo();
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
        }

        private void FillDataGridParts()
        {
            var selectedSecIss = (IDiversified)comboBox_SecurityIssuer.SelectedItem;

            dataGrid_Parts.Rows.Clear();

            if (radioButton_Currencies.Checked)
            {
                foreach (var item in selectedSecIss.Currencies)
                {
                    var rowIndex = dataGrid_Parts.Rows.Add();
                    var row = dataGrid_Parts.Rows[rowIndex];

                    row.SetValues(item.Key.Code, item.Value);
                    row.Tag = item;
                }
            }
            else if (radioButton_Countries.Checked)
            {
                foreach (var item in selectedSecIss.Countries)
                {
                    var rowIndex = dataGrid_Parts.Rows.Add();
                    var row = dataGrid_Parts.Rows[rowIndex];

                    row.SetValues(item.Key.Code, item.Value);
                    row.Tag = item.Key;
                }
            }
            else if (radioButton_Sectors.Checked)
            {
                foreach (var item in selectedSecIss.Sectors)
                {
                    var rowIndex = dataGrid_Parts.Rows.Add();
                    var row = dataGrid_Parts.Rows[rowIndex];

                    row.SetValues(item.Key.Id, item.Value);
                    row.Tag = item.Key;
                }
            }

        }

        private void FillDataGridKeys()
        {
            dataGrid_Parts.Rows.Clear();

            if (radioButton_Currencies.Checked)
            {
                foreach (var item in CurrenciesManager.Currencies)
                {
                    var rowIndex = dataGrid_Parts.Rows.Add();
                    var row = dataGrid_Parts.Rows[rowIndex];

                    var key = item.Code;
                    var value = item.NameEng;
                }
            }
            else
            {
            }
        }

    }

    public class DbEditorPage : KryptonPage
    {
        public DbEditorControl Control { get; set; }

        public DbEditorPage()
        {
            Control = new DbEditorControl();
            Control.Dock = DockStyle.Fill;
            Controls.Add(Control);
        }
    }
}
