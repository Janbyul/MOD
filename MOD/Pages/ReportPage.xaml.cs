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
    /// ReportPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReportPage : Page
    {
        #region dependency property
        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register("StartDate", typeof(DateTime), typeof(ReportPage), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(StartDateChangedCallback)));
        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register("EndDate", typeof(DateTime), typeof(ReportPage), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(EndDateChangedCallback)));
        public static readonly DependencyProperty LogLevelProperty = DependencyProperty.Register("ILogLevel", typeof(LogLevel), typeof(ReportPage), new PropertyMetadata(LogLevel.INFO, new PropertyChangedCallback(LogLevelChangedCallback)));
        public static readonly DependencyProperty MyLogProperty = DependencyProperty.Register("MyLog", typeof(ObservableCollection<LogModel>), typeof(ReportPage), new PropertyMetadata(new ObservableCollection<LogModel>(), null));
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
                ReportPage rp = sender as ReportPage;
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

        #region SizeChanged Event
        private void Logview_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView view = sender as ListView;
            GridView gridView = view.View as GridView;

            double TotalWidth = 0.0;

            for (int i = 0; i < gridView.Columns.Count - 1; i++)
            {
                TotalWidth += gridView.Columns[i].ActualWidth;
            }

            if (TotalWidth != 0 && view.ActualWidth > TotalWidth)
            {
                gridView.Columns[gridView.Columns.Count - 1].Width = view.ActualWidth - TotalWidth;
            }
        }
        #endregion

        #region Loaded Event
        private void Logview_Loaded(object sender, RoutedEventArgs e)
        {
            ListView view = sender as ListView;
            GridView gridView = view.View as GridView;

            double TotalWidth = 0.0;

            for (int i = 0; i < gridView.Columns.Count - 1; i++)
            {
                TotalWidth += gridView.Columns[i].ActualWidth;
            }

            if (TotalWidth != 0 &&  view.ActualWidth > TotalWidth)
            {
                gridView.Columns[gridView.Columns.Count - 1].Width = view.ActualWidth - TotalWidth;
            }
        }
        #endregion

        #region ColumnHeader Sort
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader ColumnHeader)
            {
                if (ColumnHeader.Role != GridViewColumnHeaderRole.Padding)
                {
                    App.log.Debug("GridViewColumnHeader_Click : " + ColumnHeader.Column.Header.ToString());

                    if (ColumnHeader.Tag == null || ColumnHeader.Tag.ToString().Equals("ASC"))
                    {
                        /* 
                         * 패턴매칭 C# 8.0 이상부터 가능
                        MyLog = ColumnHeader.Column.Header.ToString() switch
                        {
                            "Date" => new ObservableCollection<LogModel>(MyLog.OrderBy(o => o.Date));
                        };
                        */

                        if (ColumnHeader.Column.Header.ToString().Equals("Date"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderByDescending(o => o.Date));
                        }
                        else if (ColumnHeader.Column.Header.ToString().Equals("Level"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderByDescending(o => o.Level));
                        }
                        else if (ColumnHeader.Column.Header.ToString().Equals("Class"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderByDescending(o => o.ClassName));
                        }
                        else if (ColumnHeader.Column.Header.ToString().Equals("Function"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderByDescending(o => o.FunctionName));
                        }
                        else if (ColumnHeader.Column.Header.ToString().Equals("Message"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderByDescending(o => o.Message));
                        }
                        ColumnHeader.Tag = "DESC";
                    }
                    else
                    {
                        if (ColumnHeader.Column.Header.ToString().Equals("Date"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderBy(o => o.Date));
                        }
                        else if (ColumnHeader.Column.Header.ToString().Equals("Level"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderBy(o => o.Level));
                        }
                        else if (ColumnHeader.Column.Header.ToString().Equals("Class"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderBy(o => o.ClassName));
                        }
                        else if (ColumnHeader.Column.Header.ToString().Equals("Function"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderBy(o => o.FunctionName));
                        }
                        else if (ColumnHeader.Column.Header.ToString().Equals("Message"))
                        {
                            MyLog = new ObservableCollection<LogModel>(MyLog.OrderBy(o => o.Message));
                        }
                        ColumnHeader.Tag = "ASC";
                    }
                }
            }
        }
        #endregion

        #region Constructor
        public ReportPage()
        {
            InitializeComponent();
            MyLog = new ObservableCollection<LogModel>(LogParser.GetLog(StartDate, EndDate, ILogLevel));
        }
        #endregion
    }
}
