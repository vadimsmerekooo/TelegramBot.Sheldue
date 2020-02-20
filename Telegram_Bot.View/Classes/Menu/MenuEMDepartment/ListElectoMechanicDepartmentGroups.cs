using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot.View.Classes.Menu.MenuEMDepartment
{
    class ListElectoMechanicDepartmentGroups : MainMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public ListElectoMechanicDepartmentGroups(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }
        public async void ViewListGroups(object sender, MessageEventArgs e)
        {
            BotRoma.StartReceiving();
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Список групп электромеханического отделения:
{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} 03 эс
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} 04 эс
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} 19 опс
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} 20 опс
{convertEmoji = new Emoji(new int[] { 0x0035, 0x20E3 })} 18 опс
{convertEmoji = new Emoji(new int[] { 0x0036, 0x20E3 })} 10 эо
{convertEmoji = new Emoji(new int[] { 0x0037, 0x20E3 })} 11 эо");
            var keyboardNumberGroupMechan = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("03 эс"),
                                                    new KeyboardButton("04 эс"),
                                                    new KeyboardButton("19 опс")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("20 опс"),
                                                    new KeyboardButton("18 опс"),
                                                    new KeyboardButton("10 эо")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("11 эо")
                                                }
                                            },
                ResizeKeyboard = true
            };
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Тыкай на кнопочку {convertEmoji = new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, keyboardNumberGroupMechan);
           
        }
    }
}
