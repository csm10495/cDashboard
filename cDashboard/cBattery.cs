//This file is part of cDashboard
//cBattery - A battery status viewer for cDashboard
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
    public partial class cBattery : cForm
    {
        public cBattery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// updates all labels and battery_panel
        /// </summary>
        public void update()
        {
            //constants
            int panel_height = 45;
            int panel_y = 23;

            if (SystemInformation.PowerStatus.BatteryChargeStatus != BatteryChargeStatus.NoSystemBattery)
            {
                if (SystemInformation.PowerStatus.BatteryLifePercent != 1)
                {
                    string battery_percent = SystemInformation.PowerStatus.BatteryLifePercent.ToString("0.0%");
                    label_battery_percentage.Text = battery_percent.Substring(0, battery_percent.IndexOf(".")) + "%";

                    //panel_battery setting via math
                    Single s = Convert.ToSingle(panel_height) * SystemInformation.PowerStatus.BatteryLifePercent;
                    panel_battery.Height = Convert.ToInt16(s);
                    panel_battery.Location = new Point(panel_battery.Location.X, panel_y + (panel_height - Convert.ToInt16(s)));

                    if (SystemInformation.PowerStatus.BatteryLifePercent > .60)
                        panel_battery.BackColor = Color.Green;
                    else if (SystemInformation.PowerStatus.BatteryLifePercent > .20)
                        panel_battery.BackColor = Color.White;
                    else
                        panel_battery.BackColor = Color.Red;
                }
                else
                {
                    label_battery_percentage.Text = "100%";
                }

                //if this is -1, the battery life is unknown
                if (SystemInformation.PowerStatus.BatteryLifeRemaining == -1)
                {
                    if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online)
                    {
                        label_battery_remaining.Text = "Charging";
                    }
                    else
                    {
                        label_battery_remaining.Text = "";
                    }
                }
                else
                {
                    TimeSpan t = TimeSpan.FromSeconds(SystemInformation.PowerStatus.BatteryLifeRemaining);
                    label_battery_remaining.Text = t.Hours.ToString() + "h, " + t.Minutes.ToString() + "m";
                }

            }
            //no battery
            else
            {
                label_battery_remaining.Text = "No Batt.";
            }
        }

        /// <summary>
        /// closes this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// form loading, update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBattery_Load(object sender, EventArgs e)
        {
            this.update();
            CompletedForm_Load = true;
        }

        #region Dragging
        /// <summary>
        /// mousedown for moving form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBattery_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// dragging by panel_battery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_battery_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// dragging by picturebox_battery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_battery_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// dragging by label_battery_percentage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_battery_percentage_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// dragging by label_battery_remaining
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_battery_remaining_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }
        #endregion

    }
}
