using Telegram.Bot;

namespace studentassistant_bot.Models
{
    public class Const
    {
        private static TelegramBotClient BotRoma;
        private static readonly string apiKey = "904575664:AAHC4zbtgASlNF1GFxde8oFTXk1kaWHhppM";
        public static TelegramBotClient GetsetBot { get { return BotRoma; } set { BotRoma = value; } }
        public static string GetSetApiKey { get { return apiKey; } }
    }
}