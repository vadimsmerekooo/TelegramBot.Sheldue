using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using WordText = Microsoft.Office.Interop.Word;


namespace Telegram_Bot.DAL.Classes.Student
{
    public class ParseWord
    {
        WordText.Application word = new WordText.Application();
        WordText.Document doc = new WordText.Document();
        WordText.Table tbl = null;

        public string SelectGroupFile(string groupName, string day)
        {
            Thread openDocThread;
            string rowbuf = string.Empty;

            try
            {
                Object confConv = false;
                Object readOnly = true;
                Object filename = @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Information.docx";


                //openDocThread = new Thread(() => doc = word.Documents.Open(ref filename, ref confConv, ref readOnly));
                //openDocThread.Start();
                //word.Visible = true;
               // tbl = doc.Tables[1];


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                doc.Close();
                word.Quit();
                tbl = null;
            }

            rowbuf = groupName + " " + day;
            return rowbuf;
        }
        
    }
}
