using System;
using Telegram.Bot;

namespace Telegram_Bot.App
{

    public class Const
    {
        

        public static TelegramBotClient BotRoma;
        private static readonly string apiKey = "";
        public static TelegramBotClient GetsetBot { get { return BotRoma; } set { BotRoma = value; } }
        public static string GetSetApiKey { get { return apiKey; } }
    }
}
