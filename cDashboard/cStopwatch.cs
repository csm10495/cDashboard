//This file is part of cDashboard
//cStopwatch - A stopwatch for cDashboard
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
    public partial class cStopwatch : cForm
    {
        #region Global Variables
        /// <summary>
        /// signifies the exact time that the stopwatch was started
        /// </summary>
        DateTime startTime;

        /// <summary>
        /// signifies that form loading has finished
        /// </summary>
        bool CompletedForm_Load = false;
        #endregion

        #region Constructor
        /// <summary>
        /// initialization
        /// </summary>
        public cStopwatch()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// method to start the stopwatch
        /// should also make itself not visible, so that we can see the timer
        /// </summary>
        private void start_stopwatch()
        {
            //get current time
            startTime = DateTime.UtcNow;

            replaceSetting(new string[] { "cStopwatch", this.Name, "StartDateTime" }, new string[] { "cStopwatch", this.Name, "StartDateTime", startTime.Ticks.ToString() });
            //finally, start timer
            sw_timer.Start();
        }

        /// <summary>
        /// sets the text of the label_time 
        /// </summary>
        /// <param name="tmp"></param>
        private void setLabel_TimeText(DateTime datetime_2)
        {
            //gets elapsed time
            TimeSpan tmp = (datetime_2 - startTime);

            //attempted fix for auto changing of TimeSpan.ToString()
            string string_span = Convert.ToInt16(tmp.TotalHours).ToString().PadLeft(2, '0') + ":" + tmp.Minutes.ToString().PadLeft(2, '0') + ":" + tmp.Seconds.ToString().PadLeft(2, '0') + "." + tmp.Milliseconds.ToString().PadLeft(2, '0').Substring(0, 2);

            label_time.Text = string_span;

            //attempts
            while (label_time.Width > this.Width && label_time.Text.Length != 11)
            {
                label_time.Font = new Font(label_time.Font.FontFamily, Convert.ToSingle(label_time.Font.Size) - .001F);
            }

            label_started_time.Text = "cStopwatch started on: " + startTime.ToLocalTime().ToString();
        }

        /// <summary>
        /// called everytime the stopwatch timer ticks
        /// changes text to depict change in time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sw_timer_Tick(object sender, EventArgs e)
        {
            //calls label setting method
            setLabel_TimeText(DateTime.UtcNow);
        }

        /// <summary>
        /// called upon loading the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cStopwatch_Load(object sender, EventArgs e)
        {
            //get previously set start time
            string string_start_time = getSpecificSetting(new string[] { "cStopwatch", this.Name, "StartDateTime" })[0];

            //if the timer has been started before
            if (string_start_time != "NULL")
            {
                startTime = new DateTime(Convert.ToInt64(string_start_time));
                setLabel_TimeText(DateTime.UtcNow);
            }

            //see if timer was running on close
            bool timer_running = Convert.ToBoolean(getSpecificSetting(new string[] { "cStopwatch", this.Name, "TimerRunning" })[0]);

            //get end time (Null if not applicable)
            string string_end_time = getSpecificSetting(new string[] { "cStopwatch", this.Name, "EndDateTime" })[0];

            //if the timer was left running, start it again, and set text to stop
            if (timer_running)
            {
                sw_timer.Start();
            }

            //if the EndDateTime is not null, the timer ended, display previous results
            else if (string_end_time != "NULL")
            {
                setLabel_TimeText(new DateTime(Convert.ToInt64(string_end_time)));
            }

            //to allow the rest of the class to know loading has completed
            CompletedForm_Load = true;
        }

        /// <summary>
        /// combined start/stop button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if the timer is not running, start it
            if (!sw_timer.Enabled)
            {
                start_stopwatch();
                replaceSetting(new string[] { "cStopwatch", this.Name, "TimerRunning" }, new string[] { "cStopwatch", this.Name, "TimerRunning", "True" });
                replaceSetting(new string[] { "cStopwatch", this.Name, "EndDateTime" }, new string[] { "cStopwatch", this.Name, "EndDateTime", "NULL" });
            }
            else
            {
                string stop_ticks = DateTime.UtcNow.Ticks.ToString();
                sw_timer.Stop();
                replaceSetting(new string[] { "cStopwatch", this.Name, "TimerRunning" }, new string[] { "cStopwatch", this.Name, "TimerRunning", "False" });
                replaceSetting(new string[] { "cStopwatch", this.Name, "EndDateTime" }, new string[] { "cStopwatch", this.Name, "EndDateTime", stop_ticks });

                //we update the label text one more time to make sure it reflects what we save to settings
                setLabel_TimeText(new DateTime(Convert.ToInt64(stop_ticks)));
            }
        }

        /// <summary>
        /// this will save the new location of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cStopwatch_ResizeEnd(object sender, EventArgs e)
        {
            if (CompletedForm_Load)
            {
                //handle move
                replaceSetting(new string[] { "cStopwatch", this.Name, "Location" }, new string[] { "cStopwatch", this.Name, "Location", this.Location.X.ToString(), this.Location.Y.ToString() });
            }
        }

        /// <summary>
        /// this is the close button for the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Form Moving

        /// <summary>
        /// used to drag the form via the menustrip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// move form on mousedown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cStopwatch_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// move form on mousedown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_time_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// move form on mousedown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_started_time_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        #endregion

        /// <summary>
        /// makes new lap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Doesn't do anything yet");
        }
    }
}
