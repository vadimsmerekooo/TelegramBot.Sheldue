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
                    AccountInfoPageFrame.NavigationService.Navigate(new AccountGlobePage(_mWindow));
                    break;
                case 2:
                    AccountInfoPageFrame.NavigationService.Navigate(new AccountLastNotesPage(_mWindow));
                    break;
            }
        }
    }
}
