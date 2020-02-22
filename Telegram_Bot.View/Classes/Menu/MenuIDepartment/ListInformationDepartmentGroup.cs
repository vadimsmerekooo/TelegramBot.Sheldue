using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.BL;

namespace Telegram_Bot.View.Classes.Menu.MenuIDepartment
{
    class ListInformationDepartmentGroup : MainMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public ListInformationDepartmentGroup(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }
        public async void ViewListGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Список групп информационного отделения:

{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} 27 тп
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} 29 тп
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} 30 тп
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} 31 тп
{convertEmoji = new Emoji(new int[] { 0x0035, 0x20E3 })} 32 тп
{convertEmoji = new Emoji(new int[] { 0x0036, 0x20E3 })} 33 тп
{convertEmoji = new Emoji(new int[] { 0x0037, 0x20E3 })} 06 шо");
            var keyboardNumberGroupInform = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("27 тп"),
                                                    new KeyboardButton("29 тп"),
                                                    new KeyboardButton("30 тп")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("31 тп"),
                                                    new KeyboardButton("32 тп"),
                                                    new KeyboardButton("33 тп")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("06 шо")
                                                }
                                            },
                ResizeKeyboard = true
            };
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Тыкай на кнопочку {convertEmoji = new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, keyboardNumberGroupInform);
            BotRoma.OnMessage += ButtonGroups;
        }
        public void ButtonGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            switch (message.Text)
            {
                case "27 тп":
                    BL.Classes.Student.CollectionInformationParseText collectionInform = new BL.Classes.Student.CollectionInformationParseText();
                    collectionInform.SearchShedule();
                    break;
                case "29 тп": break;
                case "30 тп": break;
                case "31 тп": break;
                case "32 тп": break;
                case "33 тп": break;
                case "06 шо": break;
                default: BotRoma.OnMessage -= ButtonGroups; break;
            }
        }
    }
}
