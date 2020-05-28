using IFCore;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;

namespace Telegram_Bot.DAL.Classes.GetShledueFolder
{
    public class GetChangesForSheldue
    {
        private string week = string.Empty;
        private string dayNewSheldue = string.Empty;
        private Dictionary<string, List<SheldueAllDaysTelegram>> allChangesSheldue = new Dictionary<string, List<SheldueAllDaysTelegram>>();
        
        public Dictionary<string, List<SheldueAllDaysTelegram>> GetChangesSheldue(out string week, out string dayNewSheldue)
        {
            return ParseWordFileStudentMethod(out week, out dayNewSheldue);
        }
        private Dictionary<string, List<SheldueAllDaysTelegram>> ParseWordFileStudentMethod(out string week, out string dayNewSheldue)
        {
            if (DownloadFileChangesForSheldue())
            {
                string fullPathToFileChanegsSheldue = Path.GetFullPath("changesSheldue.docx");
                return ParseWordFileChangeSheldueMethod(fullPathToFileChanegsSheldue, out week, out dayNewSheldue);
            }
            else
            {
                week = string.Empty;
                dayNewSheldue = string.Empty;
                return null;
            }
        }

        private Dictionary<string, List<SheldueAllDaysTelegram>> ParseWordFileChangeSheldueMethod(string fullPathToFileChangesFile, out string week, out string dayNewSheldue)
        {
            week = string.Empty;
            dayNewSheldue = string.Empty;
            // Выделяем память для word прии=ложения
            Application app = new Application();
            // Загружаем документ
            Document doc = app.Documents.Open(fullPathToFileChangesFile, Visible: false);

            List<SheldueTelegram> allChangesSheldueForGroup = new List<SheldueTelegram>();
            try
            {
                // Определение недели\
                foreach (Paragraph parag in doc.Paragraphs)
                {
                    var paragSplit = RangeText(parag.Range.Text).Split(new char[] { ' ', '(', ')' });


                    string today = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                    string tomorrow = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(1).DayOfWeek);
                    string tomorrow1 = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(2).DayOfWeek);


                    foreach (string word in paragSplit)
                    {
                        if (word.Contains(today) || word.Contains(tomorrow) || word.Contains(tomorrow1))
                        {
                            dayNewSheldue = word.Contains(today) ? today
                                : word.Contains(tomorrow) ? tomorrow
                                : word.Contains(tomorrow1) ? tomorrow1
                                : string.Empty;
                        }
                    }
                    foreach (string word in paragSplit)
                    {
                        if (word.Contains("нечётная") || word.Contains("чётная"))
                        {
                            week = word;
                            return ParseCellsRanegTextTable(doc.Tables);
                        }
                    }
                }
                return allChangesSheldue;
            }
            catch
            {
                doc.Close();
                app.Quit();
                return allChangesSheldue;
            }
            finally
            {
                doc.Close();
                app.Quit();
            }
        }

        private Dictionary<string, List<SheldueAllDaysTelegram>> ParseCellsRanegTextTable(Tables tables)
        {
            List<SheldueAllDaysTelegram> changesSheldue = new List<SheldueAllDaysTelegram>();
            try
            {
                foreach (Table table in tables)
                {
                    for (int i = 1; i <= table.Rows.Count; i++)
                    {
                        for (int j = 1; j <= table.Columns.Count; j++)
                        {
                            try
                            {
                                if (RangeText(table.Cell(i, j).Range.Text).Contains("Расписание"))
                                {
                                    var splitCell = table.Cell(i, j).Range.Text.Split('\r');
                                    var groupSplit = splitCell[0].Split(' ');
                                    allChangesSheldue.Add(groupSplit[2], FormatedChangesSheldue(new List<string>(splitCell)));
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
            catch
            {

            }
            return allChangesSheldue;
        }

        private List<SheldueAllDaysTelegram> FormatedChangesSheldue(List<string> splitCell)
        {
            List<SheldueAllDaysTelegram> cellSheldue = new List<SheldueAllDaysTelegram>();
            string sheldueForDay = string.Empty;
            splitCell.RemoveAt(0);
            foreach (var text in splitCell)
            {
                sheldueForDay += $@"{text}
";
            }
            cellSheldue.Add(new SheldueAllDaysTelegram(
                    new List<SheldueAllListTelegram>()
                    {
                        new SheldueAllListTelegram()
                        {
                            ChangeSheldue = sheldueForDay
                        }
                    },
                    dayNewSheldue));
            return cellSheldue;
        }


        // Очистка текста от лишних символов
        private string RangeText(string text)
        {
            return text.Replace("\r\a", "").Replace("\r", ""); ;
        }
        private bool DownloadFileChangesForSheldue()
        {
            try
            {
                if (File.Exists("changesSheldue.docx"))
                {
                    File.Delete("changesSheldue.docx");
                }
                try { new WebClient().DownloadFile("http://ggkttd.by/wp-content/uploads/2016/01/%D0%B7%D0%B0%D0%BC%D0%B5%D0%BD%D1%8B2018.docx", "changesSheldue.docx"); } catch { }
                return File.Exists("changesSheldue.docx") ? true : false;
            }
            catch
            {
                return false;
            }
        }

    }
}
