//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//cNotification - A notification viewer for cDashboard
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
        /// this is the tick counter for the fade timer
        /// </summary>
        int int_fade_timertime = 0;

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

        #region Timer Ticks

        /// <summary>
        /// wait to close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_show_Tick(object sender, EventArgs e)
        {
            int_timertime++;
            if (int_timertime > display_time)
            {
                if (loadedin)
                {
                    timer_fade.Start();
                }
            }
        }

        /// <summary>
        /// timer to coordinate fading in and out of this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_fade_tick(object sender, EventArgs e)
        {
            //increment timer ticking
            int_fade_timertime++;

            //fade out related code
            #region Fade Out
            if (loadedin)
            {
                double tmp = 1 - (Convert.ToDouble(int_fade_timertime) * .05);

                if (this.Opacity <= 0)
                {
                    this.Opacity = 0;
                    this.Visible = false;
                    this.Close();
                }
                else
                {
                    this.Opacity = tmp;
                }
            }
            #endregion

            //fade in related code
            #region Fade In
            if (!loadedin)
            {
                double tmp = Convert.ToDouble(int_fade_timertime) * .05;
                this.Opacity = tmp;
                if (tmp >= 1)
                {
                    this.Opacity = 1;
                    int_fade_timertime = 0;
                    timer_fade.Stop();
                    loadedin = true;
                    timer_show.Start();
                }
            }
            #endregion

        }

        #endregion

        /// <summary>
        /// special form styling
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x00000008 /*WS_EX_TOPMOST*/; //topmost
                param.ExStyle |= 0x08000000 /*WS_EX_NOACTIVATE*/; //prevent activation
                return param;
            }
        }

        /// <summary>
        /// loads the cNotifcation in (stylization)
        /// </summary>
        private void load_in()
        {
            autoSizeLabelText(ref label_text);
            autoSizeLabelText(ref label_title);
            display_time = Convert.ToInt16(getSpecificSetting(new string[] { "cNotification", "DisplayTime" })[0]);
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Width - 15, Screen.PrimaryScreen.WorkingArea.Bottom - this.Height - 15);
            this.Show();
            this.Visible = true;
            this.TopMost = true;
            this.BringToFront();
            this.Opacity = 0;
            timer_fade.Start();
        }

        /// <summary>
        /// autosizes text for label
        /// </summary>
        /// <param name="lbl">label for autosize</param>
        private void autoSizeLabelText(ref Label lbl)
        {
            while (lbl.Width * 4 < System.Windows.Forms.TextRenderer.MeasureText(lbl.Text, new Font(lbl.Font.FontFamily, lbl.Font.Size, lbl.Font.Style)).Width)
            {
                lbl.Font = new Font(lbl.Font.FontFamily, lbl.Font.Size - 0.5f, lbl.Font.Style);
            }
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
    }
}
