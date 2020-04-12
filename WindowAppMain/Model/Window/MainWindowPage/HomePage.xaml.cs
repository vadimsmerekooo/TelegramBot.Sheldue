using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WindowAppMain.Model.Controls;

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
        public HomePage()
        {
            InitializeComponent();
            loadedControl = new LoadingAnimation();
            MainSheldueGrid.Children.Add(loadedControl);
            loadedControl.StartAnimation();
            Task.Run(() => this.Dispatcher.BeginInvoke((ThreadStart) delegate() { LoadAsyncMethod(); }));
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
            this.Dispatcher.BeginInvoke((ThreadStart) delegate
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
                            new Sheldue("1","Понедельник", "Пуся", "23")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("2","para2", "Prepod2", "2Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("4","para4", "Prepod4", "4Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("5","para5", "Prepod5", "5Kab")
                        })
                  }, "Понедельник"),
                  //Tuesday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            new Sheldue("1","Вторник", "Пуся", "23")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("2","para2", "Prepod2", "2Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("4","para4", "Prepod4", "4Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("5","para5", "Prepod5", "5Kab")
                        })
                  }, "Вторник"),
                  //Wednesday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            new Sheldue("1","Среда", "Пуся", "23")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("2","para2", "Prepod2", "2Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("4","para4", "Prepod4", "4Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("5","para5", "Prepod5", "5Kab")
                        })
                  }, "Среда"),
                  //Thursday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            new Sheldue("1","Четверг", "Пуся", "23")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("2","para2", "Prepod2", "2Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("4","para4", "Prepod4", "4Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("5","para5", "Prepod5", "5Kab")
                        })
                  }, "Четверг"),
                  //Friday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            new Sheldue("1","Пятница", "Пуся", "23")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("2","para2", "Prepod2", "2Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("4","para4", "Prepod4", "4Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("5","para5", "Prepod5", "5Kab")
                        })
                  }, "Пятница"),
                  //Saturday
                  new SheldueAllDays(
                  new List<SheldueAllList>()
                  {
                      new SheldueAllList(
                        new List<Sheldue>()
                        {
                            new Sheldue("1","Суббота", "Пуся", "23")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("2","para2", "Prepod2", "2Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("3","para3", "Prepod3", "3Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("4","para4", "Prepod4", "4Kab")
                        },
                        new List<Sheldue>()
                        {
                            new Sheldue("5","para5", "Prepod5", "5Kab")
                        })
                  }, "Суббота"
            )};
        }
    }
}
