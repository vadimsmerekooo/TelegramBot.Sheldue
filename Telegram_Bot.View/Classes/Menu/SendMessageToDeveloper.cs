using System;
using System.Collections.Generic;
using IFCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram_Bot.View.Interface;
using Telegram.Bot.Types.Enums;

namespace Telegram_Bot.View.Classes.Menu
{
    public class SendMessageToDeveloper : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public SendMessageToDeveloper(TelegramBotClient Bot, string api, ref Dictionary<string, List<SheldueAllDaysTelegram>> sheldue) : base(Bot, api, ref sheldue)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
        }
        public override async void SendMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            await BotRoma.SendTextMessageAsync(message.Chat.Id, "⚠ВНИМАНИЕ!⚠ Сообщение не должно содержать нецензурную брань❗, или оскорбления в адрес разработчика❗ Если одно из условий будет нарушено⛔, вы автоматически попадаете в черный список❌! Введите сообщение⬇");
            BotRoma.OnMessage += SendMessageToDev;
        }

        private async void SendMessageToDev(object sender, MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.Text || e.Message == null)
            {
                await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            await BotRoma.SendTextMessageAsync(415226650, "Сообщение от пользователя: " + e.Message.Text + $" От: {e.Message.Chat.Id} - {e.Message.Chat.FirstName}");
            await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, "Сообщение отправлено ✔");
            BotRoma.OnMessage -= SendMessageToDev;
        }
    }
}
