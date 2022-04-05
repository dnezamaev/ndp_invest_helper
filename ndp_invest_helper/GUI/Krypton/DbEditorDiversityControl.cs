using ndp_invest_helper.DataHandlers;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ndp_invest_helper.GUI.Krypton
{
    public partial class DbEditorDiversityControl : UserControl
    {
        public event Action DiversityChanged;

        private void RaiseDiversityChanged()
        {
            if (DiversityChanged != null)
            {
                DiversityChanged();
            }
        }

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

        /// <summary>
        /// Selected security or issuer.
        /// </summary>
        private IDiversified CurrentObject
        {
            get => comboBox_SecurityIssuer.SelectedItem as IDiversified;
        }

        /// <summary>
        /// Selected diversity: assets, countries, currencies, sectors.
        /// </summary>
        private Dictionary<DiversityItem, decimal> CurrentDiversity
        {
            get => dataGrid_Parts.Tag as Dictionary<DiversityItem, decimal>;
        }

        /// <summary>
        /// Selected object's type to edit: issuers or securities.
        /// </summary>
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

        /// <summary>
        /// Selected object to edit.
        /// </summary>
        private void comboBox_SecurityIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = CurrentObject;

            if (selected == null)
            {
                return;
            }

            // Assets only in ETF.
            radioButton_Assets.Enabled = (selected is ETF);

            // Selected non-ETF while Assets RadioButton checked.
            if (!radioButton_Assets.Enabled && radioButton_Assets.Checked)
            {
                radioButton_Currencies.Checked = true; // Check Currencies.
            }

            FillDataGridParts();
            FillDataGridKeys();
        }

        /// <summary>
        /// Selected diversity item type: 
        ///   assets, currencies, countries, sectors.
        /// </summary>
        private void radioButton_DivItem_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGridParts();
            FillDataGridKeys();
        }

        /// <summary>
        /// Fill table of diversity items and parts.
        /// </summary>
        private void FillDataGridParts()
        {
            IDiversified selectedObject = CurrentObject;

            if (selectedObject == null)
            {
                return;
            }

            Dictionary<DiversityItem, decimal> diversity;

            dataGrid_Parts.Rows.Clear();

            if (radioButton_Currencies.Checked)
            {
                diversity = selectedObject.Currencies;
            }
            else if (radioButton_Countries.Checked)
            {
                diversity = selectedObject.Countries;
            }
            else if (radioButton_Sectors.Checked)
            {
                diversity = selectedObject.Sectors;
            }
            else if (radioButton_Assets.Checked && selectedObject is ETF)
            {
                diversity = (selectedObject as ETF).Assets;
            }
            else
            {
                throw new NotImplementedException();
            }

            foreach (var kvp in diversity)
            {
                AddRowPart(kvp);
            }

            dataGrid_Parts.Tag = diversity;
        }

        /// <summary>
        /// Add new row to parts DataGrid.
        /// </summary>
        /// <param name="kvp">Diversity item and its part.</param>
        /// <returns></returns>
        private DataGridViewRow AddRowPart(KeyValuePair<DiversityItem, decimal> kvp)
        {
            var rowIndex = dataGrid_Parts.Rows.Add();
            var row = dataGrid_Parts.Rows[rowIndex];

            row.SetValues(kvp.Key.FriendlyName, kvp.Value * 100);
            row.Tag = kvp;

            row.Cells[0].ToolTipText = "Нажмите для удаления";

            return row;
        }

        private void FillDataGridKeys()
        {
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
            else if (radioButton_Assets.Checked)
            {
                keys = CommonData.Assets.Items;
            }
            else
            {
                throw new NotImplementedException();
            }

            foreach (var item in keys)
            {
                if (CurrentDiversity.ContainsKey(item))
                {
                    continue; // do not items that Security already has
                }

                AddRowKey(item);
            }

            dataGrid_Keys.Tag = keys;
        }

        /// <summary>
        /// Add new row to keys DataGrid.
        /// </summary>
        /// <param name="item">Key to add.</param>
        /// <returns></returns>
        private DataGridViewRow AddRowKey(DiversityItem item)
        {
            if (item == CommonData.Assets.Etf)
            {
                return null; // adding fund to fund has no sense
            }    

            var rowIndex = dataGrid_Keys.Rows.Add();
            var row = dataGrid_Keys.Rows[rowIndex];

            row.SetValues(item.Code, item.FriendlyName);
            row.Tag = item;

            row.Cells[0].ToolTipText = "Нажмите для добавления";

            return row;
        }

        /// <summary>
        /// Clicked key from parts table. Remove it from security/issuer.
        /// </summary>
        private void dataGrid_Parts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 || CurrentObject == null)
            {
                return;
            }

            // Key link.
            var kvp = 
                (KeyValuePair<DiversityItem, decimal>)
                dataGrid_Parts.Rows[e.RowIndex].Tag;

            var item = kvp.Key;
            var part = kvp.Value;

            // Update database.
            DatabaseManager.HandleDiversityItem(
                DbAction.Delete, 
                CurrentObject, 
                item, 
                part
                );

            // Remove from DataGrid and Security object.
            dataGrid_Parts.Rows.RemoveAt(e.RowIndex);
            CurrentDiversity.Remove(item);

            // Make key available for adding.
            AddRowKey(item);

            RaiseDiversityChanged();
        }

        /// <summary>
        /// Edited part. Update security/issuer.
        /// </summary>
        private void dataGrid_Parts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1 || CurrentObject == null || e.RowIndex < 0)
            {
                return;
            }

            var row = dataGrid_Parts.Rows[e.RowIndex];

            if (row.Tag == null)
            {
                return;
            }

            // Key link.
            var kvp = (KeyValuePair<DiversityItem, decimal>) row.Tag;

            var item = kvp.Key;
            var part = decimal.Parse(row.Cells[1].Value.ToString()) / 100;

            // Update database.
            DatabaseManager.HandleDiversityItem(
                DbAction.Update, 
                CurrentObject, 
                item, 
                part
                );

            // Update loaded Security.
            CurrentDiversity[item] = part;

            RaiseDiversityChanged();
        }

        /// <summary>
        /// Clicked key from available table. Add it to security/issuer.
        /// </summary>
        private void dataGrid_Keys_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 || CurrentObject == null)
            {
                return;
            }

            var item = (DiversityItem)dataGrid_Keys.Rows[e.RowIndex].Tag;
            var kvp = new KeyValuePair<DiversityItem, decimal>(item, 0M);

            DatabaseManager.HandleDiversityItem(
                DbAction.Insert,
                CurrentObject,
                kvp.Key,
                kvp.Value
                );

            // Add row to DataGrid.
            AddRowPart(kvp);

            // Update loaded Security.
            CurrentDiversity.Add(kvp.Key, kvp.Value);

            // Remove key from available for adding.
            dataGrid_Keys.Rows.RemoveAt(e.RowIndex);

            RaiseDiversityChanged();
        }
    }
}
