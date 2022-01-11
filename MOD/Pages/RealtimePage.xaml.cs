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
        #endregion

        #region public property
        public ObservableCollection<LogModel> MyLog
        {
            get => GetValue(MyLogProperty) as ObservableCollection<LogModel>;
            set => SetValue(MyLogProperty, value);
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

        #region Constructor
        public RealtimePage()
        {
            InitializeComponent();

            App.log.LogEvent += (p) => {
                if (p.Level >= App.RealTimeLogMinLogLevel)
                {
                    if (MyLog.Count >= App.RealTimeLogCount)
                        MyLog.RemoveAt(0);
                    MyLog.Add(p);
                }
            };
        }
        #endregion
    }
}
