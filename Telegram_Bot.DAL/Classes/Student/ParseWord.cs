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
        WordText.Application word;
        WordText.Document doc;
        WordText.Table tbl;

        public string SelectGroupFile(string groupName, string day)
        {
            Thread openDocThread;
            string rowbuf = string.Empty;

            try
            {
                word = new WordText.Application();
                doc = new WordText.Document();
                tbl = null;
                Object missing = System.Reflection.Missing.Value;
                Object confConv = false;
                Object readOnly = true;
                Object isVisible = false;
                Object saveChanges = false;
                Object filename = @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Information.docx";


                //openDocThread = new Thread(() => doc = word.Documents.Open(ref filename, ref confConv, ref readOnly));
                //openDocThread.Start();
                //word.Visible = false;
                //tbl = doc.Tables[1];


            }
            catch 
            {
                
            }
            finally
            {
                if(word != null)
                {
                    doc.Close();
                    word.Quit();
                    tbl = null;
                }
            }

            rowbuf = groupName + " " + day;
            return rowbuf;
        }
        
    }
}
