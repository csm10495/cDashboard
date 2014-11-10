//This file is part of cDashboard
//cNotification - A notification viewer for cDashboard
//(C) Charles Machalow 2014 under the MIT License
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
    public partial class cNotification : cForm
    {
        /// <summary>
        /// timer counter
        /// </summary>
        int int_timertime = 0;

        /// <summary>
        /// time that this cNotification is displayed
        /// </summary>
        int display_time = 5;

        /// <summary>
        /// loadedin = false if this has not been shown yet
        /// loadedin = true if this has been shown already
        /// </summary>
        bool loadedin = false;

        #region Constructor and Form Loading
        /// <summary>
        /// standard constructor
        /// </summary>
        public cNotification()
        {
            InitializeComponent();
        }

        /// <summary>
        /// constructor for cNotification with custom notification text
        /// </summary>
        /// <param name="notification_text">text for cNotification</param>
        public cNotification(string notification_text)
        {
            InitializeComponent();
            label_text.Text = notification_text;
            load_in();
        }

        /// <summary>
        /// constructor for cNotification with custom notification title/text
        /// </summary>
        /// <param name="notification_text">text for cNotification</param>
        /// <param name="notification_title">title for cNotification</param>
        public cNotification(string notification_text, string notification_title)
        {
            InitializeComponent();
            label_text.Text = notification_text;
            label_title.Text = notification_title;
            load_in();
        }

        /// <summary>
        /// form load method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cNotification_Load(object sender, EventArgs e)
        {
            CompletedForm_Load = true;
        }
        #endregion

        /// <summary>
        /// loads the cNotifcation in (stylization)
        /// </summary>
        private void load_in()
        {
            display_time = Convert.ToInt16(getSpecificSetting(new string[] { "cNotification", "DisplayTime" })[0]);
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Width - 15, Screen.PrimaryScreen.WorkingArea.Bottom - this.Height - 15);
            this.Show();
            this.Visible = true;
            this.TopMost = true;
            this.BringToFront();
            whileIn();
        }

        /// <summary>
        /// called while the form is shown
        /// </summary>
        private void whileIn()
        {
            loadedin = true;
            int_timertime = 0;
            timer_fade.Start();
        }

        /// <summary>
        /// loads the cNotification out (stylization)
        /// </summary>
        private void load_out()
        {
            this.Close();
        }

        /// <summary>
        /// close this cNotification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// wait to close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_fade_Tick(object sender, EventArgs e)
        {
            int_timertime++;
            if (int_timertime > display_time)
            {
                if (loadedin)
                {
                    load_out();
                }
            }
        }

    }
}
