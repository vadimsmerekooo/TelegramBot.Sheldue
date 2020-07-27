using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Xml.Serialization;
using Telegram_Bot.WindowApp.Classes;
using Telegram_Bot.WindowApp.Model.Pages.BotsPages;

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
                    TextBlockTelegramName.Text = $"Пользователи Telegram бота ({((List<UserInfoList>)ListBoxUsersBots.ItemsSource).Count})";
                    break;
                case 1:
                    ListBoxUsersBots.ItemsSource = MainWindow.DeserializebleMethod("Configure_Files/VkBot_Files/Users/UsersListVk.xaml");
                    TextBlockTelegramName.Text = $"Пользователи Vk бота ({((List<UserInfoList>)ListBoxUsersBots.ItemsSource).Count})";
                    break;
                case 2:
                    ListBoxUsersBots.ItemsSource = MainWindow.DeserializebleMethod("Configure_Files/ViberBot_Files/Users/UsersListViber.xaml");
                    TextBlockTelegramName.Text = $"Пользователи Viber бота ({((List<UserInfoList>)ListBoxUsersBots.ItemsSource).Count})";
                    break;
            }
        }
    }
}
