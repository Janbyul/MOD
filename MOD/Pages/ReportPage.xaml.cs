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

using ModLibrary.Comm;

namespace MOD.Pages
{
    /// <summary>
    /// ReportPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReportPage : Page
    {
        #region dependency property
        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register("StartDate", typeof(DateTime), typeof(ReportPage), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(StartDateChangedCallback)));
        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register("EndDate", typeof(DateTime), typeof(ReportPage), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(EndDateChangedCallback)));
        public static readonly DependencyProperty LogLevelProperty = DependencyProperty.Register("ILogLevel", typeof(LogLevel), typeof(ReportPage), new PropertyMetadata(LogLevel.INFO, new PropertyChangedCallback(LogLevelChangedCallback)));
        public static readonly DependencyProperty MyLogProperty = DependencyProperty.Register("MyLog", typeof(List<LogModel>), typeof(ReportPage), new PropertyMetadata(null, null));
        #endregion

        #region public property
        public DateTime StartDate
        {
            get
            {
                return (DateTime)this.GetValue(StartDateProperty);
            }
            set
            {
                this.SetValue(StartDateProperty, value);
            }
        }

        public DateTime EndDate
        {
            get
            {
                return (DateTime)this.GetValue(EndDateProperty);
            }
            set
            {
                this.SetValue(EndDateProperty, value);
            }
        }

        public LogLevel ILogLevel
        {
            get
            {
                return (LogLevel)this.GetValue(LogLevelProperty);
            }
            set
            {
                this.SetValue(LogLevelProperty, value);
            }
        }

        public LogModel MyLog
        {
            get
            {
                return this.GetValue(MyLogProperty) as LogModel;
            }
            set
            {
                this.SetValue(MyLogProperty, value);
            }
        }
        #endregion

        #region callback
        private static void StartDateChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ReportPage)
            {
                ReportPage rp = sender as ReportPage;
                rp.OnStartDateChanged(e.OldValue, e.NewValue);
            }
        }

        private static void EndDateChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ReportPage)
            {
                ReportPage rp = sender as ReportPage;
                rp.OnEndDateChanged(e.OldValue, e.NewValue);
            }
        }

        private static void LogLevelChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ReportPage)
            {
                ReportPage imgbtn = sender as ReportPage;
                imgbtn.OnLogLevelChanged(e.OldValue, e.NewValue);
            }
        }

        #endregion

        #region protected method
        protected void OnStartDateChanged(object oldValue, object newValue)
        {
            this.StartDate = (DateTime)newValue;
        }

        protected void OnEndDateChanged(object oldValue, object newValue)
        {
            this.EndDate = (DateTime)newValue;
        }

        protected void OnLogLevelChanged(object oldValue, object newValue)
        {
            this.ILogLevel = (LogLevel)newValue;
        }
        #endregion

        public ReportPage()
        {
            InitializeComponent();
        }
    }
}
