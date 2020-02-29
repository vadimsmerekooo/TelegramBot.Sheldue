using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;
using Telegram_Bot.BL.Classes.Student;

namespace Telegram_Bot.View.Classes.Menu
{
    class ListDayWeak : MainMenu, IStepsOnMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        private string groupName;

        public ListDayWeak(TelegramBotClient Bot, string api, string group) : base(Bot, api)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
            groupName = group;
        }

        public async void ListDay(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
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
            switch (message.Text.ToLower())
            {
                case "понедельник":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepParseFile(sender, e, "понедельник");
                    break;
                case "вторник":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepParseFile(sender, e, "вторник");
                    break;
                case "среда":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepParseFile(sender, e, "среда");
                    break;
                case "четверг":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepParseFile(sender, e, "четверг");
                    break;
                case "пятница":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepParseFile(sender, e, "пятница");
                    break;
                case "суббота":
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    NextStepParseFile(sender, e, "суббота");
                    break;
            }
        }

        public async void NextStepParseFile(object sender, MessageEventArgs e, string day)
        {
            var message = e.Message;

            deleteMessage = new DeleteMessage(BotRoma, ApiKeyBot);
            deleteMessage.DeleteMessageOfMenu(message);

            await BotRoma.SendTextMessageAsync(message.Chat.Id, @"Заргузка...");

            CollectionInformationParseText collectionInform = new CollectionInformationParseText();
            string parseTextWithoutWordFile = collectionInform.SearchShedule(groupName, day);

            deleteMessage = new DeleteMessage(BotRoma, ApiKeyBot);
            deleteMessage.DeleteFirstMessage(message);
            await BotRoma.SendTextMessageAsync(message.Chat.Id, parseTextWithoutWordFile, replyMarkup: new ReplyKeyboardRemove());

        }
    }
}
