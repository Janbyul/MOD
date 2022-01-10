using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ModLibrary.Comm;

using MOD.Pages;

namespace MOD
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RealtimePage realtimePage = new RealtimePage();

        private MainViewModel mainViewModel = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Tag.ToString())
            {
                case "GoResume":
                    Main_Frame.Navigate(new Uri(@"Pages\ResumePage.xaml", UriKind.Relative));
                    App.log.Debug("GoResumePage");
                    break;
                case "GoRealTime":
                    Main_Frame.Navigate(realtimePage);
                    App.log.Debug("GoRealtimePage");
                    break;
                case "GoReport":
                    Main_Frame.Navigate(new Uri(@"Pages\ReportPage.xaml", UriKind.Relative));
                    App.log.Debug("GoReportPage");
                    break;
                case "GoDashboard":
                    Main_Frame.Navigate(new Uri(@"Pages\DashboardPage.xaml", UriKind.Relative));
                    App.log.Debug("GoDashboardPage");
                    break;
            }
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private string pp = "";
        public string PP
        {
            get => pp;
            set
            {
                pp = value;
                OnPropertyChanged("PP");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
