using System;
using Telegram.Bot;

namespace Telegram_Bot.DAL
{
    public class MainClassDAL
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;

        public MainClassDAL(TelegramBotClient Bot, string api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }
    }
}
