using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;
namespace Telegram_Bot.View.Classes.Menu.MenuSDepartment
{
    class ListSewingDepartmentGroups : MainMenu, IStepsForWorkFile, IStepsForWorkFileInList
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        private string department;
        public ListSewingDepartmentGroups(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }

        public async void ViewListGroups(object sender, MessageEventArgs e, string department)
        {
            this.department = department;
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
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
        public async void ButtonGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            switch (message.Text)
            {
                case "05 шо":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "05 шо", department);
                    break;
                case "30 ш":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "30 шо", department);
                    break;
                case "29 ш":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "29 ш", department);
                    break;
                case "11 з":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "11 з", department);
                    break;
                case "05 мктт":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "05 мктт", department);
                    break;
                case "06 мктт":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "06 мктт", department);
                    break;
                default: BotRoma.OnMessage -= ButtonGroups;
                    break;
            }
        }
        public void NextStepSelectDay(object sender, MessageEventArgs e, string groupName, string department)
        {
            IStepsOnMenu selectDayKeyBoard = new ListDayWeak(BotRoma, ApiKeyBot, groupName);
            selectDayKeyBoard.ListDay(sender, e, department);
        }
    }
}
