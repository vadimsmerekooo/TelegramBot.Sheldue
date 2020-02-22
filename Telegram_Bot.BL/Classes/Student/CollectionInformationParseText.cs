using System;
using Telegram_Bot.DAL.Classes;
namespace Telegram_Bot.BL.Classes.Student
{
    public class CollectionInformationParseText
    {
        public void SearchShedule()
        {
            DAL.Classes.Student.ParseWord pw = new DAL.Classes.Student.ParseWord();
            pw.SelectGroupFile();
        }
    }
}
