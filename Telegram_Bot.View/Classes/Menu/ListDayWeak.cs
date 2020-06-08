using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;
using Telegram_Bot.BL.Classes.Student;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;

namespace Telegram_Bot.View.Classes.Menu
{
    class ListDayWeak : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        private string groupName;
        private Keyboards keyboard = new Keyboards();
        Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue;
        public ListDayWeak(TelegramBotClient Bot, string api, string group, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue) : base(Bot, api, ref sheldue)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
            groupName = group;
            this.sheldue = sheldue;
        }

        public async void SendMessageListDayWeek(object sender, MessageEventArgs e)
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
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери день {new Emoji(0x2B07)}", ParseMode.Default, true, true, 0, keyboardDays);
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
                    NextStepParseFile(sender, e, "понедельник");
                    break;
                case "вт":
                    NextStepParseFile(sender, e, "вторник");
                    break;
                case "ср":
                    NextStepParseFile(sender, e, "среда");
                    break;
                case "чт":
                    NextStepParseFile(sender, e, "четверг");
                    break;
                case "пт":
                    NextStepParseFile(sender, e, "пятница");
                    break;
                case "сб":
                    NextStepParseFile(sender, e, "суббота");
                    break;
                case "на сегодня":
                    var dayToday = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                    NextStepParseFile(sender, e, dayToday.ToLower());
                    break;
                case "на завтра":
                    var dayTodays = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                    var dayTomorow = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(1).DayOfWeek);
                    if(dayTodays == "суббота")
                    {
                        dayTomorow = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.AddDays(3).DayOfWeek);
                    }
                    NextStepParseFile(sender, e, dayTomorow.ToLower());
                    break;
                default:
                    BotRoma.OnMessage -= SelectDay;
                    break;
            }
        }

        public async void NextStepParseFile(object sender, MessageEventArgs e, string day)
        {
            var message = e.Message;
            try { await BotRoma.SendTextMessageAsync(message.Chat.Id, @"Загрузка..."); }catch { }
            // Вызываем метод для получения расписания на выбранный день
            string parseTextWithoutWordFile = SerachShldueForUser(day);
            try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId + 1); } catch { }

            try { await BotRoma.SendTextMessageAsync(message.Chat.Id, parseTextWithoutWordFile, replyMarkup: keyboard.Personality()); } catch { }
        }

        private string SerachShldueForUser(string day)
        {
            try
            {
                // Проходимся по всему полученному расписанию
                foreach (var item in sheldue)
                {
                    if (item.Key == "ГРУППА " + groupName.Replace(" ", ""))
                    {
                        // Проходимся по расписанию группы
                        foreach (var itemSheldue in item.Value)
                        {
                            if (itemSheldue.DayName.ToLower() == day.ToLower())
                            {
                                // Проходимся по расписанию во дне 
                                foreach (var itemSheldueDay in itemSheldue.Day)
                                {
                                    // Составляем расписание с помощью метода ListParaToString


                                    /*
                                     * Если одной из пар не будет, проблеов в выводе сообщения не будет
                                     * Загоняем все пары в сообщение
                                     * В конце вызываем метод => Weather
                                     * получем погоду
                                     */
                                    return $@"Твое, расписание, на {day}📚
Неделя: {MainMenu.Week}

Замены к расписанию:
{itemSheldueDay?.ChangeSheldue} -
Основное расписание:
{ListParaToString(itemSheldueDay.Para1)}
{ListParaToString(itemSheldueDay.Para2)}
{ListParaToString(itemSheldueDay.Para3)}
{ListParaToString(itemSheldueDay.Para4)}
{ListParaToString(itemSheldueDay.Para5)}
Погода: {new Weather().GetInfoAboutWeather()}";
                                }
                            }
                        }
                    }
                }
                string textError = $"Уровень: PL; Метод: SerachShldueForUser; Пустой список с расписанием!";
                IFCore.IFCore.loggerMain.Error(textError);
                new IFCore.IFCoreSendErrorMessage(BotRoma, ApiKeyBot, textError);
                return $@"Список с расписанием пуст😱! Обратись к разработчику, он все пофиксит👨‍🔧!";
            }
            catch (Exception ex)
            {
                int lineEx = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                string textError = $"Уровень: PL; Метод: SerachShldueForUser; Строка: {lineEx}; Параметры: группа - {groupName}, день - {day}";
                IFCore.IFCore.loggerMain.Error(textError);
                new IFCore.IFCoreSendErrorMessage(BotRoma, ApiKeyBot, textError);
                return $@"Произошла ошибка😱!
Без паники📛! Я вызвал фиксика - Вадю😎! Он, скоро все починит👨‍🔧!
Попробуй заново🔃";
            }
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
                    try
                    {
                        newText += word[0].ToString().ToUpper();
                        if (word[word.Length - 1].ToString() == "/")
                        {
                            newText += " / ";
                        }
                    }
                    catch
                    {
                        continue;
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
