//cDashboard - An overlay for Microsoft Windows
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
        /// api key for weather underground api
        /// </summary>
        private readonly string APIKEY = "9252b3b729b18d24";

        /// <summary>
        /// a dictionary from the json for local conditions
        /// </summary>
        private Dictionary<string, dynamic> dict_conditions;

        /// <summary>
        /// a dictionary from the json for local forecast
        /// </summary>
        private Dictionary<string, dynamic> dict_forecast;

        /// <summary>
        /// zip code represented as a string
        /// </summary>
        public string ZipCode = "";
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
        public cWeather(string string_zip)
        {
            InitializeComponent();

            ZipCode = string_zip;
            if (ZipCode == "NULL")
            {
                hideAllPanelsExcept(ref panel_zip_input);
            }
        }
        #endregion

        #region Retrival of Weather Information
        /// <summary>
        /// updates label_zip with loc
        /// makes sure the sizing of the label is good
        /// </summary>
        /// <param name="loc">text to update label with</param>
        private void updateLabelZip(string loc)
        {
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
            if (ZipCode == "NULL" || ZipCode == "")
            {
                hideAllPanelsExcept(ref panel_zip_input);
                return;
            }

            if (!isOnline())
            {
                label_instructions.Text = "  Couldn't complete request, you are offline";
                return;
            }

            hideAllPanelsExcept(ref panel_refreshing);
            //API call #1
            //API call #2
            backgroundWorker_refresh.RunWorkerAsync();
        }

        /// <summary>
        /// returns current wind conditions
        /// </summary>
        /// <returns></returns>
        private string getWind()
        {
            int wind = Convert.ToInt16(dict_conditions["current_observation"]["wind_mph"]);
            if (wind != 0)
            {
                return "Wind: " + wind + " MPH " + dict_conditions["current_observation"]["wind_dir"];
            }
            else
            {
                return "Calm, No Wind";
            }
        }

        /// <summary>
        /// background worker for grabbing data from API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_refresh_DoWork(object sender, DoWorkEventArgs e)
        {
            dict_conditions = getDictFromJsonUrl("http://api.wunderground.com/api/" + APIKEY + "/conditions/q/" + ZipCode + ".json");
            dict_forecast = getDictFromJsonUrl("http://api.wunderground.com/api/" + APIKEY + "/forecast/q/" + ZipCode + ".json");
        }

        /// <summary>
        /// after completion of backgroundworker, update ui
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_refresh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if dict_conditions is null, invalid ZipCode was provided
            if (dict_conditions == null)
                return;

            //updates the label_zip with correct location and size
            updateLabelZip(dict_conditions["current_observation"]["display_location"]["full"]);

            //updates pictureBox_weather_img with the current icon
            pictureBox_weather_img.Load(dict_conditions["current_observation"]["icon_url"]);

            //set label to say when data was fetched
            refreshDataToolStripMenuItem.Text = "Refresh Data Now   -   (Last Updated: " + DateTime.Now.ToString() + ")";

            //make sure panel_zip_input is hidden and panel_weather_ui is shown
            hideAllPanelsExcept(ref panel_weather_ui);

            //handle fahrenheit
            if (getSpecificSetting(new string[] { "cDash", "cWeather", "Unit" })[0] == "F")
            {
                //get current temperature and put into label_current_temp
                label_current_temp.Text = Convert.ToInt16(dict_conditions["current_observation"]["temp_f"]) + "°F";

                //get high and put into label_high
                label_high.Text = "High: " + dict_forecast["forecast"]["simpleforecast"]["forecastday"][0]["high"]["fahrenheit"] + "°F";

                //get low and put into label_low
                label_low.Text = "Low: " + dict_forecast["forecast"]["simpleforecast"]["forecastday"][0]["low"]["fahrenheit"] + "°F";
            }
            else  //handle celsius
            {
                //get current temperature and put into label_current_temp
                label_current_temp.Text = Convert.ToInt16(dict_conditions["current_observation"]["temp_c"]) + "°C";

                //get high and put into label_high
                label_high.Text = "High: " + dict_forecast["forecast"]["simpleforecast"]["forecastday"][0]["high"]["celsius"] + "°C";

                //get low and put into label_low
                label_low.Text = "Low: " + dict_forecast["forecast"]["simpleforecast"]["forecastday"][0]["low"]["celsius"] + "°C";
            }

            //get current humidity and put into label_humidity
            label_humidity.Text = "Humidity: " + dict_conditions["current_observation"]["relative_humidity"];

            //get current wind and put into label_wind
            label_wind.Text = getWind();

            //get precip chance and put into label_pop
            label_pop.Text = dict_forecast["forecast"]["simpleforecast"]["forecastday"][0]["pop"] + "% Chance of Rain";

            //get weather string and put into label_weather
            label_weather.Text = dict_conditions["current_observation"]["weather"];

            hideAllPanelsExcept(ref panel_weather_ui);
        }

        /// <summary>
        /// returns dictionary from url and its json data
        /// </summary>
        /// <param name="url"> string representing the url to grab data from</param>
        /// <returns></returns>
        private Dictionary<string, dynamic> getDictFromJsonUrl(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string string_url_text = reader.ReadToEnd();

            Dictionary<string, dynamic> dict = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(string_url_text);

            //special handling for weather api
            if (dict.ContainsKey("response"))
            {
                //special handling for error
                if (dict["response"].ContainsKey("error"))
                {
                    //invoke on proper thread
                    panel_zip_input.Invoke(new MethodInvoker(delegate
                    {
                        hideAllPanelsExcept(ref panel_zip_input);
                        label_instructions.Text = "       Location not found, please try again";
                    }));

                    ZipCode = "NULL";
                    replaceSetting(new string[] { "cWeather", this.Name, "ZipCode" }, new string[] { "cWeather", this.Name, "ZipCode", "NULL" });

                    return null;
                }

                //special handling for ambiguity
                if (dict["response"].ContainsKey("results"))
                {
                    string zmw = dict["response"]["results"][0]["zmw"];
                    ZipCode = "zmw:" + zmw;
                    replaceSetting(new string[] { "cWeather", this.Name, "ZipCode" }, new string[] { "cWeather", this.Name, "ZipCode", ZipCode });
                    string new_url = url.Substring(0, url.LastIndexOf(@"/") + 1) + ZipCode + ".json";
                    return getDictFromJsonUrl(new_url);
                }
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
            ZipCode = rtb_input.Text;
            replaceSetting(new string[] { "cWeather", this.Name, "ZipCode" }, new string[] { "cWeather", this.Name, "ZipCode", ZipCode });

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
            if (ZipCode == "NULL" || ZipCode == "")
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
            System.Diagnostics.Process.Start(dict_conditions["current_observation"]["forecast_url"] + "?apiref=8468212cff0ab452");
        }

        /// <summary>
        /// hides all panels on form except for p
        /// </summary>
        /// <param name="p">panel to be shown</param>
        private void hideAllPanelsExcept(ref Panel p)
        {
            label_instructions.Text = "Input a zip code, city, state, or country, city.";

            foreach (Panel c in this.Controls.OfType<Panel>())
            {
                if (c != p)
                {
                    c.Hide();
                }
            }
            p.Show();
        }

        /// <summary>
        /// autolocates based on IP
        /// uses (Awesome, 100% FREE) Telize.com/geoip API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_auto_locate_Click(object sender, EventArgs e)
        {

            if (!isOnline())
            {
                label_instructions.Text = "  Couldn't complete request, you are offline";
                return;
            }
            Dictionary<string, dynamic> dict_ip = getDictFromJsonUrl(@"http://www.telize.com/geoip");
            string latitude = dict_ip["latitude"].ToString();
            string longitude = dict_ip["longitude"].ToString();

            ZipCode = latitude + "," + longitude;
            replaceSetting(new string[] { "cWeather", this.Name, "ZipCode" }, new string[] { "cWeather", this.Name, "ZipCode", ZipCode });

            //gets weather info based on new geolocated location
            getWeatherInfo();
        }

        /// <summary>
        /// true: Online
        /// false: Offline
        /// use Google Free Public DNS address as Ping-Point
        /// </summary>
        /// <returns></returns>
        private bool isOnline()
        {
            //error would mean, fail to ping, so return false for offline
            try
            {
                if (new Ping().Send("8.8.8.8").Status == IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
