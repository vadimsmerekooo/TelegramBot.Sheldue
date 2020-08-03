﻿using System;
using System.Windows.Controls;

namespace Telegram_Bot.WindowApp.Model.Pages
{
    /// <summary>
    /// Логика взаимодействия для LogsPage.xaml
    /// </summary>
    public partial class LogsPage : Page
    {
        public LogsPage()
        {
            InitializeComponent();
            BotListBox.SelectedIndex = 0;
        }
        private void BotListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (BotListBox.SelectedIndex)
            {
                case 0:
                    BotInfoPageFrame.NavigationService.Navigate(null);
                    break;
                case 1:
                    BotInfoPageFrame.NavigationService.Navigate(null);
                    break;
                case 2:
                    BotInfoPageFrame.NavigationService.Navigate(null);
                    break;
            }
        }
    }
}