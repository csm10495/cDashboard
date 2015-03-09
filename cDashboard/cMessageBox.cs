//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//cMessageBox - A scroll-able messagebox for cDashboard
//(C) Charles Machalow under the MIT License
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cDashboard
{
    public partial class cMessageBox : Form
    {
        /// <summary>
        /// return value, used for getting DialogResult
        /// if 1: yes
        /// if 2 (or not 1): no
        /// </summary>
        int retval = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        public cMessageBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// useful constructor
        /// </summary>
        /// <param name="msg">message text</param>
        /// <param name="title">title text</param>
        public cMessageBox(string msg, string title)
        {
            InitializeComponent();
            this.Text = title;
            label_msg.Text = msg;
        }

        /// <summary>
        /// used to show the dialog and give a response to the calling form
        /// </summary>
        /// <returns></returns>
        public DialogResult cShowDialog()
        {
            this.ShowDialog();

            if (retval == 1)
            {
                return DialogResult.Yes;
            }
            else
            {
                return DialogResult.No;
            }
        }

        /// <summary>
        /// if yes is clicked, retval == 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_yes_Click(object sender, EventArgs e)
        {
            retval = 1;
            this.Close();
        }

        /// <summary>
        /// if no is clicked, retval == 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_no_Click(object sender, EventArgs e)
        {
            retval = 2;
            this.Close();
        }
    }
}
