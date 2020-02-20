using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot.View.Classes.Menu.MenuSDepartment
{
    class ListSewingDepartmentGroups : MainMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public ListSewingDepartmentGroups(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }
        public async void ViewListGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Список групп швейного отделения:
{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} 05 шо
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} 30 ш
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} 29 ш 
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} 11 з
{convertEmoji = new Emoji(new int[] { 0x0035, 0x20E3 })} 05 мктт
{convertEmoji = new Emoji(new int[] { 0x0036, 0x20E3 })} 06 мктт");
            var keyboardNumberGroupShvei = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("05 шо"),
                                                    new KeyboardButton("30 ш"),
                                                    new KeyboardButton("29 ш")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("11 з"),
                                                    new KeyboardButton("05 мктт"),
                                                    new KeyboardButton("06 мктт")
                                                }
                                            },
                ResizeKeyboard = true
            };
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Тыкай на кнопочку {convertEmoji = new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, keyboardNumberGroupShvei);
        }
    }
}
