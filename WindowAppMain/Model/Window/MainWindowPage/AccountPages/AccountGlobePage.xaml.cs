using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Telegram_Bot.BL.Classes.App;
using WindowAppMain.Classes.WindowAuthClasses;

namespace WindowAppMain.Model.Window.MainWindowPage.AccountPages
{
    /// <summary>
    /// Логика взаимодействия для AccountGlobePage.xaml
    /// </summary>
    public partial class AccountGlobePage : Page
    {
        MainWindow _mWindow;
        public AccountGlobePage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mWindow = mainWindow;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (new CheckValidateForm().IsValidatePassword(TextBlockNewPass.Password))
            {
                if (new ReferenseDALClass().GetPasswordUser(_mWindow._userInfo.Login, TextBlockOldPass.Password))
                {
                    new ReferenseDALClass().SetConnectionDBChangePassword(_mWindow._userInfo.Login, TextBlockNewPass.Password);
                    _mWindow.KindThrowMessage.Foreground = FindResource("ForegroundColorUIElements") as SolidColorBrush;
                    _mWindow.KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
                    _mWindow.TextBlockMessageThrow.Text = "Пароль успешно изменен!";
                    Storyboard sb = _mWindow.FindResource("ShowMessageThrowGrid") as Storyboard;
                    sb.Begin();
                    TextBlockOldPass.Clear();
                    TextBlockNewPass.Clear();
                }
                else
                {
                    _mWindow.KindThrowMessage.Foreground = FindResource("ErrorForegroundColorUIElements") as SolidColorBrush;
                    _mWindow.KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                    _mWindow.TextBlockMessageThrow.Text = "Введен не верный старый пароль!";
                    Storyboard sb = _mWindow.FindResource("ShowMessageThrowGrid") as Storyboard;
                    sb.Begin();
                }
            }
            else
            {
                _mWindow.KindThrowMessage.Foreground = FindResource("ErrorForegroundColorUIElements") as SolidColorBrush;
                _mWindow.KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                _mWindow.TextBlockMessageThrow.Text = "Новый пароль не соотвествует требованиям!";
                Storyboard sb = _mWindow.FindResource("ShowMessageThrowGrid") as Storyboard;
                sb.Begin();
            }
        }
    }
}
