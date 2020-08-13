using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Telegram_Bot.WindowApp.Model.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartupBotPage.xaml
    /// </summary>
    public partial class StartupBotPage : Page
    {
        public StartupBotPage()
        {
            InitializeComponent();
            BotListBox.SelectedIndex = 0;
        }

        private void BotListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (BotListBox.SelectedIndex)
            {
                case 0:
                    if (!MainWindow.TelegramBot_Working)
                        BotInfoPageFrame.NavigationService.Navigate(new StartBotPage(nameof(MainWindow.TelegramBot_Working)));
                    else
                        BotInfoPageFrame.NavigationService.Navigate(new StopBotPage(nameof(MainWindow.TelegramBot_Working)));
                    break;
                case 1:
                    if (!MainWindow.VkBot_Working)
                        BotInfoPageFrame.NavigationService.Navigate(new StartBotPage(nameof(MainWindow.VkBot_Working)));
                    else
                        BotInfoPageFrame.NavigationService.Navigate(new StopBotPage(nameof(MainWindow.VkBot_Working)));
                    break;
                case 2:
                    if (!MainWindow.ViberBot_Working)
                        BotInfoPageFrame.NavigationService.Navigate(new StartBotPage(nameof(MainWindow.ViberBot_Working)));
                    else
                        BotInfoPageFrame.NavigationService.Navigate(new StopBotPage(nameof(MainWindow.ViberBot_Working)));
                    break;
            }
        }


    }
}
