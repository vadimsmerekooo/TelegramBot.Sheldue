using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Classes.Menu;

namespace Telegram_Bot.View.Classes.Teacher
{
    class MenuWithListTeacher : MainMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;

        public MenuWithListTeacher(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }

        public async void ViewListWithTeacher(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"*Список преподавателей:*
{convertEmoji = new Emoji(0x2139)} _На данный момент некоторые преподаватели не доступны_

{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} Толочко П\.С\.
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} Киреня О\.П\.", ParseMode.MarkdownV2);      
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Вводи цифру {convertEmoji = new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, new ReplyKeyboardRemove());
        }
        
    }
}
