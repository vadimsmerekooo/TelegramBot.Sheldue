using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WindowAppMain.Model.Controls;
using WindowAppMain.Model.DataBaseEF;
using WindowAppMain.Model.DataBaseEF.DBManagerbot;

namespace WindowAppMain.Model.Window.MainWindowPage
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private DispatcherTimer StopAnimation = new DispatcherTimer();
        private LoadingAnimation loadedControl;
        private List<SheldueAllDays> allSheldueList;
        private List<UsersNotes> userNotes;
        private MainWindow _mWindow;
        private List<DateTime> listDate;


        private const System.Windows.Visibility visibleNotes = System.Windows.Visibility.Visible;
        private const MaterialDesignThemes.Wpf.PackIconKind NoteAddOutline = MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline;
        private const MaterialDesignThemes.Wpf.PackIconKind NoteMultipleOutline = MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline;

        public HomePage(MainWindow mWindow)
        {
            InitializeComponent();
            _mWindow = mWindow;
            loadedControl = new LoadingAnimation();
            MainSheldueGrid.Children.Add(loadedControl);
            loadedControl.StartAnimation();
            NotesClass userNoteCl = new NotesClass();
            userNotes = new List<UsersNotes>(userNoteCl.GetAllNotesUser(_mWindow._userInfo.ID));
            Task.Run(() => this.Dispatcher.BeginInvoke((ThreadStart)delegate () { LoadAsyncMethod(); }));
            DateTime firstDate = GetFirstDateOfWeek(DayOfWeek.Monday);
            var lastDate = GetLastDateOfWeek(DayOfWeek.Saturday);
            GetListDateOfWeek(firstDate, lastDate);
        }

        private async void LoadAsyncMethod()
        {
            await Task.Run(() => GetSheldue());
            await Task.Run(() => SetSheldueList());
            StopAnimation.Tick += new EventHandler(StopMethodAnimation);
            StopAnimation.Interval = new TimeSpan(0, 0, 1);
            StopAnimation.Start();
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
        }

        private void GetSheldue()
        {
            allSheldueList = new List<SheldueAllDays>()
            {
                //Monday
                new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[0].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[0].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[0].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[0].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[0].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        })
                  }, $"Понедельник {listDate[0].Date.ToShortDateString()}"),
                  //Tuesday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[1].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[1].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[1].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        })
                  }, $"Вторник {listDate[1].Date.ToShortDateString()}"),
                  //Wednesday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[2].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[2].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        })
                  }, $"Среда {listDate[2].Date.ToShortDateString()}"),
                  //Thursday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[3].Date,"Day5Para3","1","Четверг", "Prepod3", "3Kab")
                        })
                  }, $"Четверг {listDate[3].Date.ToShortDateString()}"),
                  //Friday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[4].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[4].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[4].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[4].Date,"Day5Para3","3","para3", "Prepod3", "3Kab")
                        })
                  }, $"Пятница {listDate[4].Date.ToShortDateString()}"),
                  //Saturday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[5].Date,"Day6Para1","1","Суббота", "Пуся", "23")
                        },
                        new List<Sheldue>()
                        {
                            GetShelduePara(listDate[5].Date,"Day6Para2","2","para2", "Prepod2", "2Kab")
                        })
                  }, $"Суббота {listDate[5].Date.ToShortDateString()}"
            )};
        }

        private Sheldue GetShelduePara(DateTime dateDay, string tagNoteButton, string para, string work, string teacher, string auditoria)
        {
            MaterialDesignThemes.Wpf.PackIconKind kind = MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline;
            UsersNotes note = GetUserNote(dateDay, para, work);
            if (note.Para != null)
            {
                kind = NoteMultipleOutline;
            }
            return new Sheldue(visibleNotes, kind, tagNoteButton, para, work, teacher, auditoria, note);
        }

        private UsersNotes GetUserNote(DateTime dateDay, string Para, string work)
        {
            UsersNotes noteRange = new UsersNotes();
            foreach (UsersNotes note in userNotes)
            {
                if (note.Para == work && note.ParaNumber == int.Parse(Para) && note.DateNote == dateDay)
                {
                    noteRange = note;
                }
            }
            return noteRange;
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
            Button buutonClick = ((Button)e.OriginalSource);
            string tag = buutonClick.Tag.ToString();
            switch (tag)
            {
                case "Day1Para1":
                    ShowModalWindow(allSheldueList[0].DayName, allSheldueList[0].Day[0].Para1[0].Work, int.Parse(allSheldueList[0].Day[0].Para1[0].Para), ref buutonClick);
                    break;
                case "Day1Para2":
                    ShowModalWindow(allSheldueList[0].DayName, allSheldueList[0].Day[0].Para2[0].Work, int.Parse(allSheldueList[0].Day[0].Para2[0].Para), ref buutonClick);
                    break;
                case "Day1Para3":
                    ShowModalWindow(allSheldueList[0].DayName, allSheldueList[0].Day[0].Para3[0].Work, int.Parse(allSheldueList[0].Day[0].Para3[0].Para), ref buutonClick);
                    break;
                case "Day1Para4":
                    ShowModalWindow(allSheldueList[0].DayName, allSheldueList[0].Day[0].Para4[0].Work, int.Parse(allSheldueList[0].Day[0].Para4[0].Para), ref buutonClick);
                    break;
                case "Day1Para5":
                    ShowModalWindow(allSheldueList[0].DayName, allSheldueList[0].Day[0].Para5[0].Work, int.Parse(allSheldueList[0].Day[0].Para4[0].Para), ref buutonClick);
                    break;
                case "Day2Para1":
                    ShowModalWindow(allSheldueList[1].DayName, allSheldueList[1].Day[0].Para1[0].Work, int.Parse(allSheldueList[1].Day[0].Para1[0].Para), ref buutonClick);
                    break;
                case "Day2Para2":
                    ShowModalWindow(allSheldueList[1].DayName, allSheldueList[1].Day[0].Para2[0].Work, int.Parse(allSheldueList[1].Day[0].Para2[0].Para), ref buutonClick);
                    break;
                case "Day2Para3":
                    ShowModalWindow(allSheldueList[1].DayName, allSheldueList[1].Day[0].Para3[0].Work, int.Parse(allSheldueList[1].Day[0].Para3[0].Para), ref buutonClick);
                    break;
                case "Day2Para4":
                    ShowModalWindow(allSheldueList[1].DayName, allSheldueList[1].Day[0].Para4[0].Work, int.Parse(allSheldueList[1].Day[0].Para4[0].Para), ref buutonClick);
                    break;
                case "Day2Para5":
                    ShowModalWindow(allSheldueList[1].DayName, allSheldueList[1].Day[0].Para5[0].Work, int.Parse(allSheldueList[1].Day[0].Para5[0].Para), ref buutonClick);
                    break;
                case "Day3Para1":
                    ShowModalWindow(allSheldueList[2].DayName, allSheldueList[2].Day[0].Para1[0].Work, int.Parse(allSheldueList[2].Day[0].Para1[0].Para), ref buutonClick);
                    break;
                case "Day3Para2":
                    ShowModalWindow(allSheldueList[2].DayName, allSheldueList[2].Day[0].Para2[0].Work, int.Parse(allSheldueList[2].Day[0].Para2[0].Para), ref buutonClick);
                    break;
                case "Day3Para3":
                    ShowModalWindow(allSheldueList[2].DayName, allSheldueList[2].Day[0].Para3[0].Work, int.Parse(allSheldueList[2].Day[0].Para3[0].Para), ref buutonClick);
                    break;
                case "Day3Para4":
                    ShowModalWindow(allSheldueList[2].DayName, allSheldueList[2].Day[0].Para4[0].Work, int.Parse(allSheldueList[2].Day[0].Para4[0].Para), ref buutonClick);
                    break;
                case "Day3Para5":
                    ShowModalWindow(allSheldueList[2].DayName, allSheldueList[2].Day[0].Para5[0].Work, int.Parse(allSheldueList[2].Day[0].Para5[0].Para), ref buutonClick);
                    break;
                case "Day4Para1":
                    ShowModalWindow(allSheldueList[3].DayName, allSheldueList[3].Day[0].Para1[0].Work, int.Parse(allSheldueList[3].Day[0].Para1[0].Para), ref buutonClick);
                    break;
                case "Day4Para2":
                    ShowModalWindow(allSheldueList[3].DayName, allSheldueList[3].Day[0].Para2[0].Work, int.Parse(allSheldueList[3].Day[0].Para2[0].Para), ref buutonClick);
                    break;
                case "Day4Para3":
                    ShowModalWindow(allSheldueList[3].DayName, allSheldueList[3].Day[0].Para3[0].Work, int.Parse(allSheldueList[3].Day[0].Para3[0].Para), ref buutonClick);
                    break;
                case "Day4Para4":
                    ShowModalWindow(allSheldueList[3].DayName, allSheldueList[3].Day[0].Para4[0].Work, int.Parse(allSheldueList[3].Day[0].Para4[0].Para), ref buutonClick);
                    break;
                case "Day4Para5":
                    ShowModalWindow(allSheldueList[3].DayName, allSheldueList[3].Day[0].Para5[0].Work, int.Parse(allSheldueList[3].Day[0].Para5[0].Para), ref buutonClick);
                    break;
                case "Day5Para1":
                    ShowModalWindow(allSheldueList[4].DayName, allSheldueList[4].Day[0].Para1[0].Work, int.Parse(allSheldueList[4].Day[0].Para1[0].Para), ref buutonClick);
                    break;
                case "Day5Para2":
                    ShowModalWindow(allSheldueList[4].DayName, allSheldueList[4].Day[0].Para2[0].Work, int.Parse(allSheldueList[4].Day[0].Para2[0].Para), ref buutonClick);
                    break;
                case "Day5Para3":
                    ShowModalWindow(allSheldueList[4].DayName, allSheldueList[4].Day[0].Para3[0].Work, int.Parse(allSheldueList[4].Day[0].Para3[0].Para), ref buutonClick);
                    break;
                case "Day5Para4":
                    ShowModalWindow(allSheldueList[4].DayName, allSheldueList[4].Day[0].Para4[0].Work, int.Parse(allSheldueList[4].Day[0].Para4[0].Para), ref buutonClick);
                    break;
                case "Day5Para5":
                    ShowModalWindow(allSheldueList[4].DayName, allSheldueList[4].Day[0].Para5[0].Work, int.Parse(allSheldueList[4].Day[0].Para5[0].Para), ref buutonClick);
                    break;
                case "Day6Para1":
                    ShowModalWindow(allSheldueList[5].DayName, allSheldueList[5].Day[0].Para1[0].Work, int.Parse(allSheldueList[5].Day[0].Para1[0].Para), ref buutonClick);
                    break;
                case "Day6Para2":
                    ShowModalWindow(allSheldueList[5].DayName, allSheldueList[5].Day[0].Para2[0].Work, int.Parse(allSheldueList[5].Day[0].Para2[0].Para), ref buutonClick);
                    break;
                case "Day6Para3":
                    ShowModalWindow(allSheldueList[5].DayName, allSheldueList[5].Day[0].Para3[0].Work, int.Parse(allSheldueList[5].Day[0].Para3[0].Para), ref buutonClick);
                    break;
                case "Day6Para4":
                    ShowModalWindow(allSheldueList[5].DayName, allSheldueList[5].Day[0].Para4[0].Work, int.Parse(allSheldueList[5].Day[0].Para4[0].Para), ref buutonClick);
                    break;
                case "Day6Para5":
                    ShowModalWindow(allSheldueList[5].DayName, allSheldueList[5].Day[0].Para5[0].Work, int.Parse(allSheldueList[5].Day[0].Para5[0].Para), ref buutonClick);
                    break;
            }

        }
        private void ShowModalWindow(string dateDay, string para, int paraNumber, ref Button butonClick)
        {
            var kindButton = butonClick.Content as MaterialDesignThemes.Wpf.PackIcon;
            switch (kindButton.Kind)
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
                    break;
                case MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline:
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
                    Storyboard sbChangeNote = _mWindow.FindResource("ShowModalWindowAddNewNote") as Storyboard;
                    sbChangeNote.Begin();
                    _mWindow.NewNoteTextBox.Clear();
                    break;
            }
        }
    }
}
