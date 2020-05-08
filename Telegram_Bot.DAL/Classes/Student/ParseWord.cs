using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using WordText = Microsoft.Office.Interop.Word;
using NLog;
using System.Web;
using Telegram.Bot;
using System.Net;
using System.IO;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace Telegram_Bot.DAL.Classes.Student
{
    public class ParseWord : MainClassDAL
    {

        private TelegramBotClient BotRoma;
        private string ApiKeyBot;

        public ParseWord(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }

        WordText.Application word = new WordText.Application();
        WordText.Document doc = new WordText.Document();
        WordText.Table tbl;
        Logger logger = LogManager.GetCurrentClassLogger();



        public string SelectGroupFile(string groupName, string day, string department)
        {
            Thread openDocThread;
            string rowbuf = string.Empty;

            try
            {
                Object missing = System.Reflection.Missing.Value;
                Object confConv = false;
                Object readOnly = true;
                Object isVisible = false;
                Object saveChanges = false;
                Dictionary<string, string> departmnetListReferensToFile = new Dictionary<string, string>();
                departmnetListReferensToFile.Add("Информационное", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Information.docx");
                departmnetListReferensToFile.Add("Швейное", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Sewing.docx");
                departmnetListReferensToFile.Add("Электромеханическое", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\ElektroMechanic.docx");
                departmnetListReferensToFile.Add("Машиностроения", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Mechanic.docx");
                Object filename = null;
                foreach (var departmentList in departmnetListReferensToFile)
                    if (departmentList.Key == department)
                        filename = departmentList.Value;

                doc = word.Documents.Open(ref filename, ref confConv, ref readOnly);
                tbl = doc.Tables[0];
                try
                {
                    for (int i = 1; i < tbl.Rows.Count; i++)
                    {
                        for (int j = 1; j < tbl.Columns.Count; j++)
                        {
                            try
                            {
                                if (tbl.Cell(i, j).Range.Text != "\r\a")
                                {
                                    string row = tbl.Cell(i, j).Range.Text;
                                    row = row.Replace("\r\a", "");
                                    if (row == "ГРУППА " + groupName.Replace(" ", "") || row == "ГРУППА " + groupName)
                                        SearchDateCell(day);
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
                catch
                {

                }
                return rowbuf = "213";
            }
            catch
            {
                string textError = $"Уровень: DAL; Метод: ParseText; Данные, при которых метод выдал ошибку: группа - {groupName}, отделение - {department}, день - {day}";
                logger.Error(textError);
                BotRoma.SendTextMessageAsync("415226650", textError);
                return $@"Произошла ошибка 😱!
Без паники 📛! Я вызвал фиксика - Вадю 😎! Он, скоро все починит 👨‍🔧!
Можешь попробывать снова 🔃";
            }
            finally
            {
                if (word != null)
                {
                    doc.Close();
                    word.Quit();
                }
            }

        }
        //Step 2
        public void SearchDateCell(string day)
        {
            for (int i = 1; i < tbl.Rows.Count; i++)
            {
                for (int j = 1; j < 2; j++)
                {
                    string row = tbl.Cell(i, j).Range.Text;
                    row = row.Replace("\r\a", "");
                    if (day == row)
                        CellWithCheldue(1, 1, 1);
                }
            }
        }
        //Step 3
        public string CellWithCheldue(int ii, int jj, int numberTableInFile)
        {
            string s = string.Empty;
            for (int i = ii; i < doc.Tables[1].Rows.Count; i++)
            {
                for (int j = jj; j < doc.Tables[1].Columns.Count; j++)
                {
                    s = doc.Tables[1].Cell(i, j).Range.Text;
                    s = s.Replace("\r\a", string.Empty);
                }
            }
            return s;
        }
    }
}
