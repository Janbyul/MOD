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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModLibrary.GUI
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ImageButton : Button
    {
        #region dependency property
        public static readonly DependencyProperty DefaultImageSourceProperty = DependencyProperty.Register("DefaultImageSource", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null, new PropertyChangedCallback(DefaultImageSourceChangedCallback)));
        public static readonly DependencyProperty PressedImageSourceProperty = DependencyProperty.Register("PressedImageSource", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null, new PropertyChangedCallback(PressedImageSourceChangedCallback)));
        public static readonly DependencyProperty ImageStretchProperty = DependencyProperty.Register("ImageStretch", typeof(Stretch), typeof(ImageButton), new PropertyMetadata(Stretch.None, new PropertyChangedCallback(ImageStretchChangedCallback)));
        #endregion

        #region callback
        private static void DefaultImageSourceChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ImageButton)
            {
                ImageButton imgbtn = sender as ImageButton;
                imgbtn.OnDefaultImageSourceChanged(e.OldValue, e.NewValue);
            }
        }

        private static void PressedImageSourceChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ImageButton)
            {
                ImageButton imgbtn = sender as ImageButton;
                imgbtn.OnPressedImageSourceChanged(e.OldValue, e.NewValue);
            }
        }

        private static void ImageStretchChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ImageButton)
            {
                ImageButton imgbtn = sender as ImageButton;
                imgbtn.OnImageStretchChanged(e.OldValue, e.NewValue);
            }
        }

        #endregion

        #region public property
        public ImageSource DefaultImageSource
        {
            get
            {
                return this.GetValue(DefaultImageSourceProperty) as ImageSource;
            }
            set
            {
                this.SetValue(DefaultImageSourceProperty, value);
            }
        }

        public ImageSource PressedImageSource
        {
            get
            {
                return this.GetValue(PressedImageSourceProperty) as ImageSource;
            }
            set
            {
                this.SetValue(PressedImageSourceProperty, value);
            }
        }

        public Stretch ImageStretch
        {
            get
            {
                return (Stretch)this.GetValue(ImageStretchProperty);
            }
            set
            {
                this.SetValue(ImageStretchProperty, value);
            }
        }
        #endregion

        #region protected method
        protected void OnDefaultImageSourceChanged(object oldValue, object newValue)
        {
            this.DefaultImageSource = newValue as ImageSource;
        }

        protected void OnPressedImageSourceChanged(object oldValue, object newValue)
        {
            this.PressedImageSource = newValue as ImageSource;
        }

        protected void OnImageStretchChanged(object oldValue, object newValue)
        {
            this.ImageStretch = (Stretch)newValue;
        }
        #endregion

        #region construct
        public ImageButton()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ImageButton_Loaded);
        }
        #endregion

        #region private event
        void ImageButton_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
