using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IFCore;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Linq;

namespace Telegram_Bot.DAL.Classes.GetShledueFolder
{
    public class GetSheldueDAL
    {

        Dictionary<string, string> departmnetListReferensToFile = new Dictionary<string, string>()
        {
            {"Информационное", "../../../Telegram_Bot.DAL/Classes/Student/FileWord/ListShedule/Information.docx"},
            {"Швейное", "../../../Telegram_Bot.DAL/Classes/Student/FileWord/ListShedule/Sewing.docx"},
            {"Электромеханическое", "../../../Telegram_Bot.DAL/Classes/Student/FileWord/ListShedule/ElektroMechanic.docx"},
            {"Отделение", "../../../Telegram_Bot.DAL/Classes/Student/FileWord/ListShedule/Mechanic.docx"}
        };

        public Dictionary<string, List<SheldueAllDaysTelegram>> GetSheldueAllGroup()
        {
            return ParseWordFileStudentMethod();
        }
        private Dictionary<string, List<SheldueAllDaysTelegram>> ParseWordFileStudentMethod()
        {
            Dictionary<string, List<SheldueAllDaysTelegram>> allSheldue = new Dictionary<string, List<SheldueAllDaysTelegram>>();
            foreach (var item in departmnetListReferensToFile)
            {
                try
                {
                    // Берем путь к файлу
                    string pathFileWord = Path.GetFullPath(item.Value);
                    // Выделяем память для word прии=ложения
                    Application app = new Application();
                    // Загружаем документ
                    Document doc = app.Documents.Open(pathFileWord, Visible: false);
                    List<SheldueTelegram> allSheldueForGroup = new List<SheldueTelegram>();
                    try
                    {
                        foreach (Table table in doc.Tables)
                        {
                            try
                            {
                                // Чекаем, если таблица состоит из двух колонок или одной колонки
                                switch (table.Columns.Count > 6 ? true : false)
                                {
                                    case true:
                                        try
                                        {
                                            allSheldueForGroup = new List<SheldueTelegram>();
                                            string groupTwoColumns = string.Empty;
                                            // Проходимся циклом по второй колонке
                                            for (int i = table.Columns.Count / 2; i <= table.Columns.Count; i++)
                                            {
                                                // Чекаем на группу
                                                if ((RangeText(table.Cell(1, i).Range.Text).Split(' '))[0] == "ГРУППА")
                                                {
                                                    groupTwoColumns = RangeText(table.Cell(1, i).Range.Text);
                                                    // Проходимся по колонке, и выписываем все строки
                                                    for (int ii = 3; ii <= table.Rows.Count; ii++)
                                                    {
                                                        try
                                                        {
                                                            if (RangeText(table.Cell(ii, 6).Range.Text) != string.Empty)
                                                            {
                                                                string paraNumber = RangeText(table.Cell(ii, 6).Range.Text);
                                                                string work = RangeText(table.Cell(ii, 7).Range.Text);
                                                                string auditoria = RangeText(table.Cell(ii, 8).Range.Text);
                                                                string teacher = RangeText(table.Cell(ii, 9).Range.Text);

                                                                allSheldueForGroup.Add(new SheldueTelegram(paraNumber,
                                                                                            work,
                                                                                            teacher,
                                                                                            auditoria));
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    allSheldue.Add(groupTwoColumns, GetSheldueOnDays(allSheldueForGroup));
                                                    allSheldueForGroup = new List<SheldueTelegram>();
                                                    break;
                                                }
                                            }
                                            // Проходимся циклом по первой колонке
                                            for (int i = 1; i <= table.Columns.Count / 2 + 1; i++)
                                            {
                                                // Чекаем на группу
                                                if (RangeText(table.Cell(1, i).Range.Text).Split(' ')[0] == "ГРУППА")
                                                {
                                                    groupTwoColumns = RangeText(table.Cell(1, i).Range.Text);
                                                    // Проходимся по колонке, и выписываем все строки
                                                    for (int ii = 3; ii <= table.Rows.Count; ii++)
                                                    {
                                                        try
                                                        {
                                                            if (RangeText(table.Cell(ii, 2).Range.Text) != string.Empty)
                                                            {
                                                                string paraNumber = RangeText(table.Cell(ii, 2).Range.Text);
                                                                string work = RangeText(table.Cell(ii, 3).Range.Text);
                                                                string auditoria = RangeText(table.Cell(ii, 4).Range.Text);
                                                                string teacher = RangeText(table.Cell(ii, 5).Range.Text);

                                                                allSheldueForGroup.Add(new SheldueTelegram(paraNumber,
                                                                                            work,
                                                                                            teacher,
                                                                                            auditoria));
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    allSheldue.Add(groupTwoColumns, GetSheldueOnDays(allSheldueForGroup));
                                                    allSheldueForGroup = new List<SheldueTelegram>();
                                                    break;
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        break;
                                    case false:
                                        try
                                        {
                                            allSheldueForGroup = new List<SheldueTelegram>();
                                            // Проходимся циклом по первой колонке
                                            for (int i = 1; i <= table.Columns.Count; i++)
                                            {
                                                // Чекаем на группу
                                                if (RangeText(table.Cell(1, i).Range.Text).Split(' ')[0] == "ГРУППА")
                                                {
                                                    string groupOneColums = RangeText(table.Cell(1, i).Range.Text);
                                                    // Проходимся по колонке, и выписываем все строки
                                                    for (int ii = 3; ii <= table.Rows.Count; ii++)
                                                    {
                                                        try
                                                        {
                                                            if (RangeText(table.Cell(ii, 2).Range.Text) != string.Empty)
                                                            {
                                                                string paraNumber = RangeText(table.Cell(ii, 2).Range.Text);
                                                                string work = RangeText(table.Cell(ii, 3).Range.Text);
                                                                string auditoria = RangeText(table.Cell(ii, 4).Range.Text);
                                                                string teacher = RangeText(table.Cell(ii, 5).Range.Text);

                                                                allSheldueForGroup.Add(new SheldueTelegram(paraNumber,
                                                                                            work,
                                                                                            teacher,
                                                                                            auditoria));
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    allSheldue.Add(groupOneColums, GetSheldueOnDays(allSheldueForGroup));
                                                    allSheldueForGroup = new List<SheldueTelegram>();
                                                    break;
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        break;
                                }
                            }
                            catch
                            {

                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{item.Key} отделение загруженно!");
                        Console.ResetColor();
                    }
                    catch
                    {
                        doc.Close();
                        app.Quit();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{item.Key} отделение не загруженно!");
                        Console.ResetColor();
                        continue;
                    }
                    finally
                    {
                        try
                        {
                            doc.Close();
                            app.Quit();
                        }
                        catch
                        {

                        }
                        if (Process.GetProcessesByName("winword").Count() > 0)
                        {
                            Microsoft.Office.Interop.Word.Application wordInstance = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                            IFCore.IFCore.loggerMain.Error("Ошибка при доступе к файлу " +  item + " отделения");
                            foreach (Microsoft.Office.Interop.Word.Document docs in wordInstance.Documents)
                            {
                                if (docs.Name == "Information.docx" || docs.Name == "Sewing.docx" || docs.Name == "ElektroMechanic.docx" || docs.Name == "Mechanic.docx")
                                {
                                    docs.Close();
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    int lineEx = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                    IFCore.IFCore.loggerMain.Error("DAL => ParseWordFileStudentMethod class " + ex.ToString() + lineEx);
                    continue;
                }
            }
            return allSheldue;
        }

        // Очистка текста от лишних символов
        private string RangeText(string text)
        {
            return text.Replace("\r\a", "").Replace("\r", ""); ;
        }

        // Метод составления расписания по дням
        private List<SheldueAllDaysTelegram> GetSheldueOnDays(List<SheldueTelegram> allSheldue)
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

            List<SheldueAllDaysTelegram> allDaysSheldue = new List<SheldueAllDaysTelegram>();
            try
            {
                int tmp = 1;
                int day = 0;
                int count = 0;
                SheldueAllListTelegram allListSheldue = new SheldueAllListTelegram();
                SheldueAllDaysTelegram allDaySheldue;
                foreach (var item in allSheldue)
                {
                    count++;
                    if (int.Parse(item.Para) < tmp || count == allSheldue.Count)
                    {
                        allDaySheldue = new SheldueAllDaysTelegram(new List<SheldueAllListTelegram>()
                                                            {
                                                                allListSheldue
                                                            },
                                                            date[day]);
                        allDaysSheldue.Add(allDaySheldue);
                        allListSheldue = new SheldueAllListTelegram();
                        day++;
                    }
                    tmp = int.Parse(item.Para);
                    switch (tmp)
                    {
                        case 1:
                            allListSheldue.Para1 = new List<SheldueTelegram>() { item };
                            break;
                        case 2:
                            allListSheldue.Para2 = new List<SheldueTelegram>() { item };
                            break;
                        case 3:
                            allListSheldue.Para3 = new List<SheldueTelegram>() { item };
                            break;
                        case 4:
                            allListSheldue.Para4 = new List<SheldueTelegram>() { item };
                            break;
                        case 5:
                            allListSheldue.Para5 = new List<SheldueTelegram>() { item };
                            break;
                    }
                    if (count == allSheldue.Count)
                    {
                        allDaySheldue = new SheldueAllDaysTelegram(new List<SheldueAllListTelegram>()
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
