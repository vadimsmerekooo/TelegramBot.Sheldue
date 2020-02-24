using System;
using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot.View.Classes
{
    class MenuPersonality : MainMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;

        public MenuPersonality(TelegramBotClient BotSet, string api) : base(BotSet, api)
        {
            this.BotRoma = BotSet;
            this.ApiKeyBot = api;
        }

        public async void ViewkeyBoardButton(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            var keyboardPersonality = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton($"Преподаватель 👨‍🏫"),
                                                    new KeyboardButton($"Учащийся {convertEmoji = new Emoji( 0x1F393 )}")
                                                }
                                            },
                ResizeKeyboard = true
            };
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Тыкай на кнопочку {convertEmoji = new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, keyboardPersonality);
            BotRoma.OnMessage += MenuPers;
        }

        private void MenuPers(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            switch (message.Text)
            {
                case "Преподаватель 👨‍🏫":
                    Teacher.MenuWithListTeacher menuSelectTeacher = new Teacher.MenuWithListTeacher(BotRoma, ApiKeyBot);
                    menuSelectTeacher.ViewListWithTeacher(sender, e);
                    BotRoma.OnMessage -= MenuPers;
                    break;
                case "Учащийся 🎓":
                    Student.MenuStudent menuSelectStudentDepartment = new Student.MenuStudent(BotRoma, ApiKeyBot);
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    menuSelectStudentDepartment.ListDepartment(sender, e);
                    BotRoma.OnMessage -= MenuPers;
                    break;
                default: BotRoma.OnMessage -= MenuPers; break;
            }
        }
    }
}
