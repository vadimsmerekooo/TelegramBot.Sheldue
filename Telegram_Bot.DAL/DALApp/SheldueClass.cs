using System;
using System.Collections.Generic;
using Telegram_Bot.DAL.Interfaces;
using Microsoft.Office.Interop.Word;
using IFCore;
using System.IO;

namespace Telegram_Bot.DAL.DALApp
{
    public class SheldueClass : IGetSheldue
    {
        string _department;
        string _group;
        List<DateTime> _dateTime;

        Dictionary<string, string> departmnetListReferensToFile = new Dictionary<string, string>()
        {
            {"Информационное", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Information.docx"},
            {"Швейное", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Sewing.docx"},
            {"Электромеханическое", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\ElektroMechanic.docx"},
            {"Машиностроения", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Mechanic.docx"}
        };
        private List<UserNotes> userNotes;


        public List<SheldueAllDays> GetListSheldue(string department, string group, List<UserNotes> notes, List<DateTime> dateTime)
        {
            //string imagePath = $"../../Resource/logoAccount.png";
            //Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            //return new BitmapImage(imageUri);
            File.Open("../../../Telegram_Bot.DAL/Classes/Student/FileWord/ListShedule/Information.docx", FileMode.Open);
            if (File.Exists("../../Classes/Student/FileWord/ListShedule/Information.docx"))
                return null;
            else
                return null;
            _department = (department.Split(' '))[0];
            _group = group;
            userNotes = notes;
            _dateTime = dateTime;
            return GetSheldueOnDays(ParseWordFileMethod());
        }



        private List<Sheldue> ParseWordFileMethod()
        {
            Application app = new Application();
            string group = $"ГРУППА {_group.Replace(" ", "")}";
            Document doc = app.Documents.Open(departmnetListReferensToFile[_department], Visible: false);
            List<Sheldue> allSheldue = new List<Sheldue>();
            try
            {
                foreach (Table table in doc.Tables)
                {
                    try
                    {
                        for (int j = 1; j < table.Columns.Count - 1; j++)
                        {
                            if (RangeText(table.Cell(1, j).Range.Text) == group)
                            {
                                if (j > 4)
                                {
                                    for (int i = 3; i < table.Rows.Count; i++)
                                    {
                                        try
                                        {
                                            if (RangeText(table.Cell(i, 6).Range.Text) != string.Empty)
                                            {
                                                string paraNumber = RangeText(table.Cell(i, 6).Range.Text);
                                                string work = RangeText(table.Cell(i, 7).Range.Text);
                                                string auditoria = RangeText(table.Cell(i, 8).Range.Text);
                                                string teacher = RangeText(table.Cell(i, 9).Range.Text);

                                                UserNotes note = GetUserNote(paraNumber, work);
                                                Sheldue paraSheldue = new Sheldue(
                                                    System.Windows.Visibility.Visible,
                                                    note.Para != null ? MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline : MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline,
                                                    $"Para{paraNumber}",
                                                    paraNumber,
                                                    work,
                                                    teacher,
                                                    auditoria,
                                                    note
                                                    );
                                                allSheldue.Add(paraSheldue);
                                            }
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 3; i < table.Rows.Count - 1; i++)
                                    {
                                        try
                                        {
                                            if (RangeText(table.Cell(i, 2).Range.Text) != string.Empty)
                                            {
                                                string paraNumber = RangeText(table.Cell(i, 2).Range.Text);
                                                string work = RangeText(table.Cell(i, 3).Range.Text);
                                                string auditoria = RangeText(table.Cell(i, 4).Range.Text);
                                                string teacher = RangeText(table.Cell(i, 5).Range.Text);

                                                UserNotes note = GetUserNote(paraNumber, work);
                                                Sheldue paraSheldue = new Sheldue(
                                                    System.Windows.Visibility.Visible,
                                                    note != null ? MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline : MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline,
                                                    $"Para{paraNumber}",
                                                    paraNumber,
                                                    work,
                                                    teacher,
                                                    auditoria,
                                                    note
                                                    );
                                                allSheldue.Add(paraSheldue);
                                            }
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                return allSheldue;
            }
            catch
            {
                doc.Close();
                app.Quit();
                return allSheldue;
            }
            finally
            {
                doc.Close();
                app.Quit();
            }
        }
        private string RangeText(string text)
        {
            return text.Replace("\r\a", "").Replace("\r", ""); ;
        }
        private UserNotes GetUserNote(string Para, string work)
        {
            UserNotes noteRange = new UserNotes();
            foreach (UserNotes note in userNotes)
            {
                if (note.Para == work && note.ParaNumber == int.Parse(Para))
                {
                    noteRange = note;
                }
            }
            return noteRange;
        }

        private List<SheldueAllDays> GetSheldueOnDays(List<Sheldue> allSheldue)
        {
            List<string> date = new List<string>()
            {
                "Понедельник",
                "Вторник",
                "Среда",
                "Четверг",
                "Пятница",
                "Суббота"
            };
            List<SheldueAllDays> allDaysSheldue = new List<SheldueAllDays>();
            try
            {
                int tmp = 1;
                int day = 0;
                int count = 0;
                SheldueAllList allListSheldue = new SheldueAllList();
                SheldueAllDays allDaySheldue;
                foreach (var item in allSheldue)
                {
                    count++;
                    if (int.Parse(item.Para) < tmp || count == allSheldue.Count)
                    {
                        List<SheldueAllList> listAllDaySheldue = new List<SheldueAllList>()
                        {
                            allListSheldue
                        };
                        allDaySheldue = new SheldueAllDays(listAllDaySheldue, date[day] + " " + _dateTime[day].Date.ToShortDateString());
                        allDaysSheldue.Add(allDaySheldue);
                        allListSheldue = new SheldueAllList();
                        day++;
                    }
                    tmp = int.Parse(item.Para);
                    List<Sheldue> listSheldue = new List<Sheldue>()
                    {
                        item
                    };
                    switch (tmp)
                    {
                        case 1:
                            allListSheldue.Para1 = listSheldue;
                            break;
                        case 2:
                            allListSheldue.Para2 = listSheldue;
                            break;
                        case 3:
                            allListSheldue.Para3 = listSheldue;
                            break;
                        case 4:
                            allListSheldue.Para4 = listSheldue;
                            break;
                        case 5:
                            allListSheldue.Para5 = listSheldue;
                            break;
                    }
                    if (count == allSheldue.Count)
                    {
                        List<SheldueAllList> listAllDaySheldue = new List<SheldueAllList>()
                        {
                            allListSheldue
                        };
                        allDaySheldue = new SheldueAllDays(listAllDaySheldue, date[day]);
                        allDaysSheldue.Add(allDaySheldue);
                    }
                }
            }
            catch
            {

            }
            return allDaysSheldue;
        }
    }
}
