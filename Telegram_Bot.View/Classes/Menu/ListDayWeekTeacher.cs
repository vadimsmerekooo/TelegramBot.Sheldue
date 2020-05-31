using IFCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes.Menu
{
    public class ListDayWeekTeacher : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        private string surname;
        Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue;
        public ListDayWeekTeacher(TelegramBotClient Bot, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue, string surname) : base(Bot, api, ref sheldue)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
            this.sheldue = sheldue;
            this.surname = surname;
        }

        public override async void SendMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 2); } catch { }
            var keyboardDays = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("На сегодня"),
                                                    new KeyboardButton("На завтра")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("Пн"),
                                                    new KeyboardButton("Вт"),
                                                    new KeyboardButton("Ср")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("Чт"),
                                                    new KeyboardButton("Пт"),
                                                    new KeyboardButton("Сб")
                                                }
                                            },
                ResizeKeyboard = true
            };
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выберите день {new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, keyboardDays);
            BotRoma.OnMessage += SelectDay;
        }
        public async void SelectDay(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1); } catch { }
            try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
            switch (message.Text.ToLower())
            {
                case "пн":
                    NextStepParseFile(sender, e, "понедельник", surname);
                    break;
                case "вт":
                    NextStepParseFile(sender, e, "вторник", surname);
                    break;
                case "ср":
                    NextStepParseFile(sender, e, "среда", surname);
                    break;
                case "чт":
                    NextStepParseFile(sender, e, "четверг", surname);
                    break;
                case "пт":
                    NextStepParseFile(sender, e, "пятница", surname);
                    break;
                case "сб":
                    NextStepParseFile(sender, e, "суббота", surname);
                    break;
                case "на сегодня":
                    var dayToday = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                    NextStepParseFile(sender, e, dayToday.ToLower(), surname);
                    break;
                case "на завтра":
                    var dayTomorow = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(1).DayOfWeek);
                    if (dayTomorow == "суббота")
                    {
                        dayTomorow = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(2).DayOfWeek);
                    }
                    NextStepParseFile(sender, e, dayTomorow.ToLower(), surname);
                    break;
                default:
                    BotRoma.OnMessage -= SelectDay;
                    break;
            }
        }

        public async void NextStepParseFile(object sender, MessageEventArgs e, string day, string surname)
        {
            var message = e.Message;
            await BotRoma.SendTextMessageAsync(message.Chat.Id, @"Заргузка...");
            // Вызываем метод для получения расписания на выбранный день
            string parseTextWithoutWordFile = SearchSheldueForTeacher(day);
            try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId + 1); } catch { }
            await BotRoma.SendTextMessageAsync(message.Chat.Id, parseTextWithoutWordFile, replyMarkup: new Keyboards().Personality());
        }


        private string SearchSheldueForTeacher(string day)
        {
            string changesSheldueString = string.Empty;
            var para1 = new SheldueAllListTelegram().Para1;
            string para1Group = string.Empty;
            var para2 = new SheldueAllListTelegram().Para2;
            string para2Group = string.Empty;
            var para3 = new SheldueAllListTelegram().Para3;
            string para3Group = string.Empty;
            var para4 = new SheldueAllListTelegram().Para4;
            string para4Group = string.Empty;
            var para5 = new SheldueAllListTelegram().Para5;
            string para5Group = string.Empty;
            foreach (var itemSheldue in sheldue)
            {
                foreach (var itemSheldueValue in itemSheldue.Value)
                {
                    if(itemSheldueValue.DayName.ToLower() == day.ToLower())
                    {
                        foreach (var itemSheldueDay in itemSheldueValue.Day)
                        {
                            if(itemSheldueDay.ChangeSheldue != null)
                            {
                                var splitChangeSheldue = itemSheldueDay.ChangeSheldue.Split('\r');
                                foreach (var itemSplit in splitChangeSheldue)
                                {
                                    itemSplit.Replace('\a', ' ');
                                    if (itemSplit.Contains(surname.ToUpper()))
                                    {
                                        changesSheldueString += itemSplit;
                                    }
                                }
                            }
                            if (itemSheldueDay.Para1 != null && itemSheldueDay.Para1[0] != null && itemSheldueDay.Para1[0].Teacher.ToUpper().Contains(surname.ToUpper()))
                            {
                                para1 = itemSheldueDay?.Para1;
                                para1Group = itemSheldue.Key;
                            }
                            if (itemSheldueDay.Para2 != null && itemSheldueDay.Para2[0] != null && itemSheldueDay.Para2[0].Teacher.ToUpper().Contains(surname.ToUpper()))
                            {
                                para2 = itemSheldueDay?.Para2;
                                para2Group = itemSheldue.Key;
                            }
                            if (itemSheldueDay.Para3 != null && itemSheldueDay.Para3[0] != null && itemSheldueDay.Para3[0].Teacher.ToUpper().Contains(surname.ToUpper()))
                            {
                                para3 = itemSheldueDay?.Para3;
                                para3Group = itemSheldue.Key;
                            }
                            if (itemSheldueDay.Para4 != null && itemSheldueDay.Para4[0] != null && itemSheldueDay.Para4[0].Teacher.ToUpper().Contains(surname.ToUpper()))
                            {
                                para4 = itemSheldueDay?.Para4;
                                para4Group = itemSheldue.Key;
                            }
                            if (itemSheldueDay.Para5 != null && itemSheldueDay.Para5[0] != null && itemSheldueDay.Para5[0].Teacher.ToUpper().Contains(surname.ToUpper()))
                            {
                                para5 = itemSheldueDay?.Para5;
                                para5Group = itemSheldue.Key;
                            }
                        }
                    }
                }
            }

            return $@"Ваше, расписание, на {day}📚
Неделя: {MainMenu.week}

Замены к расписанию:
{changesSheldueString} -
Основное расписание:
{ListParaToString(para1)} {para1Group}
{ListParaToString(para2)} {para2Group}
{ListParaToString(para3)} {para3Group}
{ListParaToString(para4)} {para4Group}
{ListParaToString(para5)} {para5Group}
Погода: {new Weather().GetInfoAboutWeather()}";
        }

        private string ListParaToString(List<IFCore.SheldueTelegram> para)
        {
            if (para == null)
                return string.Empty;
            else
                //  Возвращаем номер пары, название урока и аудиторию
                // LongStringInShort метод для укорачивания названия урока
                return $"{para[0]?.Para}. {LongStringInShort(para[0]?.Work)} {para[0]?.Auditorya}";
        }
        private string LongStringInShort(string text)
        {
            // Если название длиннее 25 символов, укарачиваем строку
            if (text?.Length > 25)
            {
                string newText = string.Empty;
                string[] splitText = text.Split(' ');
                foreach (var word in splitText)
                {
                    if (word != string.Empty)
                    {
                        newText += word[0].ToString().ToUpper();
                        if (word[word.Length - 1].ToString() == "/")
                        {
                            newText += " / ";
                        }
                    }
                }
                return newText;
            }
            else
            {
                return text;
            }
        }
    }
}
