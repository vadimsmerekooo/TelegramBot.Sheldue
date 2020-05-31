using System;
using NLog;
using Telegram.Bot;

namespace IFCore
{
    public static class IFCore
    {
        public static Logger loggerMain = LogManager.GetCurrentClassLogger();
    }
    public class IFCoreSendErrorMessage
    {
        TelegramBotClient BotRoma;
        string ApiKeyBot;
        public IFCoreSendErrorMessage(TelegramBotClient bot, string api, string errorText)
        {
            BotRoma = bot;
            ApiKeyBot = api;
            BotRoma.SendTextMessageAsync(415226650, errorText);
        }
    }
}
