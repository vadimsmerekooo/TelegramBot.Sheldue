using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes.Menu.MenuMDepartment
{
    class ListMechatronicDepartmentGroups : MainMenu, IStepsForWorkFile, IStepsForWorkFileInList
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public ListMechatronicDepartmentGroups(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }
        public async void ViewListGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message.Text == null)
                return;
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
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Список групп машиностроительного отделения:

{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} 01 эс
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} 02 эс
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} 26 тм
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} 2 м
{convertEmoji = new Emoji(new int[] { 0x0035, 0x20E3 })} 1 м
{convertEmoji = new Emoji(new int[] { 0x0036, 0x20E3 })} 3 м
{convertEmoji = new Emoji(new int[] { 0x0037, 0x20E3 })} 2 от
{convertEmoji = new Emoji(new int[] { 0x0038, 0x20E3 })} 03 от", ParseMode.Default, false, false, 0, keyboardNumberGroupMachine);
            BotRoma.OnMessage += ButtonGroups;
        }
        public void ButtonGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            switch (message.Text)
            {
                case "01 эс":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "01 эс");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "02 эс":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "02 эс");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "26 тм":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "26 тм");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "2 м":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "2 м");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "1 м":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "1 м");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "2 от":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "2 от");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "3 м":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "3 м");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "03 от":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "03 от");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                default: BotRoma.OnMessage -= ButtonGroups; break;
            }
        }

        public void NextStepSelectDay(object sender, MessageEventArgs e, string groupName)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            deleteMessage = new DeleteMessage(BotRoma, ApiKeyBot);
            deleteMessage.DeleteMessageOfMenu(message);
            IStepsOnMenu selectDayKeyBoard = new ListDayWeak(BotRoma, ApiKeyBot, groupName);
            selectDayKeyBoard.ListDay(sender, e);
        }
    }
}
