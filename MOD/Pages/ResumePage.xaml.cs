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

namespace MOD.Pages
{
    /// <summary>
    /// ResumePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ResumePage : Page
    {
        private readonly ResumeViewModel VM_Resume = new ResumeViewModel();
        public ResumePage()
        {
            InitializeComponent();
            DataContext = VM_Resume;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string url = "";

            switch (btn.Tag.ToString())
            {
                case "Blog":
                    url = @"https://janbyul0517.tistory.com/";
                    break;
                case "GitHub":
                    url = @"https://github.com/Janbyul";
                    break;
            }

            try
            {
                _ = System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                App.log.Fatal(ex.Message);
            }
        }
    }
    #region ViewModel
    public class ResumeViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; } = "발전하는 개발자";
        public string SubTitle { get; set; } = "끊임없이 발전하기 위해서 노력하는 개발자 박태영입니다.";
        public ImageSource Photo { get; set; } = GetImageSource("pack://application:,,,/../Resource/IdentificationPhoto.jpg");
        private string email = @"typark0517@gmail.com";
        public string Email
        {
            get => $"Email. {email}";
            set => email = value;
        }
        private string phone = @"010-5886-4789";
        public string Phone
        {
            get => $"Phone. {phone}";
            set => phone = value;
        }

        public static BitmapImage GetImageSource(string uri)
        {
            try
            {
                return new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute)).Clone();
            }
            catch (Exception ex)
            {
                App.log.Fatal(ex.Message);
                return new BitmapImage();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    #endregion
}
