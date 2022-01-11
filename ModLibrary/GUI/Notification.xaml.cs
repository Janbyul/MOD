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

namespace ModLibrary.GUI
{
    /// <summary>
    /// Notification.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Notification : UserControl
    {
        #region dependency property
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(Notification), new PropertyMetadata("", new PropertyChangedCallback(MessageChangedCallback)));
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(Notification), new PropertyMetadata(null, null));
        #endregion

        #region public property
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public Brush BackgroundColor
        {
            get => GetValue(BackgroundColorProperty) as Brush;
            set => SetValue(BackgroundColorProperty, value);
        }
        #endregion

        #region callback
        private static void MessageChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is Notification)
            {
                Notification obj = sender as Notification;
                obj.OnMessageChanged(e.OldValue, e.NewValue);
            }
        }
        #endregion

        #region protected method
        protected void OnMessageChanged(object oldValue, object newValue)
        {
            Message = newValue.ToString();
            BackgroundColor = string.IsNullOrEmpty(Message) ? null : new SolidColorBrush(Colors.Red);
        }
        #endregion

        public Notification()
        {
            InitializeComponent();
        }
    }
}
