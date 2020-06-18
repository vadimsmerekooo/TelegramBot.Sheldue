using System;
using Telegram.Bot;

namespace Telegram_Bot.App
{

    public class Const
    {

        private static string bot_reserv = "1123611805:AAGln7hVP6yo460tF6sR_UyFj2b7YHw04i4";
        private static string bot_main = "904575664:AAEBKyXlFX2IHieutGKofU_AqjP_--pimWI";

        public static TelegramBotClient BotRoma;
        private static readonly string apiKey = bot_main;
        public static TelegramBotClient GetsetBot { get { return BotRoma; } set { BotRoma = value; } }
        public static string GetSetApiKey { get { return apiKey; } }
    }
}
