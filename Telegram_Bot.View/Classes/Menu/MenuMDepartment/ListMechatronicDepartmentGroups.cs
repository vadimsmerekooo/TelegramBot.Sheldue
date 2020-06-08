using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes.Menu.MenuMDepartment
{
    class ListMechatronicDepartmentGroups : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        private string department;
        Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue;
        public ListMechatronicDepartmentGroups(TelegramBotClient Bot, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue, string department) : base(Bot, api, ref sheldue)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
            this.sheldue = sheldue;
            this.department = department;
        }
        public async void SendMessageListMechDep(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message.Text == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
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

{new Emoji(new int[] { 0x0031, 0x20E3 })} 01 эс
{new Emoji(new int[] { 0x0032, 0x20E3 })} 02 эс
{new Emoji(new int[] { 0x0033, 0x20E3 })} 26 тм
{new Emoji(new int[] { 0x0034, 0x20E3 })} 2 м
{new Emoji(new int[] { 0x0035, 0x20E3 })} 1 м
{new Emoji(new int[] { 0x0036, 0x20E3 })} 3 м
{new Emoji(new int[] { 0x0037, 0x20E3 })} 2 от
{new Emoji(new int[] { 0x0038, 0x20E3 })} 03 от", ParseMode.Default, false, false, 0, keyboardNumberGroupMachine);
            BotRoma.OnMessage += ButtonGroups;
        }
        public async void ButtonGroups(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            switch (message.Text)
            {
                case "01 эс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "01 эс", department);
                    break;
                case "02 эс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "02 эс", department);
                    break;
                case "26 тм":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "26 тм", department);
                    break;
                case "2 м":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "2 м", department);
                    break;
                case "1 м":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "1 м", department);
                    break;
                case "2 от":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "2 от", department);
                    break;
                case "3 м":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "3 м", department);
                    break;
                case "03 от":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "03 от", department);
                    break;
                default: BotRoma.OnMessage -= ButtonGroups;
                     break;
            }
        }

        public void NextStepSelectDay(object sender, MessageEventArgs e, string groupName, string department)
        {
            ListDayWeak selectDayKeyBoard = new ListDayWeak(BotRoma, ApiKeyBot, groupName, sheldue);
            selectDayKeyBoard.SendMessageListDayWeek(sender, e);
        }
    }
}
