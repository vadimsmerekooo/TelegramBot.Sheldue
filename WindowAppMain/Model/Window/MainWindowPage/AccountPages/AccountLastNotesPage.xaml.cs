using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Telegram_Bot.BL.Classes.App;

namespace WindowAppMain.Model.Window.MainWindowPage.AccountPages
{
    /// <summary>
    /// Логика взаимодействия для AccountLastNotesPage.xaml
    /// </summary>
    public partial class AccountLastNotesPage : Page
    {
        MainWindow _mWindow;
        public AccountLastNotesPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mWindow = mainWindow;
            NotesItemsControl.ItemsSource = new ReferenseDALClass().SetConnectionDBSelectAll(_mWindow._userInfo.ID);
        }
    }
}
