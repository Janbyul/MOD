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
        #region dependency property
        public static readonly DependencyProperty LogModelsProperty = DependencyProperty.Register("LogModels", typeof(List<LogModel>), typeof(RealtimePage), new PropertyMetadata(null, new PropertyChangedCallback(LogModelChangedCallback)));
        #endregion

        #region public property
        public List<LogModel> LogModels
        {
            get => GetValue(LogModelsProperty) as List<LogModel>;
            set => SetValue(LogModelsProperty, value);
        }
        #endregion

        #region callback
        private static void LogModelChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is RealtimePage)
            {
                RealtimePage realtimePage = sender as RealtimePage;
                realtimePage.OnLogModelsChanged(e.OldValue, e.NewValue);
            }
        }
        #endregion

        #region protected method
        protected void OnLogModelsChanged(object oldValue, object newValue)
        {
            LogModels = newValue as List<LogModel>;
        }
        #endregion

        public RealtimePage()
        {
            InitializeComponent();
        }
    }

    public class RealtimeViewModel : INotifyPropertyChanged
    {
        public List<LogModel> logModels;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
