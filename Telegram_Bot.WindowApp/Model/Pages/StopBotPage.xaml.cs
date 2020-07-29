using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Telegram_Bot.WindowApp.Model.Pages
{
    /// <summary>
    /// Логика взаимодействия для StopBotPage.xaml
    /// </summary>
    public partial class StopBotPage : Page
    {
        public StopBotPage()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            IdSendMessageRadioButton.IsChecked = false;
            BorderTextBoxIdUser.Visibility = Visibility.Hidden;
            TextBoxStackPanel.Height = 0;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            AllSendMessageRadioButton.IsChecked = false;
            BorderTextBoxIdUser.Visibility = Visibility.Visible;
            TextBoxStackPanel.Height = 25;
        }
    }
}
