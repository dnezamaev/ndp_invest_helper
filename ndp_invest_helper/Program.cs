using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ndp_invest_helper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 0)
                Settings.ReportsDirectory = args[0];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.KryptonMainForm());
            //Application.Run(new MainForm());
        }
    }
}
