using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.BL;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes.Menu.MenuIDepartment
{
    class ListInformationDepartmentGroup : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        private string department;
        Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue;
        public ListInformationDepartmentGroup(TelegramBotClient Bot, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue, string department) : base(Bot, api, ref sheldue)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
            this.sheldue = sheldue;
            this.department = department;
        }
        public async void SendMessageListInformDev(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
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
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Список групп информационного отделения:

{new Emoji(new int[] { 0x0031, 0x20E3 })} 27 тп
{new Emoji(new int[] { 0x0032, 0x20E3 })} 29 тп
{new Emoji(new int[] { 0x0033, 0x20E3 })} 30 тп
{new Emoji(new int[] { 0x0034, 0x20E3 })} 31 тп
{new Emoji(new int[] { 0x0035, 0x20E3 })} 32 тп
{new Emoji(new int[] { 0x0036, 0x20E3 })} 33 тп
{new Emoji(new int[] { 0x0037, 0x20E3 })} 06 шо",ParseMode.Default, false, false, 0, keyboardNumberGroupInform);
            
            BotRoma.OnMessage += ButtonGroups1;
        }
        public async void ButtonGroups1(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            switch (message.Text)
            {
                case "27 тп":
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
                    NextStepSelectDay(sender, e, "27 тп");
                    break;
                case "29 тп":
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
                    NextStepSelectDay(sender, e, "29 тп");
                    break;
                case "30 тп":
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
                    NextStepSelectDay(sender, e, "30 тп");
                    break;
                case "31 тп":
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
                    NextStepSelectDay(sender, e, "31 тп");
                    break;
                case "32 тп":
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
                    NextStepSelectDay(sender, e, "32 тп");
                    break;
                case "33 тп":
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
                    NextStepSelectDay(sender, e, "33 тп");
                    break;
                case "06 шо":
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
                    NextStepSelectDay(sender, e, "06 шо");
                    break;
                default: BotRoma.OnMessage -= this.ButtonGroups1;
                    break;
            }
        }

        public void NextStepSelectDay(object sender, MessageEventArgs e, string groupName)
        {
            ListDayWeak selectDayKeyBoard = new ListDayWeak(BotRoma, ApiKeyBot, groupName, sheldue);
            selectDayKeyBoard.SendMessageListDayWeek(sender, e);
        }
    }
}
