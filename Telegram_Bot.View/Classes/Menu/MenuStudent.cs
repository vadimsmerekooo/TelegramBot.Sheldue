using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram_Bot.View.Classes.Menu;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;
using System.Collections.Generic;

namespace Telegram_Bot.View.Classes.Student
{
    class MenuStudent : MainMenu, IMenu
    {
        List<string> listGroups = new List<string>()
        {
            "27 тп", "29 тп", "30 тп", "31 тп", "32 тп", "33 тп", "06 шо",
            "05 шо", "30 шо", "29 шо", "11 з", "05 мктт", "06 мктт",
            "03 эс", "04 эс", "19 опс", "20 опс", "18 опс", "10 эо", "11 эо",
            "01 эс", "02 эс", "26 тм", "2 м", "1 м", "3 м", "2 от", "03 от"
        };
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue;
        public MenuStudent(TelegramBotClient Bot, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue) : base(Bot, api, ref sheldue)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
            this.sheldue = sheldue;
        }

        public async void SendMessagemenuStudent(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            //var keyboardGroups = new ReplyKeyboardMarkup
            //{
            //    Keyboard = new[] {
            //                                    new[]
            //                                    {
            //                                        new KeyboardButton("1"),
            //                                        //new KeyboardButton("2"),
            //                                        //new KeyboardButton("3"),
            //                                        //new KeyboardButton("4")
            //                                    }
            //                                },
            //    ResizeKeyboard = true
            //};

            //    //            { convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })}
            //    //            Швейное отделение
            //    //{ convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })}
            //    //            Электромеханическое отделение
            //    //{ convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })}
            //    //            Отделение машиностроения

            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"*Введите группу*
Пример: 27 тп, 27 ТП \(ВНИМАНИЕ\! С ПРОБЕЛОМ\!\)", ParseMode.MarkdownV2, true, true, 0);   //replyMarkup: new ReplyKeyboardRemove()

            BotRoma.OnMessage += Frog;
        }
        public async void Frog(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1); } catch { }
            if (listGroups.Contains(message.Text.ToLower()))
                new ListDayWeak(BotRoma, ApiKeyBot, message.Text.ToLower(), sheldue).SendMessageListDayWeek(sender, e);
            else
            {
                try { await BotRoma.SendTextMessageAsync(message.Chat.Id, "Группа не найдена в списке 🙀! Если вы уверены, что такая группа есть, напишите разработчику ✉!"); } catch { }
            }
            BotRoma.OnMessage -= Frog;
            //switch (message.Text)
            //{
            //    case "1":
            //        new Menu.MenuIDepartment.ListInformationDepartmentGroup(BotRoma, ApiKeyBot, sheldue, "Информационное").SendMessageListInformDev(sender, e);
            //        break;
            //    //case "2":
            //    //    IStepsForWorkFile lsd = new Menu.MenuSDepartment.ListSewingDepartmentGroups(BotRoma, ApiKeyBot);
            //    //    lsd.ViewListGroups(sender, e, "Швейное");
            //    //    break;
            //    //case "3":
            //    //    IStepsForWorkFile lemd = new Menu.MenuEMDepartment.ListElectoMechanicDepartmentGroups(BotRoma, ApiKeyBot);
            //    //    lemd.ViewListGroups(sender, e, "Электромеханическое");
            //    //    break;
            //    //case "4":
            //    //    IStepsForWorkFile lmd = new Menu.MenuMDepartment.ListMechatronicDepartmentGroups(BotRoma, ApiKeyBot);
            //    //    lmd.ViewListGroups(sender, e, "Машиностроения");
            //    //    break;
            //    default: BotRoma.OnMessage -= Frog;
            //        break;
            //}
        }
    }
}
