using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot.View.Classes.Menu.MenuMDepartment
{
    class ListMechatronicDepartmentGroups : MainMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public ListMechatronicDepartmentGroups(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }
        public async void ListViewGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message.Text == null)
                return;
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Список групп машиностроительного отделения:
{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} 01 эс
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} 02 эс
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} 26 тм
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} 2 м
{convertEmoji = new Emoji(new int[] { 0x0035, 0x20E3 })} 1 м
{convertEmoji = new Emoji(new int[] { 0x0036, 0x20E3 })} 3 м
{convertEmoji = new Emoji(new int[] { 0x0037, 0x20E3 })} 2 от
{convertEmoji = new Emoji(new int[] { 0x0038, 0x20E3 })} 03 от");
            var keyboardNumberGroupMachine = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("01 эс"),
                                                    new KeyboardButton("02 эс"),
                                                    new KeyboardButton("26 тм"),
                                                    new KeyboardButton("2 м")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("1 м"),
                                                    new KeyboardButton("3 м"),
                                                    new KeyboardButton("2 от"),
                                                    new KeyboardButton("03 от")
                                                }
                                            },
                ResizeKeyboard = true
            };
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Тыкай на кнопочку {convertEmoji = new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, keyboardNumberGroupMachine);
        }
    }
}
