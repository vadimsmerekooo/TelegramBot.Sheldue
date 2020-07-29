using System;
using System.Windows.Controls;

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
                    if (MainWindow.TelegramBot_Working)
                        BotInfoPageFrame.NavigationService.Navigate(new StartBotPage());
                    else
                        BotInfoPageFrame.NavigationService.Navigate(new StopBotPage());
                    break;
                case 1:
                    if (!MainWindow.VkBot_Working)
                        BotInfoPageFrame.NavigationService.Navigate(new StartBotPage());
                    else
                        BotInfoPageFrame.NavigationService.Navigate(new StopBotPage());
                    break;
                case 2:
                    if (!MainWindow.ViberBot_Working)
                        BotInfoPageFrame.NavigationService.Navigate(new StartBotPage());
                    else
                        BotInfoPageFrame.NavigationService.Navigate(new StopBotPage());
                    break;
            }
        }
    }
}
