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

namespace RUPS_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomePage homePage = new HomePage();
        ListPage listPage = new ListPage();
        MapPage mapPage = new MapPage();

        Color selected = (Color)ColorConverter.ConvertFromString("#002171");
        Color unselected = (Color)ColorConverter.ConvertFromString("#0d47a1");
        Color selectedText = (Color)ColorConverter.ConvertFromString("#fbc02d");

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = homePage;
            HomePage_btn.Background = new SolidColorBrush(selected);
        }

        private void HomePage_btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = homePage;
            HomePage_btn.Background = new SolidColorBrush(selected);
            ListPage_btn.Background = new SolidColorBrush(unselected);
            MapPage_btn.Background = new SolidColorBrush(unselected);

            HomePage_btn.Foreground = new SolidColorBrush(selectedText);
            ListPage_btn.Foreground = Brushes.White;
            MapPage_btn.Foreground = Brushes.White;
        }

        private void ListPage_btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = listPage;
            HomePage_btn.Background = new SolidColorBrush(unselected);
            ListPage_btn.Background = new SolidColorBrush(selected);
            MapPage_btn.Background = new SolidColorBrush(unselected);

            HomePage_btn.Foreground = Brushes.White;
            ListPage_btn.Foreground = new SolidColorBrush(selectedText);
            MapPage_btn.Foreground = Brushes.White;
        }

        private void MapPage_btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = mapPage;
            HomePage_btn.Background = new SolidColorBrush(unselected);
            ListPage_btn.Background = new SolidColorBrush(unselected);
            MapPage_btn.Background = new SolidColorBrush(selected);

            HomePage_btn.Foreground = Brushes.White;
            ListPage_btn.Foreground = Brushes.White;
            MapPage_btn.Foreground = new SolidColorBrush(selectedText);
        }
    }
}
