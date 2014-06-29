//This file is part of cDashboard
//cAbout - An about box for cDashboard
//(C) Charles Machalow 2014 under the MIT License
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace cDashboard
{
    partial class cAbout : Form
    {
        public cAbout()
        {
            InitializeComponent();
        }

        /// <summary>
        /// closes the about form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}