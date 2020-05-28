using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Classes.Menu;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes.Teacher
{
    class MenuWithListTeacher : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue; 
        public MenuWithListTeacher(TelegramBotClient Bot, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue): base(Bot, api, sheldue)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
            this.sheldue = sheldue;
        }
        private SortedSet<string> listTeacher = new SortedSet<string>()
        {
            "Толочко П.С.", "Киреня О.П.",
            "Воронко Л.А.",
            "Равбуть Л.А.", "Масько Е.Ч.",
            "Анисько Р.И.",
            "Котович Н.С.", "Назарчук Т.Н.",
            "Шмулькштене Е.И.",
            "Лис Н.Н.", "Петрякова Н.С.",
            "Воропай О.А.", "Алексейченко И.В.",
            "Новик А.И.", "Можейко Е.А.",
            "Карпович Т.Я.", "Васьков Н.М.",
            "Анищик Р.М.", "Карпович Т.Я.",
            "Дереченик М.А.", "Кривопуст Е.Е.",
            "Качан Е.И.", "Авдей И.И.",
            "Лебедь Т.М.", "Левицкая Л.В.",
            "Лавекль Ю.В.", "Романович А.В.",
            "Шиманович Т.С.", "Воропай О.А.",
            "Шкута О.Г.", "Лис Н.Н.",
            "Новицкая Ю.В.", "Сакович О.И.", "Ильющеня П.И.",
            "Аблажевич И.В.", "Гоманчук В.К.",
            "Юхневич Н.И.", "Трайгель В.В.",
            "Самойло Ж.В.", "Гоманчук В.К.",
            "Чистобаева Н.И.", "Снацкая И.И.", "Лупач О.И."
        };

        //        public async void ViewListWithTeacher(object sender, MessageEventArgs e)
        //        {
        //            Student.MenuStudent ms = new Student.MenuStudent(BotRoma, ApiKeyBot);
        //            BotRoma.OnMessage -= ms.Frog;
        //            var message = e.Message;
        //            if (message.Type != MessageType.Text || message == null)
        //                return;
        //            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"*Список преподавателей:*
        //{convertEmoji = new Emoji(0x2139)} _На данный момент некоторые преподаватели не доступны_

        //{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} Толочко П.С.
        //{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} Киреня О.П.", ParseMode.MarkdownV2);      
        //            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Вводи цифру {convertEmoji = new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, new ReplyKeyboardRemove());
        //        }1


        public override async void SendMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"*👀 Введите свою Фамилию*
Пример: Иванов

{new Emoji(0x2139)} _На данный момент, некоторые преподаватели, не доступны_", ParseMode.MarkdownV2);
            BotRoma.OnMessage += TeacherMethod;
        }


        private async void TeacherMethod(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1); } catch { }
            bool checkUserInList = false;
            foreach (var item in listTeacher)
            {
                var itemSplit = item.Split(' ');
                if (itemSplit[0].ToUpper() == message.Text.ToUpper())
                {
                    checkUserInList = true;
                }
            }
            switch (checkUserInList)
            {
                case true:
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Препод найден", ParseMode.MarkdownV2, replyMarkup: new Keyboards().Personality());
                    break;
                case false:
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Преподаватель с такой фамилией, не записан в базу🥺", ParseMode.MarkdownV2);
                    break;
                default:
                    BotRoma.OnMessage -= TeacherMethod;
                    break;
            }
        }
    }
}
