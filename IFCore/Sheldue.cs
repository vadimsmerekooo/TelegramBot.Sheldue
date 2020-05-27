using System;
using System.Collections.Generic;

namespace IFCore
{
    public class Sheldue
    {
        public System.Windows.Visibility Notes { get; set; }
        public MaterialDesignThemes.Wpf.PackIconKind Kind { get; set; }
        public string TagNoteButton { get; set; }
        public string Para { get; set; }
        public string Work { get; set; }
        public string Teacher { get; set; }
        public string Auditorya { get; set; }
        public UserNotes userNotes { get; set; }

        public Sheldue(System.Windows.Visibility Notes, MaterialDesignThemes.Wpf.PackIconKind Kind, string TagNoteButton, string para, string work, string teacher, string auditorya, UserNotes note)
        {
            this.Notes = Notes;
            this.Kind = Kind;
            this.TagNoteButton = TagNoteButton;
            Para = para;
            Work = work;
            Teacher = teacher;
            Auditorya = auditorya;
            userNotes = note;
        }
    }
    public class SheldueTelegram
    {
        public string Para { get; set; }
        public string Work { get; set; }
        public string Teacher { get; set; }
        public string Auditorya { get; set; }

        public SheldueTelegram(string para, string work, string teacher, string auditorya)
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
        public SheldueAllList(List<Sheldue> para1, List<Sheldue> para2, List<Sheldue> para3, List<Sheldue> para4)
        {
            Para1 = para1;
            Para2 = para2;
            Para3 = para3;
            Para4 = para4;
        }
        public SheldueAllList(List<Sheldue> para1, List<Sheldue> para2, List<Sheldue> para3)
        {
            Para1 = para1;
            Para2 = para2;
            Para3 = para3;
        }
        public SheldueAllList(List<Sheldue> para1, List<Sheldue> para2)
        {
            Para1 = para1;
            Para2 = para2;
        }
        public SheldueAllList(List<Sheldue> para1)
        {
            Para1 = para1;
        }
        public SheldueAllList()
        {

        }
    }
    public class SheldueAllListTelegram
    {
        public string ChangeSheldue { get; set; }
        public List<SheldueTelegram> Para1 { get; set; }
        public List<SheldueTelegram> Para2 { get; set; }
        public List<SheldueTelegram> Para3 { get; set; }
        public List<SheldueTelegram> Para4 { get; set; }
        public List<SheldueTelegram> Para5 { get; set; }
        public SheldueAllListTelegram(List<SheldueTelegram> para1, List<SheldueTelegram> para2, List<SheldueTelegram> para3, List<SheldueTelegram> para4, List<SheldueTelegram> para5)
        {
            Para1 = para1;
            Para2 = para2;
            Para3 = para3;
            Para4 = para4;
            Para5 = para5;
        }
        public SheldueAllListTelegram(List<SheldueTelegram> para1, List<SheldueTelegram> para2, List<SheldueTelegram> para3, List<SheldueTelegram> para4)
        {
            Para1 = para1;
            Para2 = para2;
            Para3 = para3;
            Para4 = para4;
        }
        public SheldueAllListTelegram(List<SheldueTelegram> para1, List<SheldueTelegram> para2, List<SheldueTelegram> para3)
        {
            Para1 = para1;
            Para2 = para2;
            Para3 = para3;
        }
        public SheldueAllListTelegram(List<SheldueTelegram> para1, List<SheldueTelegram> para2)
        {
            Para1 = para1;
            Para2 = para2;
        }
        public SheldueAllListTelegram(List<SheldueTelegram> para1)
        {
            Para1 = para1;
        }
        public SheldueAllListTelegram(string changeSheldue)
        {
            ChangeSheldue = changeSheldue;
        }
        public SheldueAllListTelegram()
        {

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
    public class SheldueAllDaysTelegram
    {
        public List<SheldueAllListTelegram> Day { get; set; }
        public string DayName { get; set; }
        public SheldueAllDaysTelegram(List<SheldueAllListTelegram> day, string dayName)
        {
            Day = day;
            DayName = dayName;
        }
    }
}
