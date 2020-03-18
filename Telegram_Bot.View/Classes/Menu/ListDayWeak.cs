using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;
using Telegram_Bot.BL.Classes.Student;
using System.Threading.Tasks;

namespace Telegram_Bot.View.Classes.Menu
{
    class ListDayWeak : MainMenu, IStepsOnMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        private string groupName;
        private string department;
        private Keyboards keyboard = new Keyboards();
        public ListDayWeak(TelegramBotClient Bot, string api, string group) : base(Bot, api)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
            groupName = group;
        }

        public async void ListDay(object sender, MessageEventArgs e, string department)
        {
            this.department = department;
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 2);
            var keyboardDays = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("Понедельник"),
                                                    new KeyboardButton("Вторник"),
                                                    new KeyboardButton("Среда")
                                                },
                                                new[]
                                                {
                                                    new KeyboardButton("Четверг"),
                                                    new KeyboardButton("Пятница"),
                                                    new KeyboardButton("Суббота")
                                                }
                                            },
                ResizeKeyboard = true
            };
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери день {convertEmoji = new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, keyboardDays);
            BotRoma.OnMessage += SelectDay;
        }
        public async void SelectDay(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
            await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
            switch (message.Text.ToLower())
            {
                case "понедельник":
                    NextStepParseFile(sender, e, "понедельник", department);
                    break;
                case "вторник":
                    NextStepParseFile(sender, e, "вторник", department);
                    break;
                case "среда":
                    NextStepParseFile(sender, e, "среда", department);
                    break;
                case "четверг":
                    NextStepParseFile(sender, e, "четверг", department);
                    break;
                case "пятница":
                    NextStepParseFile(sender, e, "пятница", department);
                    break;
                case "суббота":
                    NextStepParseFile(sender, e, "суббота", department);
                    break;
                default: BotRoma.OnMessage -= SelectDay;
                    break;
            }
        }

        public async void NextStepParseFile(object sender, MessageEventArgs e, string day, string department)
        {
            var message = e.Message;
            await BotRoma.SendTextMessageAsync(message.Chat.Id, @"Заргузка...");
            CollectionInformationParseText collectionInform = new CollectionInformationParseText(BotRoma, ApiKeyBot);
            string parseTextWithoutWordFile = collectionInform.SearchShedule(groupName, day, department);
            await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId + 1);
            
            await BotRoma.SendTextMessageAsync(message.Chat.Id, parseTextWithoutWordFile, replyMarkup: keyboard.Personality());
        }
    }
}
