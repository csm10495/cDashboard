//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//cReminder - A reminder setup prompt for cDashboard
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
    public partial class cReminder : cForm
    {
        /// <summary>
        /// constructor
        /// </summary>
        public cReminder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cReminder_Load(object sender, EventArgs e)
        {

            CompletedForm_Load = true;
        }

        #region Drag Form
        /// <summary>
        /// label mouse down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_instructions_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// mouse down on background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cReminder_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// mouse down on menu strip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }
        #endregion

        /// <summary>
        /// close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// add this cReminder to settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_set_Click(object sender, EventArgs e)
        {
            DateTime date_time = datetimepicker.Value.Date + datetimepicker.Value.TimeOfDay.Subtract(new TimeSpan(0, 0, datetimepicker.Value.TimeOfDay.Seconds));
            //cReminder;TICKS1;MESSAGE1;TICKS2;MESSAGE2
            List<string> cReminders = new List<string>();
            cReminders.Add("cReminders");
            cReminders.AddRange(getSpecificSetting(new string[] { "cReminders" }));

            //cleanup rtb text
            rtb_message.Text = rtb_message.Text.Replace(";", ":");
            rtb_message.Text = rtb_message.Text.Replace(Environment.NewLine, "");
            rtb_message.Text = rtb_message.Text.Replace("\n", "");
            rtb_message.Text = rtb_message.Text.Replace("\r\n", "");

            //add this onto cReminders in settings
            cReminders.Add(date_time.Ticks.ToString());
            cReminders.Add(rtb_message.Text);

            List<string> find = new List<string>();
            find.Add("cReminders");

            replaceSetting(find, cReminders);

            //update the dict of cReminders in cDashboard
            ((cDashboard)(this.Parent)).updateDictCReminders();
           
            MessageBox.Show("Set cReminder for: " + rtb_message.Text + " for " + date_time.ToString());
            this.Close();
        }

        /// <summary>
        /// cancel, closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
