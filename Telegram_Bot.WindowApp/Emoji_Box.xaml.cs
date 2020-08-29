using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Telegram_Bot.WindowApp
{
    /// <summary>
    /// Логика взаимодействия для Emoji_Box.xaml
    /// </summary>
    public partial class Emoji_Box : UserControl
    {
        public delegate void MethodContainer(object img);

        public event MethodContainer onEmoji;

        private List<Image> images_List = new List<Image>();

        public Emoji_Box()
        {
            InitializeComponent();

            foreach (string path in Directory.GetFiles("../../Emoji"))
                images_List.Add(new Image()
                {
                    Source = new BitmapImage(new Uri(Path.GetFullPath(path), UriKind.RelativeOrAbsolute)),
                    Height = 20,
                    Width = 20,
                    Margin = new System.Windows.Thickness(2),
                    Cursor = Cursors.Hand,
                    Tag = Path.GetFileName(Path.GetFullPath(path)).Replace(".png", "")
                });
            AllEmoji.ItemsSource = images_List;
        }

        private void AllEmoji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllEmoji.SelectedIndex > 0)
            {
                onEmoji(((Image)AllEmoji.SelectedItem).Tag);
                AllEmoji.SelectedIndex = -1;
            }

        }
    }
}
