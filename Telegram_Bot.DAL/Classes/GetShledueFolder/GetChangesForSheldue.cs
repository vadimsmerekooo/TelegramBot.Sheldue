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
        private string week;
        private string dayNewSheldue;
        public Dictionary<string, List<SheldueAllDaysTelegram>> GetChangesSheldue()
        {
            return ParseWordFileStudentMethod();
        }
        private Dictionary<string, List<SheldueAllDaysTelegram>> ParseWordFileStudentMethod()
        {
            if (DownloadFileChangesForSheldue())
            {
                string fullPathToFileChanegsSheldue = Path.GetFullPath("changesSheldue.docx");
                return ParseWordFileChangeSheldueMethod(fullPathToFileChanegsSheldue);
            }
            else
            {
                return null;
            }
        }

        private Dictionary<string, List<SheldueAllDaysTelegram>> ParseWordFileChangeSheldueMethod(string fullPathToFileChangesFile)
        {
            // Выделяем память для word прии=ложения
            Application app = new Application();
            // Загружаем документ
            Document doc = app.Documents.Open(fullPathToFileChangesFile, Visible: false);

            Dictionary<string, List<SheldueAllDaysTelegram>> allChangesSheldue = new Dictionary<string, List<SheldueAllDaysTelegram>>();
            List<SheldueTelegram> allChangesSheldueForGroup = new List<SheldueTelegram>();
            try
            {
                // Определение недели\
                foreach (Paragraph parag in doc.Paragraphs)
                {
                    var paragSplit = RangeText(parag.Range.Text).Split(new char[] { ' ', '(', ')' });
                    foreach (string word in paragSplit)
                    {
                        string today = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(new DateTime().DayOfWeek);
                        string tomorrow = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(new DateTime().AddDays(1).DayOfWeek);
                        string tomorrow1 = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(new DateTime().AddDays(2).DayOfWeek);
                        dayNewSheldue = word.Contains(today) ? today : word.Contains(tomorrow) ? tomorrow : word.Contains(tomorrow1) ? tomorrow1 : string.Empty;
                        if (word.Contains(dayNewSheldue))
                        {
                            if (word.Contains("нечётная") || word.Contains("чётная"))
                            {
                                week = word;
                            }
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
            Dictionary<string, List<SheldueAllDaysTelegram>> changesSheldue = new Dictionary<string, List<SheldueAllDaysTelegram>>();
            try
            {
                foreach (Table table in tables)
                {
                    for (int i = 0; i <= table.Rows.Count; i++)
                    {
                        for (int j = 0; j <= table.Columns.Count; j++)
                        {
                            if (RangeText(table.Cell(i, j).Range.Text).Contains("Расписание"))
                            {
                                var splitCell = table.Cell(i, j).Range.Text.Split('\r');
                                var groupSplit = splitCell[0].Split(' ');
                                changesSheldue?.Add(
                                    groupSplit[2],
                                    FormatedChangesSheldue(new List<string>(splitCell)));
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return changesSheldue;
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
