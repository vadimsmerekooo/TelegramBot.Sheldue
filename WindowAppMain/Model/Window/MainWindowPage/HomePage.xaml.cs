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

namespace WindowAppMain.Model.Window.MainWindowPage
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            var sheldue = GetSheldue();
            ListViewSheldueDay.ItemsSource = sheldue;
        }
        private List<SheldueAllDays> GetSheldue()
        {
            return new List<SheldueAllDays>()
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
