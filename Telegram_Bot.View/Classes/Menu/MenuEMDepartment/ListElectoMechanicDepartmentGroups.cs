using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;
namespace Telegram_Bot.View.Classes.Menu.MenuEMDepartment
{
    class ListElectoMechanicDepartmentGroups : MainMenu, IStepsForWorkFile, IStepsForWorkFileInList
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        private string department;
        public ListElectoMechanicDepartmentGroups(TelegramBotClient Bot, string api) : base(Bot, api)
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
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Список групп электромеханического отделения:

{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} 03 эс
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} 04 эс
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} 19 опс
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} 20 опс
{convertEmoji = new Emoji(new int[] { 0x0035, 0x20E3 })} 18 опс
{convertEmoji = new Emoji(new int[] { 0x0036, 0x20E3 })} 10 эо
{convertEmoji = new Emoji(new int[] { 0x0037, 0x20E3 })} 11 эо", ParseMode.Default, false, false, 0, keyboardNumberGroupMechan);
            BotRoma.OnMessage += ButtonGroups;
        }
        public async void ButtonGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            switch (message.Text)
            {
                case "03 эс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "03 эс", department);
                    break;
                case "04 эс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "04 эс", department);
                    break;
                case "19 опс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "19 опс", department);
                    break;
                case "20 опс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "20 опс", department);
                    break;
                case "18 опс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "18 опс", department);
                    break;
                case "10 эо":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "19 эо", department);
                    break;
                case "11 эо":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "11 эо", department);
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
