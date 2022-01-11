using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ModLibrary.Comm;

namespace MOD.Pages
{
    /// <summary>
    /// DashboardPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DashboardPage : Page
    {
        #region dependency property
        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register("StartDate", typeof(DateTime), typeof(DashboardPage), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(StartDateChangedCallback)));
        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register("EndDate", typeof(DateTime), typeof(DashboardPage), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(EndDateChangedCallback)));
        public static readonly DependencyProperty LogLevelProperty = DependencyProperty.Register("ILogLevel", typeof(LogLevel), typeof(DashboardPage), new PropertyMetadata(LogLevel.INFO, new PropertyChangedCallback(LogLevelChangedCallback)));
        public static readonly DependencyProperty MyLogProperty = DependencyProperty.Register("MyLog", typeof(ObservableCollection<LogModel>), typeof(DashboardPage), new PropertyMetadata(new ObservableCollection<LogModel>(), null));
        #endregion

        #region public property
        public DateTime StartDate
        {
            get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }

        public DateTime EndDate
        {
            get => (DateTime)GetValue(EndDateProperty);
            set => SetValue(EndDateProperty, value);
        }

        public LogLevel ILogLevel
        {
            get => (LogLevel)GetValue(LogLevelProperty);
            set => SetValue(LogLevelProperty, value);
        }

        public ObservableCollection<LogModel> MyLog
        {
            get => GetValue(MyLogProperty) as ObservableCollection<LogModel>;
            set => SetValue(MyLogProperty, value);
        }
        #endregion

        #region callback
        private static void StartDateChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is DashboardPage)
            {
                DashboardPage rp = sender as DashboardPage;
                rp.OnStartDateChanged(e.OldValue, e.NewValue);
            }
        }

        private static void EndDateChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is DashboardPage)
            {
                DashboardPage rp = sender as DashboardPage;
                rp.OnEndDateChanged(e.OldValue, e.NewValue);
            }
        }

        private static void LogLevelChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is DashboardPage)
            {
                DashboardPage rp = sender as DashboardPage;
                rp.OnLogLevelChanged(e.OldValue, e.NewValue);
            }
        }

        #endregion

        #region protected method
        protected void OnStartDateChanged(object oldValue, object newValue)
        {
            if ((DateTime)newValue > EndDate)
            {
                EndDate = (DateTime)newValue;
            }
            StartDate = (DateTime)newValue;

            if (IsLoaded)
                MyLog = new ObservableCollection<LogModel>(LogParser.GetLog(StartDate, EndDate, ILogLevel));
        }

        protected void OnEndDateChanged(object oldValue, object newValue)
        {
            if ((DateTime)newValue < StartDate)
            {
                StartDate = (DateTime)newValue;
            }

            EndDate = (DateTime)newValue > DateTime.Today ? DateTime.Today : (DateTime)newValue;

            if (IsLoaded)
                MyLog = new ObservableCollection<LogModel>(LogParser.GetLog(StartDate, EndDate, ILogLevel));
        }

        protected void OnLogLevelChanged(object oldValue, object newValue)
        {
            ILogLevel = (LogLevel)newValue;

            if (IsLoaded)
                MyLog = new ObservableCollection<LogModel>(LogParser.GetLog(StartDate, EndDate, ILogLevel));
        }
        #endregion

        #region Constructor
        public DashboardPage()
        {
            InitializeComponent();
            MyLog = new ObservableCollection<LogModel>(LogParser.GetLog(StartDate, EndDate, ILogLevel));
        }
        #endregion
    }
}
