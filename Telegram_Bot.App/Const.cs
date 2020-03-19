using System;
using Telegram.Bot;

namespace Telegram_Bot.App
{

    public class Const
    {
        private static TelegramBotClient BotRoma;
        private static readonly string apiKey = "youapikey";
        public static TelegramBotClient GetsetBot { get { return BotRoma; } set { BotRoma = value; } }
        public static string GetSetApiKey { get { return apiKey; } }
    }
}
