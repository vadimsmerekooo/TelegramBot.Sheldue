using System;
using System.Collections.Generic;
using Telegram_Bot.DAL.Interfaces;
using Microsoft.Office.Interop.Word;
using IFCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram_Bot.DAL.DALApp
{
    public class SheldueClass : IGetSheldue
    {
        string _name;
        string _department;
        string _group;
        List<DateTime> _dateTime;

        Dictionary<string, string> departmnetListReferensToFile = new Dictionary<string, string>()
        {
            {"Информационное", "../../../Telegram_Bot.DAL/Classes/Student/FileWord/ListShedule/Information.docx"}
            //{"Швейное", "../../../Telegram_Bot.DAL/Classes/Student/FileWord/ListShedule/Sewing.docx"},
            //{"Электромеханическое", "../../../Telegram_Bot.DAL/Classes/Student/FileWord/ListShedule/ElektroMechanic.docx"},
            //{"Отделение", "../../../Telegram_Bot.DAL/Classes/Student/FileWord/ListShedule/Mechanic.docx"}
        };
        private List<UserNotes> userNotes;


        public List<SheldueAllDays> GetListSheldue(string department, string group, List<UserNotes> notes, List<DateTime> dateTime)
        {
            _department = (department.Split(' '))[0];
            _group = group;
            userNotes = notes;
            _dateTime = dateTime;
            return GetSheldueOnDays(ParseWordFileStudentMethod());
        }
        public List<SheldueAllDays> GetListSheldue(string name, List<UserNotes> notes, List<DateTime> dateTime)
        {
            _name = name;
            _dateTime = dateTime;
            userNotes = notes;
            return GetSheldueOnDays(ParseWordFileTeacherMethod());
        }

        // Get Student Sheldue
        private List<Sheldue> ParseWordFileStudentMethod()
        {
            string pathFileWord = Path.GetFullPath(departmnetListReferensToFile[_department]);
            Application app = new Application();
            string group = $"ГРУППА {_group.Replace(" ", "")}";
            Document doc = app.Documents.Open(pathFileWord, Visible: false);
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
                                    for (int i = 3; i <= table.Rows.Count; i++)
                                    {
                                        try
                                        {
                                            if (RangeText(table.Cell(i, 6).Range.Text) != string.Empty)
                                            {
                                                string paraNumber = RangeText(table.Cell(i, 6).Range.Text);
                                                string work = RangeText(table.Cell(i, 7).Range.Text);
                                                string auditoria = RangeText(table.Cell(i, 8).Range.Text);
                                                string teacher = RangeText(table.Cell(i, 9).Range.Text);
                                                UserNotes note = new UserNotes();
                                                if (userNotes.Count != 0)
                                                    note = GetUserNote(paraNumber, work);
                                                allSheldue.Add(new Sheldue(System.Windows.Visibility.Visible,
                                                                note.Para != null
                                                                ? MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline
                                                                : MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline,
                                                                $"Para{paraNumber}",
                                                                paraNumber,
                                                                work,
                                                                teacher,
                                                                auditoria,
                                                                note));
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
                                    for (int i = 3; i <= table.Rows.Count - 1; i++)
                                    {
                                        try
                                        {
                                            if (RangeText(table.Cell(i, 2).Range.Text) != string.Empty)
                                            {
                                                string paraNumber = RangeText(table.Cell(i, 2).Range.Text);
                                                string work = RangeText(table.Cell(i, 3).Range.Text);
                                                string auditoria = RangeText(table.Cell(i, 4).Range.Text);
                                                string teacher = RangeText(table.Cell(i, 5).Range.Text);

                                                UserNotes note = new UserNotes();
                                                if (userNotes.Count != 0)
                                                    note = GetUserNote(paraNumber, work);
                                                allSheldue.Add(new Sheldue(System.Windows.Visibility.Visible,
                                                                            note.Para != null ? MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline : MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline,
                                                                            $"Para{paraNumber}",
                                                                            paraNumber,
                                                                            work,
                                                                            teacher,
                                                                            auditoria,
                                                                            note));
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

        // Get Teacher Sheldue
        private List<Sheldue> ParseWordFileTeacherMethod()
        {
            string pathFileWord = Path.GetFullPath(departmnetListReferensToFile["Информационное"]);
            Application app = new Application();
            string[] nameTeacher = _name.ToUpper().Split(' ');
            Document doc = app.Documents.Open(pathFileWord, Visible: false);
            List<Sheldue> allSheldue = new List<Sheldue>();
            bool twoGroupInTable = true;
            try
            {
                foreach (Table table in doc.Tables)
                {
                    twoGroupInTable = table.Columns.Count > 7 ? true : false;
                    switch (twoGroupInTable)
                    {
                        case true:
                            try
                            {
                                for (int i = 3; i < table.Rows.Count; i++)
                                {
                                    string[] teacherCellParse = RangeText(table.Cell(i, 5).Range.Text).Split(new char[] { ' ', '/' });
                                    foreach (var name in teacherCellParse)
                                    {
                                        if (nameTeacher[0] == name)
                                        {
                                            string paraNumber = RangeText(table.Cell(i, 2).Range.Text);
                                            string work = RangeText(table.Cell(i, 3).Range.Text);
                                            string auditoria = RangeText(table.Cell(i, 4).Range.Text);
                                            string teacher = RangeText(table.Cell(i, 5).Range.Text);

                                            UserNotes note = new UserNotes();
                                            if (userNotes.Count != 0)
                                                note = GetUserNote(paraNumber, work);
                                            allSheldue.Add(new Sheldue(System.Windows.Visibility.Visible,
                                                                        note.Para != null ? MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline : MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline,
                                                                        $"Para{paraNumber}",
                                                                        paraNumber,
                                                                        work,
                                                                        teacher,
                                                                        auditoria,
                                                                        note));
                                        }
                                    }
                                    teacherCellParse = RangeText(table.Cell(i, 9).Range.Text).Split(new char[] { ' ', '/' });
                                    foreach (var name in teacherCellParse)
                                    {
                                        if (nameTeacher[0] == name)
                                        {
                                            string paraNumber = RangeText(table.Cell(i, 6).Range.Text);
                                            string work = RangeText(table.Cell(i, 7).Range.Text);
                                            string auditoria = RangeText(table.Cell(i, 8).Range.Text);
                                            string teacher = RangeText(table.Cell(i, 9).Range.Text);

                                            UserNotes note = new UserNotes();
                                            if (userNotes.Count != 0)
                                                note = GetUserNote(paraNumber, work);
                                            allSheldue.Add(new Sheldue(System.Windows.Visibility.Visible,
                                                                        note.Para != null ? MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline : MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline,
                                                                        $"Para{paraNumber}",
                                                                        paraNumber,
                                                                        work,
                                                                        teacher,
                                                                        auditoria,
                                                                        note));
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }
                            break;
                        case false:
                            try
                            {
                                for (int i = 3; i < table.Rows.Count; i++)
                                {
                                    string[] teacherCellParse = RangeText(table.Cell(i, 5).Range.Text).Split(new char[] { ' ', '/' });
                                    foreach (var name in teacherCellParse)
                                    {
                                        if (nameTeacher[0] == name)
                                        {
                                            string paraNumber = RangeText(table.Cell(i, 2).Range.Text);
                                            string work = RangeText(table.Cell(i, 3).Range.Text);
                                            string auditoria = RangeText(table.Cell(i, 4).Range.Text);
                                            string teacher = RangeText(table.Cell(i, 5).Range.Text);
                                            UserNotes note = new UserNotes();
                                            if (userNotes.Count != 0)
                                                note = GetUserNote(paraNumber, work);

                                            allSheldue.Add(new Sheldue(System.Windows.Visibility.Visible,
                                                                        note.Para != null ? MaterialDesignThemes.Wpf.PackIconKind.NoteMultipleOutline : MaterialDesignThemes.Wpf.PackIconKind.NoteAddOutline,
                                                                        $"Para{paraNumber}",
                                                                        paraNumber,
                                                                        work,
                                                                        teacher,
                                                                        auditoria,
                                                                        note));
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }
                            break;
                    }
                }
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

            return allSheldue;
        }

        private string RangeText(string text)
        {
            return text.Replace("\r\a", "").Replace("\r", ""); ;
        }
        private UserNotes GetUserNote(string Para, string work)
        {
            foreach (UserNotes note in userNotes)
            {
                if (note.Para == work && note.ParaNumber == int.Parse(Para))
                {
                    return note;
                }
            }
            return new UserNotes();
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
                    item.TagNoteButton = $"Day{day + 1}{item.TagNoteButton}";
                    count++;
                    if (int.Parse(item.Para) < tmp || count == allSheldue.Count)
                    {
                        allDaySheldue = new SheldueAllDays(new List<SheldueAllList>()
                                                            {
                                                                allListSheldue
                                                            },
                                                            date[day] + " " + _dateTime[day].Date.ToShortDateString());
                        allDaysSheldue.Add(allDaySheldue);
                        allListSheldue = new SheldueAllList();
                        day++;
                    }
                    tmp = int.Parse(item.Para);
                    switch (tmp)
                    {
                        case 1:
                            allListSheldue.Para1 = new List<Sheldue>() { item };
                            break;
                        case 2:
                            allListSheldue.Para2 = new List<Sheldue>() { item };
                            break;
                        case 3:
                            allListSheldue.Para3 = new List<Sheldue>() { item };
                            break;
                        case 4:
                            allListSheldue.Para4 = new List<Sheldue>() { item };
                            break;
                        case 5:
                            allListSheldue.Para5 = new List<Sheldue>() { item };
                            break;
                    }
                    if (count == allSheldue.Count)
                    {
                        allDaySheldue = new SheldueAllDays(new List<SheldueAllList>()
                                                            {
                                                                allListSheldue
                                                            },
                                                            date[day]);
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
