using IFCore;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace Telegram_Bot.DAL.Classes.GetShledueFolder
{
    public class GetChangesForSheldue
    {
        private string week = string.Empty;
        private string dayNewSheldue = string.Empty;
        private Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> allChangesSheldue = new Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>>();

        public Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> GetChangesSheldue(out string week)
        {
            return ParseWordFileStudentMethod(out week);
        }
        private Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> ParseWordFileStudentMethod(out string week)
        {
            if (DownloadFileChangesForSheldue())
            {
                string fullPathToFileChanegsSheldue = Path.GetFullPath("changesSheldue.docx");
                return ParseWordFileChangeSheldueMethod(fullPathToFileChanegsSheldue, out week);
            }
            else
            {
                week = string.Empty;
                dayNewSheldue = string.Empty;
                return null;
            }
        }

        private Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> ParseWordFileChangeSheldueMethod(string fullPathToFileChangesFile, out string week)
        {
            List<SheldueTelegram> allChangesSheldueForGroup = new List<SheldueTelegram>();
            week = string.Empty;
            int counter = 0;
            try
            {
                // Выделяем память для word прии=ложения
                Application app = new Application();
                // Загружаем документ
                Document doc = app.Documents.Open(fullPathToFileChangesFile, Visible: false);

                try
                {
                    List<string> listDate = new List<string>()
                        {
                            CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(-3).DayOfWeek),
                            CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(-2).DayOfWeek),
                            CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(-1).DayOfWeek),
                            DayNowSaturdayDelete(CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek)),
                            CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(1).DayOfWeek),
                            CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(2).DayOfWeek),
                            CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(3).DayOfWeek)
                        };
                    // Определение недели\
                    foreach (Paragraph parag in doc.Paragraphs)
                    {
                        var paragSplit = RangeText(parag.Range.Text).Split(new char[] { ' ', '(', ')' });


                        foreach (string word in paragSplit)
                        {
                            foreach (var day in listDate)
                            {
                                if (word.Contains(day))
                                {
                                    dayNewSheldue = day;
                                    int posititonDay = listDate.IndexOf(day);
                                    foreach (Paragraph parag2 in doc.Paragraphs)
                                    {
                                        var paragSplit2 = RangeText(parag2.Range.Text).Split(new char[] { ' ', '(', ')' });
                                        foreach (string wordl in paragSplit2)
                                        {
                                            if (wordl.Replace("/t", "").Contains("нечётная") || wordl.Replace("/t", "").Contains("чётная"))
                                            {
                                                week = wordl;
                                                counter++;
                                                if (counter == 2)
                                                {
                                                    dayNewSheldue = dayNewSheldue != "суббота" ? listDate[posititonDay + 1] : listDate[posititonDay + 2];
                                                    allChangesSheldue.Add(dayNewSheldue, ParseCellsRanegTextTable(doc.Tables[counter]));
                                                }
                                                else
                                                {
                                                    allChangesSheldue.Add(dayNewSheldue, ParseCellsRanegTextTable(doc.Tables[counter]));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return allChangesSheldue;
                }
                catch (Exception ex)
                {
                    doc.Close();
                    app.Quit();
                    int lineEx = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                    IFCore.IFCore.loggerMain.Error("DAL => ParseWordFileChangeSheldueMethod class " + ex.ToString() + lineEx);
                    return allChangesSheldue;
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

                        foreach (Microsoft.Office.Interop.Word.Document docs in wordInstance.Documents)
                        {
                            if (docs.Name == "changesSheldue.docx")
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
                IFCore.IFCore.loggerMain.Error("DAL => ParseWordFileChangeSheldueMethod class " + ex.ToString() + lineEx);
            }
            return allChangesSheldue;
        }

        private string DayNowSaturdayDelete(string day)
        {
            return day == "воскресенье" ? "понедельник" : day;
        }
        private Dictionary<string, List<SheldueAllDaysTelegram>> ParseCellsRanegTextTable(Table table)
        {
            Dictionary<string, List<SheldueAllDaysTelegram>> changesSheldues = new Dictionary<string, List<SheldueAllDaysTelegram>>();
            List<SheldueAllDaysTelegram> changesSheldue = new List<SheldueAllDaysTelegram>();
            try
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
                                changesSheldues.Add(groupSplit[2], FormatedChangesSheldue(new List<string>(splitCell)));
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                int lineEx = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                IFCore.IFCore.loggerMain.Error("DAL => ParseCellsRanegTextTable class " + ex.ToString() + lineEx);
            }
            return changesSheldues;
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
            return text.Replace("\r\a", "").Replace("\r", "");
        }
        private bool DownloadFileChangesForSheldue()
        {
            try
            {
                if (File.Exists("changesSheldue.docx"))
                {
                    File.Delete("changesSheldue.docx");
                }                                   
                try { new WebClient().DownloadFile("http://ggkttd.by/wp-content/uploads/2016/01/замены2018.docx", "changesSheldue.docx"); } catch { }
                return File.Exists("changesSheldue.docx") ? true : false;
            }
            catch(Exception ex)
            {
                IFCore.IFCore.loggerMain.Error("Ошибка при доступе к файлу с изм. Расписанием: " + ex);
                if (Process.GetProcessesByName("winword").Count() > 0)
                {
                    Microsoft.Office.Interop.Word.Application wordInstance = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");

                    foreach (Microsoft.Office.Interop.Word.Document docs in wordInstance.Documents)
                    {
                        if (docs.Name == "changesSheldue.docx")
                        {
                            docs.Close();
                            DownloadFileChangesForSheldue(); 
                            break;
                        }
                    }
                }
                return false;
            }
        }

    }
}
