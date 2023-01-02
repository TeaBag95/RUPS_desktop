using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
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

namespace RUPS_desktop
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            getNotices();
            var data = getNotices();

            ic.ItemsSource = data;
        }

        public class Notice
        {
            public string title { get; set; }
            public string content { get; set; }

            public Notice(string Title, string Content)
            {
                title = Title;
                content = Content;
            }
        }

        private List<Notice> getNotices()
        {
            string url = "http://localhost:3002/api/notice";
            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            Trace.WriteLine("-------------"+html);
            dynamic myDeserializedClass = JsonConvert.DeserializeObject<dynamic>(html);
            dynamic notices = myDeserializedClass.notices;
            var data = new List<Notice> {};

            //data.Add(new Notice("title1", "content1"));
            foreach(var item in notices)
            {
                string title = item.title;
                string content = item.content;
                data.Add(new Notice(title, content));
            }
            Trace.WriteLine("------------------" + data[0].title);
            return data;
        }

        public void postNotice(object sender, RoutedEventArgs e)
        {
            try
            {
                var cookies = new CookieContainer();
                string url = "http://localhost:3002/api/notice";
                var request = (HttpWebRequest)WebRequest.Create(url);
                string cookie = App.Current.Properties["Cookie"].ToString();
                request.Headers.Add("Cookie", cookie);
                request.Method = "POST";
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"title\":\"" + title.Text + "\"," +
                                  "\"content\":\"" + content.Text + "\"}";

                    streamWriter.Write(json);
                }


                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var data = getNotices();
                    ic.ItemsSource = data;
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
