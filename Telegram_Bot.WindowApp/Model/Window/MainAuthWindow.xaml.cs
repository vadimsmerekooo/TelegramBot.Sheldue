using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Telegram_Bot.WindowApp.Model.Window
{
    /// <summary>
    /// Логика взаимодействия для MainAuthWindow.xaml
    /// </summary>
    public partial class MainAuthWindow : System.Windows.Window
    {
        public MainAuthWindow()
        {
            InitializeComponent();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAuth_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBoxLogin.Text) && !String.IsNullOrWhiteSpace(PasswordBoxPassword.Password))
            {
                TextBoxLogin.Text.GetHashCode();
                PasswordBoxPassword.Password.GetHashCode();
                if (TextBoxLogin.Text.GetHashCode() == 980268629 && PasswordBoxPassword.Password.GetHashCode() == 1967420705)
                {
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    ErrorReg("Неверный логин или пароль!");
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(TextBoxLogin.Text))
                    BorderLoginTextBox.Background = Brushes.Red;
                if (String.IsNullOrWhiteSpace(PasswordBoxPassword.Password))
                    BorderPasswordPasswordBox.Background = Brushes.Red;
            }
        }

        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            BorderLoginTextBox.Background = Brushes.White;
        }

        private void PasswordBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            BorderPasswordPasswordBox.Background = Brushes.White;
        }
        private void ErrorReg(string textMessagePanel)
        {
            Storyboard sb = this.FindResource("InfoPanelAuthReg") as Storyboard;
            sb.Begin();
            KindIconInfoStackPanel.Kind = MaterialDesignThemes.Wpf.PackIconKind.Error;
            TextBoxInfoStackPanel.FontSize = 12;
            TextBoxInfoStackPanel.Text = textMessagePanel;
            BorderInfoPanelAuthReg.Background = Brushes.Red;
            BorderPasswordPasswordBox.Background = Brushes.Red;
            BorderLoginTextBox.Background = Brushes.Red;
        }

        private void TextBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (PasswordBoxPassword.Password == string.Empty)
                    PasswordBoxPassword.Focus();
                else
                    ButtonAuth_Click(null, null);
        }

        private void PasswordBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ButtonAuth_Click(null, null);
        }
    }
}
