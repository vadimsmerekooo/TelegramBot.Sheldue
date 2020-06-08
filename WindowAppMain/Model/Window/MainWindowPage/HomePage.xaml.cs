using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Telegram_Bot.BL.Classes.App;
using WindowAppMain.Model.Controls;
using IFCore;
using System.Windows.Media;

namespace WindowAppMain.Model.Window.MainWindowPage
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private DispatcherTimer StopAnimation = new DispatcherTimer();
        private LoadingAnimation loadedControl;
        public List<SheldueAllDays> allSheldueList;
        private List<UserNotes> userNotes;
        private MainWindow _mWindow;
        private List<DateTime> listDate;
        const string teacher = "Преподаватель";
        const string student = "Студент";


        public HomePage(MainWindow mWindow)
        {
            InitializeComponent();
            _mWindow = mWindow;

            ReferenseDALClass refClassDAL = new ReferenseDALClass();
            loadedControl = new LoadingAnimation();
            MainSheldueGrid.Children.Add(loadedControl);
            loadedControl.StartAnimation();
            allSheldueList = _mWindow.GetSheldue;
            if (allSheldueList == null)
            {
                userNotes = new List<UserNotes>(refClassDAL.SetConnectionDBSelectAll(_mWindow._userInfo.ID));
                _mWindow.KindThrowMessage.Foreground = FindResource("WarningForegroundColorUIElements") as SolidColorBrush;
                _mWindow.KindThrowMessage.Kind = MaterialDesignThemes.Wpf.PackIconKind.WarningOutline;
                _mWindow.TextBlockMessageThrow.Text = "Расписание загружается! Не закрывайте программу!";
                Storyboard sb = _mWindow.FindResource("ShowMessageThrowGrid") as Storyboard;
                sb.Begin();
                _mWindow.IsEnabled = false;
                switch (_mWindow._userInfo.Status)
                {
                    case teacher:
                        Task.Run(() => this.Dispatcher.BeginInvoke((ThreadStart)delegate () { LoadSheldueAsyncMethod(false); }));
                        break;
                    case student:
                        Task.Run(() => this.Dispatcher.BeginInvoke((ThreadStart)delegate () { LoadSheldueAsyncMethod(true); }));
                        break;
                }
            }
            else
            {
                ListViewSheldueDay.ItemsSource = allSheldueList;
                StopAnimation.Tick += new EventHandler(StopMethodAnimation);
                StopAnimation.Interval = new TimeSpan(0, 0, 1);
                StopAnimation.Start();
            }
            DateTime firstDate = GetFirstDateOfWeek(DayOfWeek.Monday);
            var lastDate = GetLastDateOfWeek(DayOfWeek.Saturday);
            GetListDateOfWeek(firstDate, lastDate);
            refClassDAL.SetConnectionDBDeleteNote(listDate[0]);
        }

        private async void LoadSheldueAsyncMethod(bool student)
        {
            if (student)
                await Task.Run(() => GetSheldueStudent());
            else
                await Task.Run(() => GetSheldueTeacher());
            await Task.Run(() => SetSheldueList());
            StopAnimation.Tick += new EventHandler(StopMethodAnimation);
            StopAnimation.Interval = new TimeSpan(0, 0, 1);
            StopAnimation.Start();
            _mWindow.IsEnabled = true;
        }

        private void StopMethodAnimation(object sender, EventArgs e)
        {
            Storyboard sb = this.FindResource("ShowPage") as Storyboard;
            sb.Begin();
            loadedControl.StopAnimation();
            ScrollViewerSheldue.Opacity = 1;
            StopAnimation.Stop();
        }

        private void SetSheldueList()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate
           {
               ListViewSheldueDay.ItemsSource = allSheldueList;
           });
            _mWindow.SetSheldue = allSheldueList;
        }

        private void GetSheldueStudent()
        {
            allSheldueList = new ReferenseDALClass().SetConnectionDBGetSheldue(_mWindow._userInfo.Department, _mWindow._userInfo.Group, userNotes, listDate);
        }


        private void GetSheldueTeacher()
        {
            allSheldueList = new ReferenseDALClass().SetConnectionDBGetSheldue(_mWindow._userInfo.Name, userNotes, listDate);
        }
        private DateTime GetFirstDateOfWeek(DayOfWeek firstDay)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                DateTime dayInWeek = DateTime.Now.AddDays(1);
                DateTime firstDayInWeek = dayInWeek.Date;
                while (firstDayInWeek.DayOfWeek != firstDay)
                    firstDayInWeek = firstDayInWeek.AddDays(-1);

                return firstDayInWeek;
            }
            else
            {
                DateTime dayInWeek = DateTime.Now;
                DateTime firstDayInWeek = dayInWeek.Date;
                while (firstDayInWeek.DayOfWeek != firstDay)
                    firstDayInWeek = firstDayInWeek.AddDays(-1);
                return firstDayInWeek;
            }
        }
        private DateTime GetLastDateOfWeek(DayOfWeek firstDay)
        {
            DateTime dayInWeek = DateTime.Now;
            DateTime lastDayInWeek = dayInWeek.Date;
            while (lastDayInWeek.DayOfWeek != firstDay)
                lastDayInWeek = lastDayInWeek.AddDays(1);

            return lastDayInWeek;
        }
        private void GetListDateOfWeek(DateTime firstDay, DateTime lastDay)
        {
            listDate = new List<DateTime>();
            do
            {
                listDate.Add(firstDay);
                firstDay = firstDay.AddDays(1);
            } while (firstDay != lastDay.AddDays(1));
        }

        private void ButtonNotes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Button buutonClick = ((Button)e.OriginalSource);
                string tag = buutonClick.Tag.ToString();
                switch (tag)
                {
                    case "Day1Para1":
                        ShowModalWindow(allSheldueList[0].DayName,
                                        allSheldueList[0].Day[0].Para1[0].Work,
                                        int.Parse(allSheldueList[0].Day[0].Para1[0].Para),
                                        allSheldueList[0].Day[0].Para1[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day1Para2":
                        ShowModalWindow(allSheldueList[0].DayName,
                                        allSheldueList[0].Day[0].Para2[0].Work,
                                        int.Parse(allSheldueList[0].Day[0].Para2[0].Para),
                                        allSheldueList[0].Day[0].Para2[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day1Para3":
                        ShowModalWindow(allSheldueList[0].DayName,
                                        allSheldueList[0].Day[0].Para3[0].Work,
                                        int.Parse(allSheldueList[0].Day[0].Para3[0].Para),
                                        allSheldueList[0].Day[0].Para3[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day1Para4":
                        ShowModalWindow(allSheldueList[0].DayName,
                                        allSheldueList[0].Day[0].Para4[0].Work,
                                        int.Parse(allSheldueList[0].Day[0].Para4[0].Para),
                                        allSheldueList[0].Day[0].Para4[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day1Para5":
                        ShowModalWindow(allSheldueList[0].DayName,
                                        allSheldueList[0].Day[0].Para5[0].Work,
                                        int.Parse(allSheldueList[0].Day[0].Para4[0].Para),
                                        allSheldueList[0].Day[0].Para4[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day2Para1":
                        ShowModalWindow(allSheldueList[1].DayName,
                                        allSheldueList[1].Day[0].Para1[0].Work,
                                        int.Parse(allSheldueList[1].Day[0].Para1[0].Para),
                                        allSheldueList[1].Day[0].Para1[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day2Para2":
                        ShowModalWindow(allSheldueList[1].DayName,
                                        allSheldueList[1].Day[0].Para2[0].Work,
                                        int.Parse(allSheldueList[1].Day[0].Para2[0].Para),
                                        allSheldueList[1].Day[0].Para2[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day2Para3":
                        ShowModalWindow(allSheldueList[1].DayName,
                                        allSheldueList[1].Day[0].Para3[0].Work,
                                        int.Parse(allSheldueList[1].Day[0].Para3[0].Para),
                                        allSheldueList[1].Day[0].Para3[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day2Para4":
                        ShowModalWindow(allSheldueList[1].DayName,
                                        allSheldueList[1].Day[0].Para4[0].Work,
                                        int.Parse(allSheldueList[1].Day[0].Para4[0].Para),
                                        allSheldueList[1].Day[0].Para4[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day2Para5":
                        ShowModalWindow(allSheldueList[1].DayName,
                                        allSheldueList[1].Day[0].Para5[0].Work,
                                        int.Parse(allSheldueList[1].Day[0].Para5[0].Para),
                                        allSheldueList[1].Day[0].Para5[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day3Para1":
                        ShowModalWindow(allSheldueList[2].DayName,
                                        allSheldueList[2].Day[0].Para1[0].Work,
                                        int.Parse(allSheldueList[2].Day[0].Para1[0].Para),
                                        allSheldueList[2].Day[0].Para1[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day3Para2":
                        ShowModalWindow(allSheldueList[2].DayName,
                                        allSheldueList[2].Day[0].Para2[0].Work,
                                        int.Parse(allSheldueList[2].Day[0].Para2[0].Para),
                                        allSheldueList[2].Day[0].Para2[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day3Para3":
                        ShowModalWindow(allSheldueList[2].DayName,
                                        allSheldueList[2].Day[0].Para3[0].Work,
                                        int.Parse(allSheldueList[2].Day[0].Para3[0].Para),
                                        allSheldueList[2].Day[0].Para3[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day3Para4":
                        ShowModalWindow(allSheldueList[2].DayName,
                                        allSheldueList[2].Day[0].Para4[0].Work,
                                        int.Parse(allSheldueList[2].Day[0].Para4[0].Para),
                                        allSheldueList[2].Day[0].Para4[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day3Para5":
                        ShowModalWindow(allSheldueList[2].DayName,
                                        allSheldueList[2].Day[0].Para5[0].Work,
                                        int.Parse(allSheldueList[2].Day[0].Para5[0].Para),
                                        allSheldueList[2].Day[0].Para5[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day4Para1":
                        ShowModalWindow(allSheldueList[3].DayName,
                                        allSheldueList[3].Day[0].Para1[0].Work,
                                        int.Parse(allSheldueList[3].Day[0].Para1[0].Para),
                                        allSheldueList[3].Day[0].Para1[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day4Para2":
                        ShowModalWindow(allSheldueList[3].DayName,
                                        allSheldueList[3].Day[0].Para2[0].Work,
                                        int.Parse(allSheldueList[3].Day[0].Para2[0].Para),
                                        allSheldueList[3].Day[0].Para2[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day4Para3":
                        ShowModalWindow(allSheldueList[3].DayName,
                                        allSheldueList[3].Day[0].Para3[0].Work,
                                        int.Parse(allSheldueList[3].Day[0].Para3[0].Para),
                                        allSheldueList[3].Day[0].Para3[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day4Para4":
                        ShowModalWindow(allSheldueList[3].DayName,
                                        allSheldueList[3].Day[0].Para4[0].Work,
                                        int.Parse(allSheldueList[3].Day[0].Para4[0].Para),
                                        allSheldueList[3].Day[0].Para4[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day4Para5":
                        ShowModalWindow(allSheldueList[3].DayName,
                                        allSheldueList[3].Day[0].Para5[0].Work,
                                        int.Parse(allSheldueList[3].Day[0].Para5[0].Para),
                                        allSheldueList[3].Day[0].Para5[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day5Para1":
                        ShowModalWindow(allSheldueList[4].DayName,
                                        allSheldueList[4].Day[0].Para1[0].Work,
                                        int.Parse(allSheldueList[4].Day[0].Para1[0].Para),
                                        allSheldueList[4].Day[0].Para1[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day5Para2":
                        ShowModalWindow(allSheldueList[4].DayName,
                                        allSheldueList[4].Day[0].Para2[0].Work,
                                        int.Parse(allSheldueList[4].Day[0].Para2[0].Para),
                                        allSheldueList[4].Day[0].Para2[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day5Para3":
                        ShowModalWindow(allSheldueList[4].DayName,
                                        allSheldueList[4].Day[0].Para3[0].Work,
                                        int.Parse(allSheldueList[4].Day[0].Para3[0].Para),
                                        allSheldueList[4].Day[0].Para3[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day5Para4":
                        ShowModalWindow(allSheldueList[4].DayName,
                                        allSheldueList[4].Day[0].Para4[0].Work,
                                        int.Parse(allSheldueList[4].Day[0].Para4[0].Para),
                                        allSheldueList[4].Day[0].Para4[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day5Para5":
                        ShowModalWindow(allSheldueList[4].DayName,
                                        allSheldueList[4].Day[0].Para5[0].Work,
                                        int.Parse(allSheldueList[4].Day[0].Para5[0].Para),
                                        allSheldueList[4].Day[0].Para5[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day6Para1":
                        ShowModalWindow(allSheldueList[5].DayName,
                                        allSheldueList[5].Day[0].Para1[0].Work,
                                        int.Parse(allSheldueList[5].Day[0].Para1[0].Para),
                                        allSheldueList[5].Day[0].Para1[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day6Para2":
                        ShowModalWindow(allSheldueList[5].DayName,
                                        allSheldueList[5].Day[0].Para2[0].Work,
                                        int.Parse(allSheldueList[5].Day[0].Para2[0].Para),
                                        allSheldueList[5].Day[0].Para2[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day6Para3":
                        ShowModalWindow(allSheldueList[5].DayName,
                                        allSheldueList[5].Day[0].Para3[0].Work,
                                        int.Parse(allSheldueList[5].Day[0].Para3[0].Para),
                                        allSheldueList[5].Day[0].Para3[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day6Para4":
                        ShowModalWindow(allSheldueList[5].DayName,
                                        allSheldueList[5].Day[0].Para4[0].Work,
                                        int.Parse(allSheldueList[5].Day[0].Para4[0].Para),
                                        allSheldueList[5].Day[0].Para4[0].userNotes,
                                        ref buutonClick);
                        break;
                    case "Day6Para5":
                        ShowModalWindow(allSheldueList[5].DayName,
                                        allSheldueList[5].Day[0].Para5[0].Work,
                                        int.Parse(allSheldueList[5].Day[0].Para5[0].Para),
                                        allSheldueList[5].Day[0].Para5[0].userNotes,
                                        ref buutonClick);
                        break;
                }
            }
            catch
            {

            }
        }
        public UserNotes notes;
        private void ShowModalWindow(string dateDay, string para, int paraNumber, UserNotes note, ref Button butonClick)
        {
            switch ((butonClick.Content as MaterialDesignThemes.Wpf.PackIcon).Kind)
            {
                case MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline:
                    _mWindow._homePage = this;
                    _mWindow._buttonRefAddNote = butonClick;
                    _mWindow.GridModalWindows.Visibility = System.Windows.Visibility.Visible;
                    _mWindow.ModalWindowAddNotes.Visibility = System.Windows.Visibility.Visible;
                    _mWindow.DosentOpacityGrid.IsEnabled = false;
                    _mWindow.NameGridChangeOrAddNotes.Text = "Добавление заметки";
                    _mWindow.SaveNoteButton.Content = "Сохранить";
                    _mWindow.SaveNoteButton.ToolTip = "Сохранить заметку";
                    _mWindow.KindHeaderTextBock.Kind = MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline;
                    _mWindow.SaveNoteButton.Visibility = System.Windows.Visibility.Hidden;
                    _mWindow.DateNoteTextBlock.Text = dateDay;
                    _mWindow.ParaNoteTextBlock.Text = para;
                    _mWindow.paraNumber = paraNumber;
                    Storyboard sb = _mWindow.FindResource("ShowModalWindowAddNewNote") as Storyboard;
                    sb.Begin();
                    _mWindow.NewNoteTextBox.Clear();
                    notes = note;
                    break;
                case MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline:
                    notes = note;
                    _mWindow._homePage = this;
                    _mWindow._buttonRefAddNote = butonClick;
                    _mWindow.GridModalWindows.Visibility = System.Windows.Visibility.Visible;
                    _mWindow.ModalWindowAddNotes.Visibility = System.Windows.Visibility.Visible;
                    _mWindow.DosentOpacityGrid.IsEnabled = false;
                    _mWindow.NameGridChangeOrAddNotes.Text = "Изменение заметки";
                    _mWindow.SaveNoteButton.Content = "Изменить";
                    _mWindow.SaveNoteButton.ToolTip = "Изменить заметку";
                    _mWindow.KindHeaderTextBock.Kind = MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline;
                    _mWindow.SaveNoteButton.Visibility = System.Windows.Visibility.Visible;
                    _mWindow.DateNoteTextBlock.Text = dateDay;
                    _mWindow.ParaNoteTextBlock.Text = para;
                    _mWindow.paraNumber = paraNumber;
                    _mWindow.NewNoteTextBox.Text = note.NoteText;
                    _mWindow.tmpNoteUserId = note.IDNotes;
                    Storyboard sbChangeNote = _mWindow.FindResource("ShowModalWindowAddNewNote") as Storyboard;
                    sbChangeNote.Begin();
                    break;
            }
        }
    }
}
