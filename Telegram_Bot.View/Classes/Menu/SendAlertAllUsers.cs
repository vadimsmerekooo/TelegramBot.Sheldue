using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes.Menu
{
    public class SendAlertAllUsers: MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKey;
        private List<int> messageChatIdClients;
        public SendAlertAllUsers(TelegramBotClient bot, string api, List<int> messageChatIdClients, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue) : base(bot, api, ref sheldue)
        {
            BotRoma = bot;
            ApiKey = api;
            this.messageChatIdClients = messageChatIdClients;
        }
        public async void AlertMessage(string alertMessage)
        {
            foreach (int id in messageChatIdClients)
            {
                await BotRoma.SendTextMessageAsync(id, alertMessage);
            }
        }
        public async void AlertMessage(string alertMessage, string referenceImage)
        {
            foreach (int id in messageChatIdClients)
            {
                await BotRoma.SendTextMessageAsync(id, $"[{alertMessage}]({referenceImage})", ParseMode.Markdown);
            }
        }
    }
}
