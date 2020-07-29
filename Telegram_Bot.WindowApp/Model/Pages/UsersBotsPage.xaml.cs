using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Xml.Serialization;
using Telegram_Bot.WindowApp.Classes;

namespace Telegram_Bot.WindowApp.Model.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersBotsPage.xaml
    /// </summary>
    public partial class UsersBotsPage : Page
    {
        public UsersBotsPage()
        {
            InitializeComponent();
            BotListBox.SelectedIndex = 0;
        }
        private void BotListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (BotListBox.SelectedIndex)
            {
                case 0:
                    ListBoxUsersBots.ItemsSource = MainWindow.DeserializebleMethod("Configure_Files/TelegramBot_Files/Users/UsersListTelegram.xaml");
                    BotListBox_EnterData("Telegram");
                    break;
                case 1:
                    ListBoxUsersBots.ItemsSource = MainWindow.DeserializebleMethod("Configure_Files/VkBot_Files/Users/UsersListVk.xaml");
                    BotListBox_EnterData("Vk");
                    break;
                case 2:
                    ListBoxUsersBots.ItemsSource = MainWindow.DeserializebleMethod("Configure_Files/ViberBot_Files/Users/UsersListViber.xaml");
                    BotListBox_EnterData("Viber");
                    break;
            }
        }
        private void BotListBox_EnterData(string botText)
        {
            if (ListBoxUsersBots.ItemsSource != null && ((List<UserInfoList>)ListBoxUsersBots.ItemsSource).Count != 0)
                TextBlockTelegramName.Text = $"Пользователи {botText} бота ({((List<UserInfoList>)ListBoxUsersBots.ItemsSource).Count})";
            else
            {
                ClearList.Visibility = System.Windows.Visibility.Visible;
                TextBlockTelegramName.Text = $"Пользователи {botText} бота (список пуст)";
            }
        }
    }
}
