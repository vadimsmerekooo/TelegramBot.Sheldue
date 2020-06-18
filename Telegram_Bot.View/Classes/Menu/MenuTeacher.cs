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
        public MenuWithListTeacher(TelegramBotClient Bot, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue): base(Bot, api, ref sheldue)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
            this.sheldue = sheldue;
        }
        private SortedSet<string> listTeacher = new SortedSet<string>()
        {
            "Аблажевич И.В.",
  "Авдей И.И.",
  "Алейникова М.П.",
  "Алексейченко И.В.",
  "Анисько Р.И.",
  "Анищик Р.М.",
  "Безносик Т.В.",
  "Болтач Е.П.",
  "Бондарь Е.И.",
  "Васьков Н.М.",
  "Ващилова Н.И.",
  "Вильчик Н.М.",
  "Войшель М.С.",
  "Воронко Л.А.",
  "Гоманчук В.К.",
  "Дереченик М.А.",
  "Езепчук А.Ф.",
  "Ефимик Е.В.",
  "Закревский Ю.В.",
  "Ильющеня И.И.",
  "Карпович Т.Я.",
  "Качан Е.И.",
  "Киреня О.П.",
  "Ковальчук Е.В.",
  "Котович Н.С.",
  "Кох С.И.",
  "Кривопуст Е.Е.",
  "Кулаковская С.М.",
  "Лавкель Ю.В.",
  "Лебедь Т.М.",
  "Левицкая Л.В.",
  "Лис Н.Н.",
  "Логош Т.В.",
  "Лукошко В.И.",
  "Лупач О.И.",
  "Масько Е.Ч.",
  "Можейко Е.А.",
  "Назарчук Т.Н.",
  "Нестер В.П.",
  "Новик А.И.",
  "Отцецкая А.А.",
  "Петрякова Н.С.",
  "Пецевич И.В.",
  "Павбуть Л.А.",
  "Романович А.В.",
  "Рымашевская Н.И.",
  "Сакович О.И.",
  "Самойло Ж.В.",
  "Сколыш С.Г.",
  "Снацкая И.И.",
  "Созоненко О.Н.",
  "Сычкова О.Р.",
  "Толочко П.С.",
  "Трайгель В.В.",
  "Фахрутдинова А.Я.",
  "Чилько М.Д.",
  "Чапля Н.В.",
  "Чистобаева Н.И.",
  "Чистобаева Н.И.",
  "Швец А.С.",
  "Шершнева С.Ф.",
  "Шиманович Т.С.",
  "Шкута О.Г.",
  "Шмулькштене Е.И.",
  "Шулеева С.С.",
  "Шупикова Л.М.",
  "Юргель Е.А.",
  "Юхневич Н.И.",
  "Янушкевич И.П."
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


        public async void SendMessageMenuTeacher(object sender, MessageEventArgs e)
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
                    ListDayWeekTeacher selectDayKeyBoard = new ListDayWeekTeacher(BotRoma, ApiKeyBot, sheldue, message.Text.ToUpper());
                    selectDayKeyBoard.SendMessageListDayWeekTeacher(sender, e);
                    break;
                case false:
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Преподаватель с такой фамилией, не записан в базу🥺", ParseMode.MarkdownV2, replyMarkup: new Keyboards().Personality());
                    break;
                default:
                    BotRoma.OnMessage -= TeacherMethod;
                    break;
            }
            BotRoma.OnMessage -= TeacherMethod;
        }
    }
}
