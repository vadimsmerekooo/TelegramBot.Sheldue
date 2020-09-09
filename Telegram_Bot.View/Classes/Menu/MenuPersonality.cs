using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Classes.Menu;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes
{
    class MenuPersonality : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue;
        long idUserIni;
        public MenuPersonality(TelegramBotClient BotSet, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue, long idInputUser):base(BotSet, api, ref sheldue)
        {
            this.BotRoma = BotSet;
            this.ApiKeyBot = api;
            this.sheldue = sheldue;
            idUserIni = idInputUser;
        }

        public async void SendMessagePersonality(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            if (!MainMenu.CheckPrivarteClassUser(idUserIni, e.Message.Chat.Id))
                return;
            var keyboardPersonality = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton($"Преподаватель 👨‍🏫"),
                                                    new KeyboardButton($"Учащийся {new Emoji( 0x1F393 )}")
                                                }
                                            },
                ResizeKeyboard = true
            };
            try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Тыкай на кнопочку {new Emoji(0x2B07)}", ParseMode.Default, false, false, 0, keyboardPersonality); } catch { }
            BotRoma.OnMessage += MenuPers;
        }

        private async void MenuPers(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            if (!MainMenu.CheckPrivarteClassUser(idUserIni, e.Message.Chat.Id))
                return;
            switch (message.Text)
            {
                case "Преподаватель 👨‍🏫":
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1); } catch { }
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
                    Teacher.MenuWithListTeacher menuSelectTeacher = new Teacher.MenuWithListTeacher(BotRoma, ApiKeyBot, sheldue, idUserIni);
                    menuSelectTeacher.SendMessageMenuTeacher(sender, e);
                    break;
                case "Учащийся 🎓":
                    try{ await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1); } catch { }
                    try { await BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId); } catch { }
                    Student.MenuStudent menuSelectStudentDepartment = new Student.MenuStudent(BotRoma, ApiKeyBot, sheldue, idUserIni);
                    menuSelectStudentDepartment.SendMessagemenuStudent(sender, e);
                    break;
                default: BotRoma.OnMessage -= MenuPers;
                    break;
            }
        }
    }
}
