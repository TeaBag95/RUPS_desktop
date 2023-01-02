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
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace RUPS_desktop
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            try {
                var cookies = new CookieContainer();
                string url = "http://localhost:3002/api/public/login";
                Trace.WriteLine(url);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = cookies;
                request.Method = "POST";
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"email\":\"" + email.Text + "\"," +
                                  "\"password\":\"" + password.Password + "\"}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                Trace.WriteLine("!!!!!!!!!!!!!!!!!!! "+ httpResponse.Headers[HttpResponseHeader.SetCookie]);
                App.Current.Properties["Cookie"] = httpResponse.Headers[HttpResponseHeader.SetCookie];
                //Application.SetCookie("", httpResponse.Cookies);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    Trace.WriteLine(result);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    LoginWindow parentWindow = (LoginWindow)Window.GetWindow(this);
                    parentWindow.Close();
                }

            } catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
