using Telegram_Bot.DAL.Classes.Student;
using Telegram.Bot;

namespace Telegram_Bot.BL.Classes.Student
{
    public class CollectionInformationParseText
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        

        public CollectionInformationParseText(TelegramBotClient Bot, string api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }

        public string SearchShedule(string groupName, string day, string department)
        {
            ParseWord pw = new ParseWord(BotRoma, ApiKeyBot, null);
            return pw.SelectGroupFile(groupName, day, department);
        }
    }
}
