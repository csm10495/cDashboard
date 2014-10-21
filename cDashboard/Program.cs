//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//(C) Charles Machalow 2014 under the MIT License
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cDashboard
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
              Application.Run(new cDashboard());
        }
    }
}
