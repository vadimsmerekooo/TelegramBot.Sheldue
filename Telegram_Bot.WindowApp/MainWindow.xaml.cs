using IFCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
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


        public static bool TelegramBot_Working;
        public static bool VkBot_Working;
        public static bool ViberBot_Working;


        public static BackgroundWorker bw = new BackgroundWorker();

        public static Dictionary<string, List<SheldueAllDaysTelegram>> allSheldue;
        public static Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> changeSheldue;
        public static Dictionary<string, List<SheldueAllDaysTelegram>> allSheldueCopy;
        public static string weekCheck = string.Empty;



        public static List<int> idMessageClients;
        public static List<int> idMessageClientsBlackList;
        public static List<IFCore.DictionaryList> idMessageClientsWarningList;

        public static DispatcherTimer timerChangesSheldue = new DispatcherTimer();
        public static string dayOldSheldue = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

        public MainWindow()
        {
            InitializeComponent();
            _mWindow = this;
            GroupListBox.SelectedIndex = 0;

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

        private void GridTitleBar_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }


        public void ShowErrorMessage(string text_Message)
        {
            KindThrowMessage.Foreground = FindResource("ErrorForegroundColorUIElements") as SolidColorBrush;
            KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
            TextBlockMessageThrow.Text = text_Message;
            Storyboard sbThrowMessge = this.FindResource("ShowMessageThrowGrid") as Storyboard;
            sbThrowMessge.Begin();
        }

        public void ShowSuccessfulMessage(string text_Message)
        {
            KindThrowMessage.Foreground = FindResource("ForegroundColorUIElements") as SolidColorBrush;
            KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
            TextBlockMessageThrow.Text = text_Message;
            Storyboard sbThrowMessge = this.FindResource("ShowMessageThrowGrid") as Storyboard;
            sbThrowMessge.Begin();
        }

        public static void SetParam(string bot, bool on_off)
        {
            _mWindow.SetParamOnOffBot(bot, on_off);
        }

        public void SetParamOnOffBot(string bot, bool on_off)
        {
            switch (bot)
            {
                case nameof(MainWindow.TelegramBot_Working):
                    if (on_off)
                    {
                        TelegramBot_Working = true;
                        TelegramBotStatus.Style = FindResource("EllipseStyleOn") as Style;
                    }
                    else
                    {
                        TelegramBot_Working = false;
                        TelegramBotStatus.Style = FindResource("EllipseStyleOff") as Style;
                    }
                    break;
                case nameof(MainWindow.VkBot_Working):
                    if (on_off)
                    {
                        VkBot_Working = true;
                        VKBotStatus.Style = FindResource("EllipseStyleOn") as Style;
                    }
                    else
                    {
                        VkBot_Working = false;
                        VKBotStatus.Style = FindResource("EllipseStyleOff") as Style;
                    }
                    break;
                case nameof(MainWindow.ViberBot_Working):
                    if (on_off)
                    {
                        ViberBot_Working = true;
                        ViberBotStatus.Style = FindResource("EllipseStyleOn") as Style;
                    }
                    else
                    {
                        ViberBot_Working = false;
                        ViberBotStatus.Style = FindResource("EllipseStyleOff") as Style;
                    }
                    break;
            }
        }
    }
}
