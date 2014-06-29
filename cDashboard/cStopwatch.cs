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
            startTime = DateTime.Now;

            replaceSetting(new string[] { "cStopwatch", this.Name, "StartDateTime" }, new string[] { "cStopwatch", this.Name, "StartDateTime", DateTime.Now.Ticks.ToString() });
            //finally, start timer
            sw_timer.Start();
        }

        /// <summary>
        /// start the stopwatch from the menustrip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if the timer is not running, start it
            if (!sw_timer.Enabled)
            {
                start_stopwatch();
                startToolStripMenuItem.Text = "Stop";
                replaceSetting(new string[] { "cStopwatch", this.Name, "TimerRunning" }, new string[] { "cStopwatch", this.Name, "TimerRunning", "True" });
                replaceSetting(new string[] { "cStopwatch", this.Name, "EndDateTime" }, new string[] { "cStopwatch", this.Name, "EndDateTime", "NULL" });
            }
            else
            {
                string stop_ticks = DateTime.Now.Ticks.ToString();
                startToolStripMenuItem.Text = "(Re)Start";
                sw_timer.Stop();
                replaceSetting(new string[] { "cStopwatch", this.Name, "TimerRunning" }, new string[] { "cStopwatch", this.Name, "TimerRunning", "False" });
                replaceSetting(new string[] { "cStopwatch", this.Name, "EndDateTime" }, new string[] { "cStopwatch", this.Name, "EndDateTime", stop_ticks });
                
                //we update the label text one more time to make sure it reflects what we save to settings
                setLabel_TimeText(new DateTime(Convert.ToInt64(stop_ticks)));
            }
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
            string string_span = tmp.TotalHours.ToString().PadLeft(2, '0').Substring(0, tmp.TotalHours.ToString().PadLeft(2, '0').IndexOf("."))  + ":" + tmp.Minutes.ToString().PadLeft(2,'0') + ":" + tmp.Seconds.ToString().PadLeft(2, '0') + "." + tmp.Milliseconds.ToString().PadLeft(2, '0').Substring(0, 2) ;

            label_time.Text = tmp.ToString().PadRight(12,'0');

            //tmp removal
            //label_time.Text = tmp.ToString().Substring(0, tmp.ToString().IndexOf(".") + 3);
            //while (label_time.Width > this.Width)
            //{
            //    label_time.Font = new Font(label_time.Font.FontFamily, label_time.Font.Size - 1);
            //}
            
            label_started_time.Text = "cStopwatch started on: " + startTime.ToString();
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
            setLabel_TimeText(DateTime.Now);
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
                setLabel_TimeText(DateTime.Now);
            }

            //see if timer was running on close
            bool timer_running = Convert.ToBoolean(getSpecificSetting(new string[] { "cStopwatch", this.Name, "TimerRunning" })[0]);

            //get end time (Null if not applicable)
            string string_end_time = getSpecificSetting(new string[] { "cStopwatch", this.Name, "EndDateTime" })[0];

            //if the timer was left running, start it again, and set text to stop
            if (timer_running)
            {
                sw_timer.Start();
                startToolStripMenuItem.Text = "Stop";
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
        /// this will save the new location of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cStopwatch_Move(object sender, EventArgs e)
        {
            if (CompletedForm_Load == true)
            {
                List<string> list_find = new List<string>();
                List<string> list_replace = new List<string>();

                list_find.Add("cStopwatch");
                list_find.Add(this.Name);
                list_find.Add("Location");

                foreach (string s in list_find)
                {
                    list_replace.Add(s);
                }

                list_replace.Add(this.Location.X.ToString());
                list_replace.Add(this.Location.Y.ToString());

                replaceSetting(list_find, list_replace);
            }
        }


    }
}
