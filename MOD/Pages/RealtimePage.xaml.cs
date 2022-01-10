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
using System.ComponentModel;

using ModLibrary.Comm;

namespace MOD.Pages
{
    /// <summary>
    /// ReamlTimePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RealtimePage : Page
    {
        private readonly RealtimeViewModel VM_RealtimePage = new RealtimeViewModel();
        public RealtimePage()
        {
            InitializeComponent();
            DataContext = VM_RealtimePage;

            App.log.LogEvent += (p) => {
                if (p.Level >= App.RealTimeLogMinLogLevel)
                {
                    if (VM_RealtimePage.MyLog.Count >= App.RealTimeLogCount)
                        VM_RealtimePage.MyLog.RemoveAt(0);
                    VM_RealtimePage.AddMyLog(p);
                }
            };
        }
    }

    public class RealtimeViewModel : INotifyPropertyChanged
    {
        private readonly List<LogModel> mylog = new List<LogModel>();
        public List<LogModel> MyLog { get; set; } = new List<LogModel>();

        public void AddMyLog(LogModel logmodel)
        {
            mylog.Add(logmodel);
            MyLog = mylog;
            OnPropertyChanged("MyLog");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
