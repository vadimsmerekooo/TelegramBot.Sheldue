using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;
using WindowAppMain.Model.DataBaseEF;
using System.Windows.Media;
using WindowAppMain.Classes;

namespace WindowAppMain.Model.Window.MainWindowPage.AccountPages
{
    /// <summary>
    /// Логика взаимодействия для AccountMainInfo.xaml
    /// </summary>
    public partial class AccountMainInfo : Page
    {
        private BitmapImage changeNewImageUserLogo;
        private Person userInformation;
        private MainWindow _mWindow;
        private AccountInfoPage _accWindow;
        public AccountMainInfo(MainWindow mWindow, AccountInfoPage accWindow)
        {
            InitializeComponent();
            _mWindow = mWindow;
            _accWindow = accWindow;
            userInformation = mWindow._userInfo;
            ChangeImageLogo(mWindow.ImageLogo.ImageSource);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "Image files (*.BMP, *.JPG, *.PNG)|*.bmp; *.jpg; *.png;"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                changeNewImageUserLogo = new BitmapImage(imageUri);
                ChangeImageLogo(changeNewImageUserLogo);
                SaveImageLogoButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ChangeImageLogo(ImageSource logoImageUser)
        {
            AccountLogo.Source = logoImageUser;
            ImageSmallLogo.ImageSource = logoImageUser;
        }

        private void SaveImageLogoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DecoderBitmapImage decByteOrImage = new DecoderBitmapImage();
            try
            {
                //if (File.Exists($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.dat"))
                //{
                //    using (FileStream fstream = File.OpenWrite($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.dat"))
                //    {
                //        byte[] byteArray = decByteOrImage.ImageToByteArray(AccountLogo.Source);
                //        fstream.Write(byteArray, 0, byteArray.Length);
                //    }

                //}
                //else
                //{
                //    using (FileStream fstream = new FileStream($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.dat", FileMode.OpenOrCreate))
                //    {
                //        byte[] byteArray = decByteOrImage.ImageToByteArray(AccountLogo.Source);
                //        fstream.Write(byteArray, 0, byteArray.Length);
                //    }
                //}
                using (FileStream fstream = new FileStream($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.dat", FileMode.OpenOrCreate))
                {
                    byte[] byteArray = decByteOrImage.ImageToByteArray(AccountLogo.Source);
                    fstream.Write(byteArray, 0, byteArray.Length);
                }
                //JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                //jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(AccountLogo.Source as BitmapSource));
                //if (File.Exists($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.jpg"))
                //{
                //    _mWindow.ImageLogo.ImageSource = null;
                //    _mWindow.imageLogo = null;
                //    _accWindow.UserLogoImage.ImageSource = null;
                //    File.Move($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.jpg", $"tempFileUser_{userInformation.Login}");
                //}
                //else
                //{
                //    using (FileStream fileStream = new FileStream($"../../Resource/LogoImageAccount/User{userInformation.Login}_Image.jpg", FileMode.Create))
                //    {
                //        jpegBitmapEncoder.Save(fileStream);
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
        }
    }
}
