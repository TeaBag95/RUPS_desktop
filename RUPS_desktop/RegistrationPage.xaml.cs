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
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        public void Registration_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = "http://localhost:3002/api/public/register";
                Trace.WriteLine(url);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"email\":\"" + email.Text + "\"," +
                                  "\"password\":\""+ Password.Password + "\"}";

                    streamWriter.Write(json);
                }

            
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    LoginPage login = new LoginPage();
                    NavigationService.Navigate(login);
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Content_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!email.Text.Equals(String.Empty) && Password.Password.Equals(PasswordConf.Password) && !Password.Password.Equals(String.Empty))
            {
                Register_btn.IsEnabled = true;
            }
            else
            {
                Register_btn.IsEnabled = false;
            }
        }
    }
}
