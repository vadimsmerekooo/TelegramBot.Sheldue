using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram_Bot.DAL.Classes;
using Telegram_Bot.DAL.Classes.Student;

namespace Telegram_Bot.BL.Classes.Student
{
    public class CollectionInformationParseText
    {
        public string SearchShedule(string groupName, string day)
        {
            ParseWord pw = new ParseWord();
            return pw.SelectGroupFile(groupName, day);
        }
    }
}
