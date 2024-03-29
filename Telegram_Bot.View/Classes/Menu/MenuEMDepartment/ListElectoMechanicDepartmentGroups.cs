﻿using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;
namespace Telegram_Bot.View.Classes.Menu.MenuEMDepartment
{
    class ListElectoMechanicDepartmentGroups : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue;
        public ListElectoMechanicDepartmentGroups(TelegramBotClient Bot, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue) : base(Bot, api, ref sheldue)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
            this.sheldue = sheldue;
        }
        public async void SendMessageListElectDep(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
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

{new Emoji(new int[] { 0x0031, 0x20E3 })} 03 эс
{new Emoji(new int[] { 0x0032, 0x20E3 })} 04 эс
{new Emoji(new int[] { 0x0033, 0x20E3 })} 19 опс
{new Emoji(new int[] { 0x0034, 0x20E3 })} 20 опс
{new Emoji(new int[] { 0x0035, 0x20E3 })} 18 опс
{new Emoji(new int[] { 0x0036, 0x20E3 })} 10 эо
{new Emoji(new int[] { 0x0037, 0x20E3 })} 11 эо", ParseMode.Default, false, false, 0, keyboardNumberGroupMechan);
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
                case "03 эс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "03 эс");
                    break;
                case "04 эс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "04 эс");
                    break;
                case "19 опс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "19 опс");
                    break;
                case "20 опс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "20 опс");
                    break;
                case "18 опс":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "18 опс");
                    break;
                case "10 эо":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "19 эо");
                    break;
                case "11 эо":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepSelectDay(sender, e, "11 эо");
                    break;
                default: BotRoma.OnMessage -= ButtonGroups;
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
