using System;
using Telegram.Bot;

namespace Telegram_Bot.App
{

    public class Const
    {
        

        public static TelegramBotClient BotRoma;
        private static readonly string apiKey = "1123611805:AAGln7hVP6yo460tF6sR_UyFj2b7YHw04i4";
        public static TelegramBotClient GetsetBot { get { return BotRoma; } set { BotRoma = value; } }
        public static string GetSetApiKey { get { return apiKey; } }
    }
}
