//This file is part of cDashboard
//cDashboard - An information-based overlay for Microsoft Windows
//cWeather - A weather forcast widget for cDashboard 
//(C) Charles Machalow 2014 under the MIT License
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Script.Serialization;

namespace cDashboard
{
    public partial class cWeather : cForm
    {
        #region Global Variables
        /// <summary>
        /// APPID used by the Yahoo API
        /// </summary>
        private readonly string APPID = "dj0yJmk9b1dCT3RzQ0xxU2lSJmQ9WVdrOVRrZE9ZV042TkdNbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD0yOA--";

        /// <summary>
        /// a dictionary from the json for local forecast
        /// from Yahoo
        /// </summary>
        private Dictionary<string, dynamic> dict_y_forecast;

        /// <summary>
        /// Yahoo Where on Earth ID represented by a string
        /// </summary>
        public string WOEID = "";

        /// <summary>
        /// flag will be tripped to say if the user is offline
        /// </summary>
        private bool OfflineFlag = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public cWeather()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with zip
        /// </summary>
        public cWeather(string string_w)
        {
            InitializeComponent();

            WOEID = string_w;
            if (WOEID == "NULL" || WOEID == "" || WOEID == null)
            {
                hideAllPanelsExcept(ref panel_zip_input);
            }
        }
        #endregion

        #region Retrival of Weather Information
        /// <summary>
        /// updates label_zip with loc
        /// makes sure the sizing of the label is good
        /// updated to remove Yahoo prefix
        /// </summary>
        /// <param name="loc">text to update label with</param>
        private void updateLabelZip(string loc)
        {
            //remove Yahoo prefix
            loc = loc.Substring(loc.IndexOf("Yahoo! Weather - ") + 17);

            //remove whitespace and get rid of possible trailing commas
            loc = loc.Trim();
            if (loc[loc.Length - 1] == ',')
                loc = loc.Substring(0, loc.Length - 1);

            //update label_zip to have the observation display location
            label_zip.Text = loc;

            //makes sure the name fits while having a max size of 14.25f
            label_zip.Font = new Font(label_zip.Font.FontFamily, 14.25f, label_zip.Font.Style);
            while (label_zip.Width < System.Windows.Forms.TextRenderer.MeasureText(label_zip.Text, new Font(label_zip.Font.FontFamily, label_zip.Font.Size, label_zip.Font.Style)).Width)
            {
                label_zip.Font = new Font(label_zip.Font.FontFamily, label_zip.Font.Size - 0.5f, label_zip.Font.Style);
            }
        }

        /// <summary>
        /// gets weather info from API and sets things properly
        /// </summary>
        public void getWeatherInfo()
        {
            //if location is null, don't bother trying to get data
            if (WOEID == "NULL" || WOEID == "" || WOEID == null)
            {
                hideAllPanelsExcept(ref panel_zip_input);
                label_instructions.Text = "       Location not found, please try again";
                return;
            }

            //return if worker is busy
            if (backgroundWorker_refresh.IsBusy)
                return;

            hideAllPanelsExcept(ref panel_refreshing);

            //API Calls for WOEID, Weather
            backgroundWorker_refresh.RunWorkerAsync();
        }

        /// <summary>
        /// background worker for grabbing data from API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_refresh_DoWork(object sender, DoWorkEventArgs e)
        {
            //at this point a WOEID should exist

            dict_y_forecast = getDictFromJsonUrl("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%3D" + WOEID + "&format=json&u=f");
        }

        /// <summary>
        /// after completion of backgroundworker, update ui
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_refresh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this can only happen if something went wrong
            //ex: no internet
            if (dict_y_forecast == null)
                return;

            //updates the label_zip with correct location and size
            updateLabelZip(dict_y_forecast["query"]["results"]["channel"]["title"]);

            //updates pictureBox_weather_img with the current icon
            pictureBox_weather_img.Load(getImageURLFromYDescription(dict_y_forecast["query"]["results"]["channel"]["item"]["description"]));

            //set label to say when data was fetched
            refreshDataToolStripMenuItem.Text = "Refresh Data Now   -   (Last Updated: " + DateTime.Now.ToString() + ")";

            //make sure panel_zip_input is hidden and panel_weather_ui is shown
            hideAllPanelsExcept(ref panel_weather_ui);

            //handle fahrenheit
            if (getSpecificSetting(new string[] { "cDash", "cWeather", "Unit" })[0] == "F")
            {
                //get current temperature and put into label_current_temp
                label_current_temp.Text = Convert.ToInt16(dict_y_forecast["query"]["results"]["channel"]["item"]["condition"]["temp"]) + "°F";

                //get high and put into label_high
                label_high.Text = "High: " + dict_y_forecast["query"]["results"]["channel"]["item"]["forecast"][0]["high"] + "°F";

                //get low and put into label_low
                label_low.Text = "Low: " + dict_y_forecast["query"]["results"]["channel"]["item"]["forecast"][0]["low"] + "°F";
            }
            else  //handle celsius
            {
                //get current temperature and put into label_current_temp
                label_current_temp.Text = (getCelsius(Convert.ToDouble(dict_y_forecast["query"]["results"]["channel"]["item"]["condition"]["temp"]))) + "°C";

                //get high and put into label_high
                label_high.Text = "High: " + (getCelsius(Convert.ToDouble(dict_y_forecast["query"]["results"]["channel"]["item"]["forecast"][0]["high"]))) + "°C";

                //get low and put into label_low
                label_low.Text = "Low: " + (getCelsius(Convert.ToDouble(dict_y_forecast["query"]["results"]["channel"]["item"]["forecast"][0]["low"]))).ToString() + "°C";
            }


            //get current humidity and put into label_humidity
            label_humidity.Text = "Humidity: " + dict_y_forecast["query"]["results"]["channel"]["atmosphere"]["humidity"] + "%";

            //get current wind and put into label_wind
            label_wind.Text = getWind();

            //get visibility, chance and put into label_visibility
            label_precip.Text = getPrecip();

            //get weather string and put into label_weather
            label_weather.Text = (dict_y_forecast["query"]["results"]["channel"]["item"]["condition"]["text"]).Trim();

            //bring up the weather
            hideAllPanelsExcept(ref panel_weather_ui);
        }

        /// <summary>
        /// get string relating to precipitation/later conditions 
        /// </summary>
        /// <returns></returns>
        private string getPrecip()
        {
            string baro_state = dict_y_forecast["query"]["results"]["channel"]["atmosphere"]["rising"];

            //steady
            if (baro_state == "0")
            {
                return "➔ Similar";
            }
            else if (baro_state == "1") //rising
            {
                return "➔ Clearing";
            }
            else if (baro_state == "2") //falling
            {
                return "➔ Precipitation";
            }

            //shouldn't ever happen
            return "➔ Odd Conditions";
        }

        /// <summary>
        /// uses the Yahoo API to get a WOEID (Where on Earth ID) for a location
        /// </summary>
        /// <param name="zip">given location as a string</param>
        /// <returns></returns>
        private string getWOEID(string zip)
        {
            Dictionary<string, dynamic> d_tmp = getDictFromJsonUrl("http://where.yahooapis.com/v1/places.q('" + zip + "')?appid=" + APPID + "&format=json");

            if (d_tmp == null)
                return null;
            else
                return (d_tmp["places"]["place"][0]["woeid"]).ToString();
        }

        /// <summary>
        /// returns current wind conditions
        /// </summary>
        /// <returns></returns>
        private string getWind()
        {
            //when there is no wind data, an error will be thrown
            try
            {
                int wind = Convert.ToInt16(dict_y_forecast["query"]["results"]["channel"]["wind"]["speed"]);
                int wind_dir = Convert.ToInt16(dict_y_forecast["query"]["results"]["channel"]["wind"]["direction"]);
                if (wind != 0)
                {
                    int val = Convert.ToInt16((wind_dir / 22.5) + .5);
                    string[] arr = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };

                    return "Wind: " + wind + " MPH " + arr[(val % 16)];
                }
                else
                {
                    return "Calm, No Wind";
                }
            }
            catch
            {
                return "No Wind Data";
            }
        }

        /// <summary>
        /// returns URL for .gif from Yahoo Weather Description
        /// </summary>
        /// <param name="y_description">description text from Yahoo API</param>
        /// <returns></returns>
        private string getImageURLFromYDescription(string y_description)
        {
            try
            {
                return y_description.Substring(y_description.IndexOf("<img src=\"") + 10, y_description.IndexOf(".gif") - 6 - y_description.IndexOf("<img src=\""));
            }
            catch
            {
                return "http://www.yahoo.com/favicon.ico";
            }
        }

        /// <summary>
        /// converts farenheit to celcius
        /// </summary>
        /// <param name="f">temp in f</param>
        /// <returns></returns>
        private int getCelsius(double f)
        {
            return Convert.ToInt16((5.0 / 9.0) * (f - 32.0));
        }

        /// <summary>
        /// returns dictionary from url and its json data
        /// </summary>
        /// <param name="url"> string representing the url to grab data from</param>
        /// <returns></returns>
        private Dictionary<string, dynamic> getDictFromJsonUrl(string url)
        {
            //predeclare before try/catch
            WebRequest request;
            WebResponse response;
            System.IO.StreamReader reader;
            Dictionary<string, dynamic> dict;

            //try/catch is used to detect connectivity
            //any catch should be the result of a lack of internet access
            try
            {
                request = WebRequest.Create(url);
                response = request.GetResponse();
                reader = new System.IO.StreamReader(response.GetResponseStream());
                string string_url_text = reader.ReadToEnd();
                dict = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(string_url_text);
            }
            catch
            {
                OfflineFlag = true;

                hideAllPanelsExcept(ref panel_zip_input);

                //invoke on proper thread if needed
                if (panel_zip_input.InvokeRequired)
                {
                    panel_zip_input.Invoke(new MethodInvoker(delegate
                    {
                        label_instructions.Text = "Couldn't complete request, you may be offline";
                    }));
                }
                else
                {
                    label_instructions.Text = "Couldn't complete request, you may be offline";
                }

                return null;
            }

            //return dict if using Telize for this call
            //avoids confusing logic
            if (url.Contains("telize"))
                return dict;

            //no places found for woeid
            if (url.Contains("where.yah") && Convert.ToInt16(dict["places"]["total"]) == 0)
            {
                //invoke on proper thread
                panel_zip_input.Invoke(new MethodInvoker(delegate
                {
                    hideAllPanelsExcept(ref panel_zip_input);
                    label_instructions.Text = "       Location not found, please try again";
                }));

                WOEID = "NULL";
                replaceSetting(new string[] { "cWeather", this.Name, "WOEID" }, new string[] { "cWeather", this.Name, "WOEID", "NULL" });

                return null;
            }

            //woeid found! but no weather data available (eg: atlantic ocean)
            if (!url.Contains("where.yah") && dict["query"]["results"]["channel"]["title"] == "Yahoo! Weather - Error")
            {
                //invoke on proper thread
                panel_zip_input.Invoke(new MethodInvoker(delegate
                {
                    hideAllPanelsExcept(ref panel_zip_input);
                    label_instructions.Text = "       No data available, please try again";
                }));

                WOEID = "NULL";
                replaceSetting(new string[] { "cWeather", this.Name, "WOEID" }, new string[] { "cWeather", this.Name, "WOEID", "NULL" });

                return null;
            }

            return dict;

        }
        #endregion

        #region MenuStrip Related
        /// <summary>
        /// used to change location for weather data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideAllPanelsExcept(ref panel_zip_input);
            rtb_input.Focus();
        }

        /// <summary>
        /// refreshes weather data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker_refresh.IsBusy)
                getWeatherInfo();
        }

        /// <summary>
        /// Used to move form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// 'x' button to close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// set this cWeather's zip code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_set_Click(object sender, EventArgs e)
        {
            WOEID = getWOEID(rtb_input.Text);

            replaceSetting(new string[] { "cWeather", this.Name, "WOEID" }, new string[] { "cWeather", this.Name, "WOEID", WOEID });

            getWeatherInfo();
        }

        /// <summary>
        /// loading for cWeather
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cWeather_Load(object sender, EventArgs e)
        {
            rtb_input.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;

            //if location is null, don't bother trying to get data
            if (WOEID == "NULL" || WOEID == "" || WOEID == null)
            {
                hideAllPanelsExcept(ref panel_zip_input);
            }

            CompletedForm_Load = true;
        }

        /// <summary>
        /// allow moving form by draging the panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_weather_ui_MouseDown(object sender, MouseEventArgs e)
        {
            dragForm(e);
        }

        /// <summary>
        /// navigate to weatherunderground page for this location
        /// include apiref
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_attrib_Click(object sender, EventArgs e)
        {
            //call fade_out, then open web browser
            ((cDashboard)this.Parent).fade_out();
            System.Diagnostics.Process.Start(dict_y_forecast["query"]["results"]["channel"]["item"]["link"]);
        }

        /// <summary>
        /// hides all panels on form except for p
        /// </summary>
        /// <param name="p">panel to be shown</param>
        private void hideAllPanelsExcept(ref Panel p)
        {
            Panel pp = p;

            //invoke on proper thread (if necessary)
            if (panel_zip_input.InvokeRequired)
            {
                panel_zip_input.Invoke(new MethodInvoker(delegate
                {

                    if (!OfflineFlag)
                        label_instructions.Text = "Input a zip code, city, state, or country, city.";

                    foreach (Panel c in this.Controls.OfType<Panel>())
                    {
                        if (c != pp)
                        {
                            c.Hide();
                        }
                    }
                    pp.Show();
                }));
            }
            else
            {
                if (!OfflineFlag)
                    label_instructions.Text = "Input a zip code, city, state, or country, city.";

                foreach (Panel c in this.Controls.OfType<Panel>())
                {
                    if (c != pp)
                    {
                        c.Hide();
                    }
                }
                pp.Show();
            }

        }

        /// <summary>
        /// autolocates based on IP
        /// uses (Awesome, 100% FREE) Telize.com/geoip API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_auto_locate_Click(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> dict_ip = getDictFromJsonUrl(@"http://www.telize.com/geoip");

            //avoid continuing if getDic
            if (dict_ip == null)
                return;

            string latitude = dict_ip["latitude"].ToString();
            string longitude = dict_ip["longitude"].ToString();

            WOEID = getWOEID(latitude + "," + longitude);
            replaceSetting(new string[] { "cWeather", this.Name, "WOEID" }, new string[] { "cWeather", this.Name, "WOEID", WOEID });

            //gets weather info based on new geolocated location
            getWeatherInfo();
        }
    }
}
