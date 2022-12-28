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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3002/api/station/");
                var request = new HttpRequestMessage(HttpMethod.Post, "");
                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3002/api/station/");

                var response = await client.GetAsync($"?prefix={searchTerm}");
                var responseString = await response.Content.ReadAsStringAsync();

                var stations = JsonConvert.DeserializeObject<List<Stations>>(responseString);

                ResultsListView.ItemsSource = stations;
            }
        }
    }

    public class Stations
    {
        public string Name { get; set; }
    }
}
