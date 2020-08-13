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
        string bot = string.Empty;
        public StopBotPage(string bot)
        {
            InitializeComponent();
            this.bot = bot;
            Box_Emoji_UIElement.onEmoji += GetEmoji;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Emoji_BoxGrid.Visibility == Visibility.Hidden)
                Emoji_BoxGrid.Visibility = Visibility.Visible;
            else
                Emoji_BoxGrid.Visibility = Visibility.Hidden;
        }
        private void GetEmoji(object tag)
        {
            TextBoxNewMessage.Text += (string)tag;
            TextBoxNewMessage.Focus();
            TextBoxNewMessage.SelectionStart = TextBoxNewMessage.Text.Length + 1;
        }



        private ListBoxItem CreateListBoxItem_Bot(string messageText)
        {
            ListBoxItem item = new ListBoxItem();

            Image img = new Image() { Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resource/botRoma.png"), UriKind.RelativeOrAbsolute)) };
            img.Height = 50;
            img.Width = 50;
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = img.Source;

            Ellipse el = new Ellipse()
            {
                Width = 30,
                Height = 30,
                Fill = imgBrush,
                ToolTip = "Бот",
                VerticalAlignment = VerticalAlignment.Top
            };

            item.Content = el;
            Border border = new Border();
            border.CornerRadius = new CornerRadius() { TopLeft = 10, BottomLeft = 10, BottomRight = 10, TopRight = 10 };
            border.BorderThickness = new Thickness() { Bottom = 1, Left = 1, Right = 1, Top = 1 };
            border.BorderBrush = Brushes.Gray;

            TextBlock txtblock = new TextBlock();
            txtblock.Text = messageText;
            txtblock.MaxWidth = 250;
            txtblock.Margin = new Thickness(3, 5, 3, 5);
            txtblock.TextWrapping = TextWrapping.Wrap;
            txtblock.Foreground = Brushes.White;
            txtblock.VerticalAlignment = VerticalAlignment.Center;
            txtblock.Style = (Style)Application.Current.Resources["MaterialDesignBody1TextBlock"];
            border.Width = txtblock.Width + 10;
            border.Height = txtblock.Height + 10;
            Grid grid = new Grid();
            grid.Children.Add(border);
            grid.Children.Add(txtblock);
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Children.Add(item);
            panel.Children.Add(grid);
            TextBlock timeTextBlock = new TextBlock();
            if (DateTime.Now.Minute.ToString().Length == 2)
                timeTextBlock.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            else
                timeTextBlock.Text = DateTime.Now.Hour + ":0" + DateTime.Now.Minute;
            timeTextBlock.Foreground = Brushes.Gray;
            timeTextBlock.Opacity = 0.7;
            timeTextBlock.FontSize = 11;
            timeTextBlock.HorizontalAlignment = HorizontalAlignment.Right;

            StackPanel panelTime = new StackPanel();
            panelTime.Orientation = Orientation.Vertical;
            panelTime.Children.Add(panel);
            panelTime.Children.Add(timeTextBlock);

            ListBoxItem bigItem = new ListBoxItem();
            bigItem.Content = panelTime;


            return bigItem;
        }
        private ListBoxItem CreateListBoxItem_Admin(string messageText)
        {
            ListBoxItem item = new ListBoxItem();
            Image img = new Image() { Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resource/logoAccount2x2.png"), UriKind.RelativeOrAbsolute)) };
            img.Height = 50;
            img.Width = 50;
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = img.Source;

            Ellipse el = new Ellipse()
            {
                Width = 30,
                Height = 30,
                Fill = imgBrush,
                ToolTip = "Админ",
                VerticalAlignment = VerticalAlignment.Top
            };

            item.Content = el;
            Border border = new Border();
            border.CornerRadius = new CornerRadius() { TopLeft = 10, BottomLeft = 10, BottomRight = 10, TopRight = 10 };
            border.BorderThickness = new Thickness() { Bottom = 1, Left = 1, Right = 1, Top = 1 };
            border.BorderBrush = Brushes.Gray;

            TextBlock txtblock = new TextBlock();
            txtblock.Text = messageText;
            txtblock.MaxWidth = 250;
            txtblock.Margin = new Thickness(3, 5, 3, 5);
            txtblock.TextWrapping = TextWrapping.Wrap;
            txtblock.Foreground = Brushes.White;
            txtblock.VerticalAlignment = VerticalAlignment.Center;
            txtblock.Style = (Style)Application.Current.Resources["MaterialDesignBody1TextBlock"];
            border.Width = txtblock.Width + 10;
            border.Height = txtblock.Height + 10;
            Grid grid = new Grid();
            grid.Children.Add(border);
            grid.Children.Add(txtblock);

            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Children.Add(grid);
            panel.Children.Add(item);

            TextBlock timeTextBlock = new TextBlock();
            if (DateTime.Now.Minute.ToString().Length == 2)
                timeTextBlock.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            else
                timeTextBlock.Text = DateTime.Now.Hour + ":0" + DateTime.Now.Minute;
            timeTextBlock.Foreground = Brushes.Gray;
            timeTextBlock.Opacity = 0.7;
            timeTextBlock.FontSize = 11;
            timeTextBlock.HorizontalAlignment = HorizontalAlignment.Right;

            StackPanel panelTime = new StackPanel();
            panelTime.Orientation = Orientation.Vertical;
            panelTime.Children.Add(panel);
            panelTime.Children.Add(timeTextBlock);

            ListBoxItem bigItem = new ListBoxItem();
            bigItem.Content = panelTime;
            bigItem.HorizontalAlignment = HorizontalAlignment.Right;

            return bigItem;
        }

        private ListBoxItem CreateListBoxItem_BlockedUser(int idUser, string nameUser)
        {

            Grid MainGrid = new Grid();
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock tb_nameuser = new TextBlock()
            {
                Text = nameUser,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(5, 0, 5, 0),
                Foreground = Brushes.White,
                Style = (Style)Application.Current.Resources["MaterialDesignBody1TextBlock"],
            };

            MainGrid.Children.Add(tb_nameuser);
            Grid.SetColumn(tb_nameuser, 0);
            TextBox tb_iduser = new TextBox()
            {
                Text = idUser.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(5, 0, 0, 0),
                Foreground = Brushes.White,
                Background = Brushes.Transparent,
                IsReadOnly = true,
                Style = (Style)Application.Current.Resources["TextBoxTransparateStyle"],
            };

            MainGrid.Children.Add(tb_iduser);
            Grid.SetColumn(tb_iduser, 1);

            Border brd = new Border()
            {
                Background = Brushes.Gray,
                Height = 1,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            MainGrid.Children.Add(brd);
            Grid.SetColumnSpan(brd, 2);

            Border brdRight = new Border()
            {
                Background = Brushes.Gray,
                Width = 1,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            MainGrid.Children.Add(brdRight);

            return new ListBoxItem() { Content = MainGrid, HorizontalAlignment = HorizontalAlignment.Center };
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxNewMessage.Text))
                return;
            ChatListBox.Items.Add(CreateListBoxItem_Admin(TextBoxNewMessage.Text));
            TextBoxNewMessage.Clear();
            Emoji_BoxGrid.Visibility = Visibility.Hidden;
        }

        private void TextBoxNewMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click_1(null, null);
        }

        private void ButtonStartBot_Click(object sender, RoutedEventArgs e)
        {
            switch (bot)
            {
                case nameof(MainWindow.TelegramBot_Working):
                    MainWindow.bw.Dispose();
                    MainWindow.TelegramBot_Working = false;
                    MainWindow._mWindow.SetParamOnOffBot(bot, false);
                    MainWindow._mWindow.ShowErrorMessage("Бот остановлен!");
                    MainWindow._mWindow.GroupListBox.SelectedIndex = -1;
                    MainWindow._mWindow.GroupListBox.SelectedIndex = 0;
                    break;
                case nameof(MainWindow.VkBot_Working):

                    break;
                case nameof(MainWindow.ViberBot_Working):

                    break;
            }
        }
    }
}
