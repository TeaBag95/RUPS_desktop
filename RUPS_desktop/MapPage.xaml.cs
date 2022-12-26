using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Net.Http;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace RUPS_desktop
{
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {

        public MapPage()
        {
            InitializeComponent();
            myMap.Mode = new AerialMode(true);
        }

        private void myMap_Loaded(object sender, RoutedEventArgs e)
        {
            myMap.Center = new Location(46.14, 15);
            myMap.ZoomLevel = 8;

        }

        private void FindLocation_btn_Click(object sender, RoutedEventArgs e)
        {
            Location curr = myMap.Center;

            //print
            Trace.WriteLine("lat: " + curr.Latitude + " long: " + curr.Longitude);

            string url = "http:\\\\localhost:3002/api/station/nearest?lat=" + curr.Latitude.ToString().Replace(',', '.') + "&lon=" + curr.Longitude.ToString().Replace(',', '.');
            Trace.WriteLine(url);
            string html = string.Empty;
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Station myDeserializedClass = JsonConvert.DeserializeObject<Station>(html);

            Trace.WriteLine(myDeserializedClass.geometry.coordinates[0] + " " + myDeserializedClass.geometry.coordinates[1]);
            myMap.Center= new Location(myDeserializedClass.geometry.coordinates[1], myDeserializedClass.geometry.coordinates[0]);
            myMap.ZoomLevel = 18;


        }

    }
}
