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
            var settings = Settings.ReadFromFile("settings.xml");

            if (args.Length != 0)
                settings.Files.ReportsDir = args[0];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
