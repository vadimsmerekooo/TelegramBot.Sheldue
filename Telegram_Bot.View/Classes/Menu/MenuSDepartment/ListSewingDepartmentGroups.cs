using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;
namespace Telegram_Bot.View.Classes.Menu.MenuSDepartment
{
    class ListSewingDepartmentGroups : MainMenu, IStepsForWorkFile
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
            await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
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
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Список групп швейного отделения:

{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} 05 шо
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} 30 ш
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} 29 ш 
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} 11 з
{convertEmoji = new Emoji(new int[] { 0x0035, 0x20E3 })} 05 мктт
{convertEmoji = new Emoji(new int[] { 0x0036, 0x20E3 })} 06 мктт", ParseMode.Default, false, false, 0, keyboardNumberGroupShvei);
            BotRoma.OnMessage += ButtonGroups;
        }
        public void ButtonGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            switch (message.Text)
            {
                case "05 шо":
                    DeleteMessageWithoutList(e);
                    NextStepSelectDay(sender, e, "05 шо");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "30 шо":
                    DeleteMessageWithoutList(e);
                    NextStepSelectDay(sender, e, "30 шо");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "29 ш":
                    DeleteMessageWithoutList(e);
                    NextStepSelectDay(sender, e, "29 ш");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "11 з":
                    DeleteMessageWithoutList(e);
                    NextStepSelectDay(sender, e, "11 з");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "05 мктт":
                    DeleteMessageWithoutList(e);
                    NextStepSelectDay(sender, e, "05 мктт");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "06 мктт":
                    DeleteMessageWithoutList(e);
                    NextStepSelectDay(sender, e, "06 мктт");
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
        private async void DeleteMessageWithoutList(MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
        }
    }
}
