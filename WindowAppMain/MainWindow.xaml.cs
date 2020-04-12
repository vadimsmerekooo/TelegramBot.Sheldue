using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using WindowAppMain.Model.Window;
using WindowAppMain.Model.Window.MainWindowPage;

namespace WindowAppMain
{
    public partial class MainWindow : Window
    {
        #region Parametr's
        private bool StateClosed = true;
        string _name;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            HomeBox.IsSelected = true;

        }
        public MainWindow(Dictionary<string, string> userInfo) : this()
        {
            switch (userInfo.Count)
            {
                case 3:
                    UserName.Text = userInfo["Name"];
                    UserStatus.Text = userInfo["Status"];
                    break;
                case 5:
                    UserName.Text = userInfo["Login"];
                    UserStatus.Text = userInfo["Status"] + " из группы " + userInfo["Group"];
                    break;
            }
        }
        //DoubleAnimation Open and Close UserPageInfo
        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
                MainWindowName.MinWidth = 710;
            }
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
                MainWindowName.MinWidth = 500;
            }

            StateClosed = !StateClosed;
        }
        //!DoubleAnimation Open and Close UserPageInfo

        //Select Page
        private void GroupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GroupListBox.SelectedIndex)
            {
                case 0:
                    Task.Run(() => this.Dispatcher.BeginInvoke((ThreadStart)delegate () { MainWindowPage.NavigationService.Navigate(new Uri("Model/Window/MainWindowPage/HomePage.xaml", UriKind.Relative)); }));
                    break;
                case 1:
                    MainWindowPage.NavigationService.Navigate(new Uri("Model/Window/MainWindowPage/AccountInfoPage.xaml", UriKind.Relative));
                    break;
                case 2:
                    MainWindowPage.NavigationService.Navigate(new Uri("Model/Window/MainWindowPage/SettingsPage.xaml", UriKind.Relative));
                    break;
            }
        }
        //!Select Page

        //Event's TitleBar Change
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void WindowMinimized_Click(object sender, RoutedEventArgs e) { this.WindowState = WindowState.Minimized; }
        private void WindowMaxNormal_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                WindowMaxNormal.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                WindowMaxNormal.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowRestore;
            }
        }
        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void GridTitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
        }
        //!Event's TitleBar Change

        //Event's UserInfoPage
        private void CloseAccount_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter("bin/fileauth/SET_COOKIEUSER.xml"))
                    {
                        sw.WriteLine(string.Empty);
                    }
                }
                catch
                {

                }
                WindowAuthReg windowAuthReg = new WindowAuthReg(this);
                windowAuthReg.Show();
            }
            catch
            {

            }
        }
        //!Event's UserInfoPage
    }
}
