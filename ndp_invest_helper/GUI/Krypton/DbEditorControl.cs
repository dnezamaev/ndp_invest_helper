using ComponentFactory.Krypton.Navigator;

using ndp_invest_helper.DataHandlers;

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
        public DbEditorDiversityControl DiversityEditor
        {
            get => dbEditorDiversityControl;
        }

        public DbEditorControl()
        {
            InitializeComponent();
        }

        public void FillControls()
        {
            dbEditorDiversityControl.FillControls();
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
