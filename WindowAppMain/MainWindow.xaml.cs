using IFCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Telegram_Bot.BL.Classes.App;
using WindowAppMain.Classes;
using WindowAppMain.Model.Window;
using WindowAppMain.Model.Window.MainWindowPage;

namespace WindowAppMain
{
    public partial class MainWindow : Window
    {
        #region Parametr's
        private bool StateClosed = true;
        public Person _userInfo = new Person();
        public BitmapImage imageLogo;
        public const string teacher = "Преподаватель";
        public const string student = "Студент";
        public int paraNumber;
        public Button _buttonRefAddNote;
        public HomePage _homePage;
        public int tmpNoteUserId;
        private List<SheldueAllDays> sheldueList { get; set;}
        public List<SheldueAllDays> SetSheldue { set { sheldueList = value; } }
        public List<SheldueAllDays> GetSheldue { get { return sheldueList; } }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(Person userInfo) : this()
        {
            // Init Information Of User
            _userInfo = userInfo;
            // Load User Avatar
            LoadImageLogoUser loadImage = new LoadImageLogoUser();
            imageLogo = loadImage.SelectImage(_userInfo.Login);
            ImageLogo.ImageSource = loadImage.SelectImage(_userInfo.Login);
            switch (_userInfo.Status)
            {
                case teacher:
                    UserName.Text = _userInfo.Name;
                    UserStatus.Text = _userInfo.Status;
                    break;
                case student:
                    UserName.Text = _userInfo.Login;
                    UserStatus.Text = _userInfo.Status + " из группы " + _userInfo.Group;
                    break;
            }
            HomeBox.IsSelected = true;
        }
        //DoubleAnimation Open and Close UserPageInfo
        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
                MainWindowName.MinWidth = 710;
            }
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
                MainWindowName.MinWidth = 500;
            }

            StateClosed = !StateClosed;
        }
        //!DoubleAnimation Open and Close UserPageInfo

        //Select Page
        private void GroupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GroupListBox.SelectedIndex)
            {
                case 0:
                    HomePage hmPage = new HomePage(this);
                    Task.Run(() => this.Dispatcher.BeginInvoke((ThreadStart)delegate () { MainWindowPage.NavigationService.Navigate(hmPage); }));
                    break;
                case 1:
                    //MainWindowPage.NavigationService.Navigate(new Uri("Model/Window/MainWindowPage/AccountInfoPage.xaml", UriKind.Relative));
                    MainWindowPage.NavigationService.Navigate(new AccountInfoPage(this));
                    break;
                case 2:
                    MainWindowPage.NavigationService.Navigate(new Uri("Model/Window/MainWindowPage/SettingsPage.xaml", UriKind.Relative));
                    break;
            }
        }
        //!Select Page

        //Event's TitleBar Change
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
        //!Event's TitleBar Change

        //Event's UserInfoPage
        private void CloseAccount_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter("SET_COOKIEUSER.xml"))
                    {
                        sw.WriteLine(string.Empty);
                    }
                }
                catch
                {

                }
                new WindowAuthReg(this).Show();
            }
            catch
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("CloseModalWindowAddNewNote") as Storyboard;
            sb.Completed += Sb_Completed;
            sb.Begin();
        }

        private void Sb_Completed(object sender, EventArgs e)
        {
            GridModalWindows.Visibility = Visibility.Hidden;
            ModalWindowAddNotes.Visibility = Visibility.Hidden;
            DosentOpacityGrid.IsEnabled = true;
        }

        private void SaveNoteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (NameGridChangeOrAddNotes.Text)
                {
                    case "Добавление заметки":
                        try
                        {
                            string[] parseDate = DateNoteTextBlock.Text.Split(' ');
                            if (new ReferenseDALClass().SetConnectionDBNoteAdd(_userInfo.ID, NewNoteTextBox.Text, Convert.ToDateTime(parseDate[1]), ParaNoteTextBlock.Text, paraNumber))
                            {
                                Storyboard sb = FindResource("CloseModalWindowAddNewNote") as Storyboard;
                                sb.Begin();
                                MaterialDesignThemes.Wpf.PackIcon kindNoteSave = new MaterialDesignThemes.Wpf.PackIcon();
                                kindNoteSave.Kind = MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline;
                                kindNoteSave.Width = 20;
                                kindNoteSave.Height = 20;
                                _buttonRefAddNote.Content = kindNoteSave;
                                _buttonRefAddNote.ToolTip = "Изменить заметку";
                                KindThrowMessage.Foreground = FindResource("ForegroundColorUIElements") as SolidColorBrush;
                                KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
                                TextBlockMessageThrow.Text = "Заметка сохранена!";
                                Storyboard sbShowNodalWindow = this.FindResource("ShowMessageThrowGrid") as Storyboard;
                                sbShowNodalWindow.Begin();
                                _homePage.notes.NoteText = NewNoteTextBox.Text;
                            }
                            else
                            {
                                KindThrowMessage.Foreground = FindResource("ErrorForegroundColorUIElements") as SolidColorBrush;
                                KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                                TextBlockMessageThrow.Text = "Заметка не добавлена!";
                                Storyboard sb = this.FindResource("ShowMessageThrowGrid") as Storyboard;
                                sb.Begin();
                            }
                            DosentOpacityGrid.IsEnabled = true;
                        }
                        catch
                        {
                            Storyboard sb = FindResource("CloseModalWindowAddNewNote") as Storyboard;
                            sb.Begin();
                            KindThrowMessage.Foreground = FindResource("ErrorForegroundColorUIElements") as SolidColorBrush;
                            KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                            TextBlockMessageThrow.Text = "Заметка не добавлена!";
                            Storyboard sbThrowMessge = FindResource("ShowMessageThrowGrid") as Storyboard;
                            sb.Begin();
                            DosentOpacityGrid.IsEnabled = true;
                        }
                        break;
                    case "Изменение заметки":
                        try
                        {
                            if (!String.IsNullOrWhiteSpace(NewNoteTextBox.Text))
                            {
                                if (new ReferenseDALClass().SetConnectionDBNoteClass(tmpNoteUserId, NewNoteTextBox.Text))
                                {
                                    KindThrowMessage.Foreground = FindResource("ForegroundColorUIElements") as SolidColorBrush;
                                    KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
                                    TextBlockMessageThrow.Text = "Заметка изменена!";
                                    Storyboard sb = FindResource("CloseModalWindowAddNewNote") as Storyboard;
                                    sb.Begin();
                                    Storyboard sbShowNodalWindow = this.FindResource("ShowMessageThrowGrid") as Storyboard;
                                    sbShowNodalWindow.Begin();
                                    _homePage.notes.NoteText = NewNoteTextBox.Text;
                                    _homePage.ListViewSheldueDay.ItemsSource = _homePage.allSheldueList;
                                }
                                else
                                {
                                    KindThrowMessage.Foreground = FindResource("ErrorForegroundColorUIElements") as SolidColorBrush;
                                    KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                                    TextBlockMessageThrow.Text = "Заметка не изменена!";
                                    Storyboard sb = FindResource("CloseModalWindowAddNewNote") as Storyboard;
                                    sb.Begin();
                                    Storyboard sbThrowMessge = this.FindResource("ShowMessageThrowGrid") as Storyboard;
                                    sbThrowMessge.Begin();
                                }
                                DosentOpacityGrid.IsEnabled = true;
                            }
                        }
                        catch 
                        {
                            KindThrowMessage.Foreground = FindResource("ErrorForegroundColorUIElements") as SolidColorBrush;
                            KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                            TextBlockMessageThrow.Text = "Заметка не изменена!";
                            Storyboard sb = FindResource("CloseModalWindowAddNewNote") as Storyboard;
                            sb.Begin();
                            Storyboard sbThrowMessge = this.FindResource("ShowMessageThrowGrid") as Storyboard;
                            sbThrowMessge.Begin();
                            DosentOpacityGrid.IsEnabled = true;
                        }
                        break;
                }
            }
            catch
            {

            }
        }

        private void NewNoteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CounterLengthTextBox.Text = $"{NewNoteTextBox.Text.Length}/100";
            if (NameGridChangeOrAddNotes.Text != "Изменение заметки")
            {
                if (NewNoteTextBox.Text.Length > 5)
                    SaveNoteButton.Visibility = Visibility.Visible;
                else
                    SaveNoteButton.Visibility = Visibility.Hidden;
            }
        }
        //!Event's UserInfoPage
    }
}
