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
using System.ComponentModel;

using ModLibrary.Comm;

namespace MOD.Pages
{
    /// <summary>
    /// ReamlTimePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RealtimePage : Page
    {
        #region dependency property
        public static readonly DependencyProperty MyLogProperty = DependencyProperty.Register("MyLog", typeof(ObservableCollection<LogModel>), typeof(RealtimePage), new PropertyMetadata(new ObservableCollection<LogModel>(), null));
        public static readonly DependencyProperty DisplayLogLevelProperty = DependencyProperty.Register("DisplayLogLevel", typeof(LogLevel), typeof(RealtimePage), new PropertyMetadata(LogLevel.DEBUG, null));
        public static readonly DependencyProperty MaxLogCountProperty = DependencyProperty.Register("MaxLogCount", typeof(int), typeof(RealtimePage), new PropertyMetadata(20, null));
        public static readonly DependencyProperty NotificationLogLevelProperty = DependencyProperty.Register("NotificationLogLevel", typeof(LogLevel), typeof(RealtimePage), new PropertyMetadata(LogLevel.ERROR, null));
        #endregion

        #region public property
        public ObservableCollection<LogModel> MyLog
        {
            get => GetValue(MyLogProperty) as ObservableCollection<LogModel>;
            set => SetValue(MyLogProperty, value);
        }

        public LogLevel DisplayLogLevel
        {
            get => (LogLevel)GetValue(DisplayLogLevelProperty);
            set => SetValue(DisplayLogLevelProperty, value);
        }

        public int MaxLogCount
        {
            get => (int)GetValue(MaxLogCountProperty);
            set => SetValue(MaxLogCountProperty, value);
        }

        public LogLevel NotificationLogLevel
        {
            get => (LogLevel)GetValue(NotificationLogLevelProperty);
            set => SetValue(NotificationLogLevelProperty, value);
        }
        #endregion

        #region SizeChangedEvent
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

            if (TotalWidth != 0 && view.ActualWidth > TotalWidth)
            {
                gridView.Columns[gridView.Columns.Count - 1].Width = view.ActualWidth - TotalWidth;
            }
        }
        #endregion

        #region Notification Event
        public event Action<LogModel> NotificationEvent;
        #endregion

        #region Constructor
        public RealtimePage()
        {
            InitializeComponent();

            App.log.LogEvent += (p) => {
                if (p.Level >= DisplayLogLevel)
                {
                    if (MyLog.Count >= MaxLogCount)
                        MyLog.RemoveAt(0);
                    MyLog.Add(p);
                }
                if (p.Level >= NotificationLogLevel)
                {
                    NotificationEvent?.Invoke(p);
                }
            };
        }
        #endregion
    }
}
