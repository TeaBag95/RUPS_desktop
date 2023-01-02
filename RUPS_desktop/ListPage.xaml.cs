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
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace RUPS_desktop
{
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ListPage()
        {
            InitializeComponent();
            SearchButton.Click += SearchButton_Click;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text;
            Trace.WriteLine("here");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3002/api/station/" + searchTerm);
                var request = new HttpRequestMessage(HttpMethod.Post, "");
                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3002/api/station/" + searchTerm);

                var response = await client.GetAsync($"?prefix={searchTerm}");
                var responseString = await response.Content.ReadAsStringAsync();

                var stations = JsonConvert.DeserializeObject<List<StationDetails>>(responseString);

                ResultsListView.ItemsSource = stations;
            }
        }
    }

    public class StationDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string url { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
