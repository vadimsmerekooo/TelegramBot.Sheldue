using IFCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Telegram_Bot.WindowApp.Classes;
using Telegram_Bot.WindowApp.Model.Pages;

namespace Telegram_Bot.WindowApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static MainWindow _mWindow;

        public MainWindow()
        {
            InitializeComponent();
            _mWindow = this;
        }

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
            try
            {
                DragMove();
            }
            catch
            {

            }
        }
        private void GridTitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
        }

        private void GroupListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (GroupListBox.SelectedIndex)
            {
                case 0:
                    MainWindowPage.NavigationService.Navigate(new StartupBotPage());
                    break;
                case 1:
                    MainWindowPage.NavigationService.Navigate(new LogsPage());
                    break;
                case 2:
                    MainWindowPage.NavigationService.Navigate(new UsersBotsPage());
                    break;
            }
        }
        

        public static void SerializebleMethod(string pathToFile, List<UserInfoList> listUsers)
        {
            try
            {
                using (FileStream fs = new FileStream(pathToFile, FileMode.Open))
                {
                    new XmlSerializer(typeof(UserInfoList), new XmlRootAttribute() { ElementName = "UserInfoList" }).Serialize(fs, listUsers);
                }
            }
            catch
            {
                IFCore.IFCore.loggerMain.Error("Ошибка доступа к файлу: " + pathToFile);
            }
        }
        public static List<UserInfoList> DeserializebleMethod(string pathToFile)
        {
            try
            {
                using (FileStream fs = new FileStream(pathToFile, FileMode.Open))
                {
                    return (List<UserInfoList>)(new XmlSerializer(typeof(UserInfoList), new XmlRootAttribute() { ElementName = "UserInfoList" })).Deserialize(fs);
                }
            }
            catch
            {
                IFCore.IFCore.loggerMain.Error("Ошибка доступа к файлу: " + pathToFile);
                return null;
            }
        }
    }
}
