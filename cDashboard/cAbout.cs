//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//cAbout - An about box for cDashboard
//(C) Charles Machalow under the MIT License
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

        /// <summary>
        /// opens wunderground.com with APIREF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_weather_data_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.weather.yahoo.com");
        }

        /// <summary>
        /// cAbout loading event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cAbout_Load(object sender, EventArgs e)
        {
            //put the icon in a location off screen
            picture_icon.Location = new Point(picture_icon.Location.X + 200, picture_icon.Location.Y - 200);

            //animate
            timer_animation.Start();
        }

        /// <summary>
        /// timer tick event for the animation timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_animation_Tick(object sender, EventArgs e)
        {
            //move the icon (animate)
            picture_icon.Location = new Point(picture_icon.Location.X - 5, picture_icon.Location.Y + 5);

            //stop animation at correct point: 12,12
            if (picture_icon.Location.X == picture_icon.Location.Y) //== 12
            {
                timer_animation.Stop();
            }
        }
    }
}