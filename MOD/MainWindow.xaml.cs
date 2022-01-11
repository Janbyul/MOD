using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
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

        private readonly MainViewModel VM_Main = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM_Main;
            realtimePage.NotificationEvent += (p) => { VM_Main.Message = $"확인 요망 : [{p.Date:yyyy-MM-dd HH:mm:ss.fff}] [{p.Level}] [{p.ClassName}]"; };
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
                case "CreateError":
                    App.log.Fatal("수동 오류 생성");
                    break;
            }
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.1)
            };
            timer.Tick += (p, q) => { OnPropertyChanged("HomeTime"); };
            timer.Start();
        }

        public DateTime HomeTime => DateTime.Now;
        private string message = "";
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
