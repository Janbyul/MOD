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

namespace MOD
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.log.Debug(("yyyy-MM-dd hh:mm:ss") + "TEST");
            Main_Frame.Navigate(new Uri(@"Pages\ReportPage.xaml", UriKind.Relative), "");
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
