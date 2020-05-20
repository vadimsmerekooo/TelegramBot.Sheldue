using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WindowAppMain.Model.Window.MainWindowPage.AccountPages;

namespace WindowAppMain.Model.Window.MainWindowPage
{
    /// <summary>
    /// Логика взаимодействия для AccountInfoPage.xaml
    /// </summary>
    public partial class AccountInfoPage : Page
    {
        private MainWindow _mWindow;
        public AccountInfoPage(MainWindow mWindow)
        {
            InitializeComponent();
            _mWindow = mWindow;
            UserNameAccountPage.Text = mWindow.UserName.Text;
            UserLogoImage.ImageSource = mWindow.imageLogo;
            AccountBox.IsSelected = true;
        }

        private void AccountListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (AccountListBox.SelectedIndex)
            {
                case 0:
                    AccountInfoPageFrame.NavigationService.Navigate(new AccountMainInfo(_mWindow, this));
                    break;
                case 1:
                    AccountInfoPageFrame.NavigationService.Navigate(new Uri("Model/Window/MainWindowPage/AccountPages/AccountGlobePage.xaml", UriKind.Relative));
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
    }
}
