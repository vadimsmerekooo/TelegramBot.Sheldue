using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media;
using WindowAppMain.Classes;
using System.Windows.Media.Animation;
using System.Collections.Generic;
using IFCore;

namespace WindowAppMain.Model.Window.MainWindowPage.AccountPages
{
    /// <summary>
    /// Логика взаимодействия для AccountMainInfo.xaml
    /// </summary>
    public partial class AccountMainInfo : Page
    {
        private BitmapImage changeNewImageUserLogo;
        private IFCore.Person userInformation;
        private MainWindow _mWindow;
        private AccountInfoPage _accWindow;

        public AccountMainInfo(MainWindow mWindow, AccountInfoPage accWindow)
        {
            InitializeComponent();
            // ссылаемся на главное окно
            _mWindow = mWindow;
            // ссылаемся на окно с пользовательской инфой
            _accWindow = accWindow;
            // инициализируем приватное поле с информацией о пользователе 
            userInformation = mWindow._userInfo;
            // изменяем изображения пользователя на странице из главного окна
            ChangeImageLogo(mWindow.ImageLogo.ImageSource);
            // подгружаем данные об пользователе в LsitBox
            ListBoxUserInfo.ItemsSource = userInfoList(mWindow._userInfo);
        }
        // Кнопка изменения изображения
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "Image files (*.BMP, *.JPG, *.PNG)|*.bmp; *.jpg; *.png;"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                changeNewImageUserLogo = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute));
                // изменяем изображения пользователя на выбранное изображение из OpenFileDialog
                ChangeImageLogo(changeNewImageUserLogo);
                SaveImageLogoButton.Visibility = System.Windows.Visibility.Visible;
            }
        }
        // Метод изменеия изображений
        private void ChangeImageLogo(ImageSource logoImageUser)
        {
            AccountLogo.Source = logoImageUser;
            ImageSmallLogo.ImageSource = logoImageUser;
        }
        // Сохранение нового изображения(конфертируем в байты и сохраняем в файл)
        private void SaveImageLogoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                /*
                if (File.Exists($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.dat"))
                {
                    using (FileStream fstream = File.OpenWrite($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.dat"))
                    {
                        byte[] byteArray = decByteOrImage.ImageToByteArray(AccountLogo.Source);
                        fstream.Write(byteArray, 0, byteArray.Length);
                    }

                }
                else
                {
                    using (FileStream fstream = new FileStream($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.dat", FileMode.OpenOrCreate))
                    {
                        byte[] byteArray = decByteOrImage.ImageToByteArray(AccountLogo.Source);
                        fstream.Write(byteArray, 0, byteArray.Length);
                    }
                } */



                //здесь мы переобразуем новое изображение в массив byte и записываем в юзерский файл
                using (FileStream fstream = new FileStream($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.dat", FileMode.OpenOrCreate))
                {
                    byte[] byteArray = new DecoderBitmapImage().ImageToByteArray(AccountLogo.Source);
                    fstream.Write(byteArray, 0, byteArray.Length);
                }
                // изменяем в остальных окнах изображения
                _mWindow.ImageLogo.ImageSource = this.AccountLogo.Source;
                _mWindow.imageLogo = this.AccountLogo.Source as BitmapImage;
                _accWindow.UserLogoImage.ImageSource = this.AccountLogo.Source;
                SaveImageLogoButton.Visibility = System.Windows.Visibility.Hidden;


                // Вызываем на главном окне ThrowMessageGrid
                _mWindow.KindThrowMessage.Foreground = FindResource("ForegroundColorUIElements") as SolidColorBrush;
                _mWindow.KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
                _mWindow.TextBlockMessageThrow.Text = "Изображение успешно изменено!";
                Storyboard sb = _mWindow.FindResource("ShowMessageThrowGrid") as Storyboard;
                sb.Begin();

                /*
                JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(AccountLogo.Source as BitmapSource));
                if (File.Exists($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.jpg"))
                {
                    _mWindow.ImageLogo.ImageSource = null;
                    _mWindow.imageLogo = null;
                    _accWindow.UserLogoImage.ImageSource = null;
                    File.Move($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.jpg", $"tempFileUser_{userInformation.Login}");
                }
                else
                {
                    using (FileStream fileStream = new FileStream($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.jpg", FileMode.Create))
                    {
                        jpegBitmapEncoder.Save(fileStream);
                    }
                }
                */
            }
            catch
            {
                _mWindow.KindThrowMessage.Foreground = FindResource("ErrorForegroundColorUIElements") as SolidColorBrush;
                _mWindow.KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                _mWindow.TextBlockMessageThrow.Text = "Изображение не изменено! Ошибка(";
                Storyboard sb = _mWindow.FindResource("ShowMessageThrowGrid") as Storyboard;
                sb.Begin();
            }
        }
        // метод для формирования ListBox
        private List<UserInfoListClass> userInfoList(Person userInfo)
        {
            switch (userInfo.Status)
            {
                case "Преподаватель":
                    return new List<UserInfoListClass>()
                    {
                        new UserInfoListClass("Имя:", userInfo.Name),
                        new UserInfoListClass("Логин:", userInfo.Login),
                        new UserInfoListClass("Статус:", userInfo.Status)
                    };
                    break;
                case "Студент":
                    return new List<UserInfoListClass>()
                    {
                        new UserInfoListClass("Логин:", userInfo.Login),
                        new UserInfoListClass("Статус:", userInfo.Status),
                        new UserInfoListClass("Отделение:", userInfo.Department),
                        new UserInfoListClass("Группа:", userInfo.Group)
                    };
                    break;
                default:
                    return new List<UserInfoListClass>()
                    {
                        new UserInfoListClass("Данный не получены!", string.Empty)
                    };
                    break;
            }
        }
    }

    // Класс для фрмирования List
    public class UserInfoListClass
    {
        public string ParamByName { get; set; }
        public string PropertiesParam { get; set; }

        public UserInfoListClass(string ParamByName, string PropertiesParam)
        {
            this.ParamByName = ParamByName;
            this.PropertiesParam = PropertiesParam;
        }
    }
}
