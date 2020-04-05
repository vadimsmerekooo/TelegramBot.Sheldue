using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowAppMain.Model.Window.MainWindowPage
{
    public class Sheldue
    {
        public string Para { get; set; }
        public string Work { get; set; }
        public string Teacher { get; set; }
        public string Auditorya { get; set; }

        public Sheldue(string para, string work, string teacher, string auditorya)
        {
            Para = para;
            Work = work;
            Teacher = teacher;
            Auditorya = auditorya;
        }
    }
    public class SheldueAllList
    {
        public List<Sheldue> Para1 { get; set; }
        public List<Sheldue> Para2 { get; set; }
        public List<Sheldue> Para3 { get; set; }
        public List<Sheldue> Para4 { get; set; }
        public List<Sheldue> Para5 { get; set; }
        public SheldueAllList(List<Sheldue> para1, List<Sheldue> para2, List<Sheldue> para3, List<Sheldue> para4, List<Sheldue> para5)
        {
            Para1 = para1;
            Para2 = para2;
            Para3 = para3;
            Para4 = para4;
            Para5 = para5;
        }
    }
    public class SheldueAllDays
    {
        public List<SheldueAllList> Day { get; set; }
        public string DayName { get; set; }
        public SheldueAllDays(List<SheldueAllList> day, string dayName)
        {
            Day = day;
            DayName = dayName;
        }
    }
}
