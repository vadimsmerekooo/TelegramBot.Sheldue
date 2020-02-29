﻿using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.BL;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes.Menu.MenuIDepartment
{
    class ListInformationDepartmentGroup : MainMenu, IStepsForWorkFile, IStepsForWorkFileInList
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
            await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
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

{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} 27 тп
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} 29 тп
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} 30 тп
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} 31 тп
{convertEmoji = new Emoji(new int[] { 0x0035, 0x20E3 })} 32 тп
{convertEmoji = new Emoji(new int[] { 0x0036, 0x20E3 })} 33 тп
{convertEmoji = new Emoji(new int[] { 0x0037, 0x20E3 })} 06 шо",ParseMode.Default, false, false, 0, keyboardNumberGroupInform);
            
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
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "27 тп");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "29 тп":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "29 тп");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "30 тп":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "30 тп");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "31 тп":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "31 тп");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "32 тп":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "32 тп");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "33 тп":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "33 тп");
                    BotRoma.OnMessage -= ButtonGroups;
                    break;
                case "06 шо":
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    NextStepSelectDay(sender, e, "06 шо");
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