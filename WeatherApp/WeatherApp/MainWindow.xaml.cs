using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;


namespace WeatherApp
{
    public partial class MainWindow : Window
    {

        string condition;
        private const String APIKEY = "a41e65344a73472487fb522404d44c03";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_00_Click(object sender, RoutedEventArgs e)
        {
            string location;
            location = txtbox_00.Text;
            label_03.Content = $"Today in {location}:";
            label_03.Opacity = 100;
            label_03.HorizontalContentAlignment = HorizontalAlignment.Center;
            img_00.Opacity = 0;img_01.Opacity = 0;img_02.Opacity = 0;img_03.Opacity = 0;img_04.Opacity = 0;img_05.Opacity = 0;img_06.Opacity = 0;img_07.Opacity = 0;img_08.Opacity = 0;
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(location)}&appid={APIKEY}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Process the weather information
                    string json = reader.ReadToEnd();

                    var jss = new JavaScriptSerializer();
                    var dict = jss.Deserialize<Dictionary<string, dynamic>>(json);

                    // Print out the current weather conditions
                    label_04.Content = ($"Conditions:  {dict["weather"][0]["main"]}");
                    condition = Convert.ToString(label_04.Content);

                    label_05.Content = ($"Temperature:  {Math.Round(Convert.ToDouble(dict["main"]["temp"]) - 273.15)}°C");

                    label_06.Content = ($"Humidity:  {dict["main"]["humidity"]}%");

                    label_07.Content = ($"Wind:  {dict["wind"]["speed"]} m/s");
                }
            }
            catch (Exception)
            {
                label_03.Opacity = 0;
                MessageBox.Show($" {location} is not a valid location. Please try again.", "Error!");
            }

            switch(condition)
            {
                case "Conditions:  Broken Clouds":
                    img_00.Opacity = 100;
                    break;
                case "Conditions:  Clear":
                    img_01.Opacity = 100;
                    break;
                case "Conditions:  Clouds":
                    img_02.Opacity = 100;
                    break;
                case "Conditions:  Few Clouds":
                    img_03.Opacity = 100;
                    break;
                case "Conditions:  Mist":
                    img_04.Opacity = 100;
                    break;
                case "Conditions:  Rain":
                    img_05.Opacity = 100;
                    break;
                case "Conditions:  Drizzle":
                    img_06.Opacity = 100;
                    break;
                case "Conditions:  Snow":
                    img_07.Opacity = 100;
                    break;
                case "Conditions:  Thunderstorm":
                    img_08.Opacity = 100;
                    break;
                default:
                    break;
            }       
        }
        }
    }
