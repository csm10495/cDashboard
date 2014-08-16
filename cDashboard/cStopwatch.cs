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
        public DateTime startTime;

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
        /// drag form by rtb_l
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richtextbox_lap_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }
        #endregion

        /// <summary>
        /// forces a center alignment for the RTBL
        /// </summary>
        private void centerAlignRTBL()
        {
            richtextbox_lap.SelectAll();
            richtextbox_lap.SelectionAlignment = HorizontalAlignment.Center;
        }

        /// <summary>
        /// method to start the stopwatch
        /// should also make itself not visible, so that we can see the timer
        /// </summary>
        private void start_stopwatch()
        {
            //get current time
            startTime = DateTime.UtcNow;

            string string_origStartTime = getSpecificSetting(new string[] { "cStopwatch", this.Name, "StartDateTime" })[0];
            string string_origEndTime = getSpecificSetting(new string[] { "cStopwatch", this.Name, "EndDateTime" })[0];
            string string_origLapTime = getSpecificSetting(new string[] { "cStopwatch", this.Name, "LapTime" })[0];


            long origStartTime = 0;
            if (string_origStartTime != "NULL")
                origStartTime = Convert.ToInt64(string_origStartTime);

            long origEndTime = 0;
            if (string_origEndTime != "NULL")
                origEndTime = Convert.ToInt64(string_origEndTime);

            long origLapTime = 0;
            if (string_origLapTime != "NULL")
                origLapTime = Convert.ToInt64(string_origLapTime);

            //calculate new laptime
            string newLapTime = "NULL";
            if ((origLapTime + (origEndTime - origStartTime)) != 0)
            {
                newLapTime = (origLapTime + ((startTime.Ticks - (origEndTime - origStartTime)) - origStartTime)).ToString();
            }

            replaceSetting(new string[] { "cStopwatch", this.Name, "StartDateTime" }, new string[] { "cStopwatch", this.Name, "StartDateTime", (startTime.Ticks - (origEndTime - origStartTime)).ToString() });
            replaceSetting(new string[] { "cStopwatch", this.Name, "LapTime" }, new string[] { "cStopwatch", this.Name, "LapTime", newLapTime });

            startTime = new DateTime(startTime.Ticks - (origEndTime - origStartTime));
            //finally, start timer
            sw_timer.Start();

            //updates the start time
            string s = richtextbox_lap.Lines[0].Substring(0);
            string[] lines = richtextbox_lap.Lines;

            lines[0] = "cStopwatch start time: " + startTime.ToLocalTime().ToString(); 
            richtextbox_lap.Lines = lines;

            centerAlignRTBL();
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
            string string_span = Convert.ToInt64(tmp.TotalHours).ToString().PadLeft(2, '0') + ":" + tmp.Minutes.ToString().PadLeft(2, '0') + ":" + tmp.Seconds.ToString().PadLeft(2, '0') + "." + tmp.Milliseconds.ToString().PadLeft(2, '0').Substring(0, 2);

            label_time.Text = string_span;

            //attempts
            while (label_time.Width > this.Width && label_time.Text.Length != 11)
            {
                //lowered precision from .0001F to .1F to speed up application startup
                label_time.Font = new Font(label_time.Font.FontFamily, Convert.ToSingle(label_time.Font.Size) - .1F);
            }
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
                richtextbox_lap.Text = "cStopwatch start time: " + startTime.ToLocalTime().ToString();
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

            //get previous laps (if any)
            List<String> list_laps = getSpecificSetting(new string[] { "cStopwatch", this.Name, "Laps" });
            foreach (string s in list_laps)
            {
                //add each lap to rtbl without updating settings
                addLap(s, false);
            }

            //force center alignment
            centerAlignRTBL();

            //make lap only work if timer is running
            lToolStripMenuItem.Enabled = sw_timer.Enabled;

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
                //pause timer
                string stop_ticks = DateTime.UtcNow.Ticks.ToString();
                sw_timer.Stop();
                replaceSetting(new string[] { "cStopwatch", this.Name, "TimerRunning" }, new string[] { "cStopwatch", this.Name, "TimerRunning", "False" });
                replaceSetting(new string[] { "cStopwatch", this.Name, "EndDateTime" }, new string[] { "cStopwatch", this.Name, "EndDateTime", stop_ticks });

                //we update the label text one more time to make sure it reflects what we save to settings
                setLabel_TimeText(new DateTime(Convert.ToInt64(stop_ticks)));
            }

            //make lap only work if timer is running
            lToolStripMenuItem.Enabled = sw_timer.Enabled;
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

        /// <summary>
        /// makes new lap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addLap(label_time.Text, true);
        }

        /// <summary>
        /// method to add lap 
        /// </summary>
        /// <param name="text_lap">lap time as text</param>
        /// <param name="update_settings">true if settings should be resaved</param>
        private void addLap(string text_lap, bool update_settings)
        {
            //get approx tick value
            DateTime now = DateTime.UtcNow;

            //don't add more space to form after certain time
            if (richtextbox_lap.Lines.Count() < 7)
            {
                this.Height += 16;
            }

            //make text_lap equal to the proper lap length (time from last lap till now)
            string string_laptime = getSpecificSetting(new string[] { "cStopwatch", this.Name, "LapTime" })[0];

            //if we have a previous laptime, convert to long, use
            if (string_laptime != "NULL" && update_settings)
            {
                long long_laptime = Convert.ToInt64(string_laptime);

                DateTime datetime_laptime = new DateTime(long_laptime);

                text_lap = timeSpanToString((now - datetime_laptime));
            }

            //add lap to richtextbox
            richtextbox_lap.Text += Environment.NewLine + "Lap " + richtextbox_lap.Lines.Count().ToString() + ". " + text_lap;
            centerAlignRTBL();

            //scroll to end
            richtextbox_lap.SelectionStart = richtextbox_lap.Text.Length;
            richtextbox_lap.ScrollToCaret();

            if (update_settings)
            {
                //must save lap to settings
                List<String> list_laps = getSpecificSetting(new string[] { "cStopwatch", this.Name, "Laps" });
                list_laps.InsertRange(0, new string[] { "cStopwatch", this.Name, "Laps" });
                list_laps.Add(text_lap);
                replaceSetting(new string[] { "cStopwatch", this.Name, "Laps" }, list_laps.ToArray());
                //replace setting
                replaceSetting(new string[] { "cStopwatch", this.Name, "LapTime" }, new string[] { "cStopwatch", this.Name, "LapTime", now.Ticks.ToString() });
            }


        }

        /// <summary>
        /// reset button for timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richtextbox_lap.Text = "\"S\" to Start/Stop. \"L\" to Lap. \"R\" to Reset";

            //reset all settings
            replaceSetting(new string[] { "cStopwatch", this.Name, "StartDateTime" }, new string[] { "cStopwatch", this.Name, "StartDateTime", "NULL" });
            replaceSetting(new string[] { "cStopwatch", this.Name, "EndDateTime" }, new string[] { "cStopwatch", this.Name, "EndDateTime", "NULL" });
            replaceSetting(new string[] { "cStopwatch", this.Name, "TimerRunning" }, new string[] { "cStopwatch", this.Name, "TimerRunning", "False" });
            replaceSetting(new string[] { "cStopwatch", this.Name, "Laps" }, new string[] { "cStopwatch", this.Name, "Laps" });
            replaceSetting(new string[] { "cStopwatch", this.Name, "LapTime" }, new string[] { "cStopwatch", this.Name, "LapTime", "NULL" });
            label_time.Text = "00:00:00.00";
            sw_timer.Stop();

            //reset form size
            this.Size = new Size(255, 93);

            //force center alignment
            centerAlignRTBL();

            //make lap only work if timer is running
            lToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// returns a string from a timespan
        /// </summary>
        /// <param name="tmp">timespan</param>
        /// <returns></returns>
        private string timeSpanToString(TimeSpan tmp)
        {
            return Convert.ToInt64(tmp.TotalHours).ToString().PadLeft(2, '0') + ":" + tmp.Minutes.ToString().PadLeft(2, '0') + ":" + tmp.Seconds.ToString().PadLeft(2, '0') + "." + tmp.Milliseconds.ToString().PadLeft(2, '0').Substring(0, 2);
        }

        /// <summary>
        /// converts string to timespan
        /// 00:13:30.32 -> Timespan 13 min, 30 seconds, 32 milliseconds
        /// </summary>
        /// <returns></returns>
        private TimeSpan stringToTimeSpan(string s)
        {
            //get hours
            string hours = s.Substring(0, s.IndexOf(":"));
            s = s.Substring(s.IndexOf(":") + 1);

            string minutes = s.Substring(0, s.IndexOf(":"));
            s = s.Substring(s.IndexOf(":") + 1);

            string seconds = s.Substring(0, s.IndexOf("."));
            s = s.Substring(s.IndexOf(".") + 1);

            string milliseconds = s;

            return new TimeSpan(0, Convert.ToInt16(hours), Convert.ToInt16(minutes), Convert.ToInt16(seconds), Convert.ToInt16(milliseconds));
        }
    }
}
