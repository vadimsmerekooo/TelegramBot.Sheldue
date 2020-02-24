using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot.View.Classes.Menu
{
    public class DeleteMessage : MainMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKey;
        public DeleteMessage(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            BotRoma = Bot;
            ApiKey = api;
        }
        public async void DeleteMessageOfMenu(Message message)
        {
            try
            {
                await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 2);
                await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
            }
            catch
            {

            }
        }
        public async void DeleteFirstMessage(Message message)
        {
            await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId + 1);
        }
    }
}
