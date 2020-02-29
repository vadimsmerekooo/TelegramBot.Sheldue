using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram_Bot.View.Classes.Menu;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes.Student
{
    class MenuStudent : MainMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;

        public MenuStudent(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }

        public async void ListDepartment(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            var keyboardGroups = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton("1"),
                                                    new KeyboardButton("2"),
                                                    new KeyboardButton("3"),
                                                    new KeyboardButton("4")
                                                }
                                            },
                ResizeKeyboard = true
            };
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"*Список отделений:*

{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} Информационное отделение
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} Швейное отделение
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} Электромеханическое отделение
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} Отделение машиностроения", ParseMode.MarkdownV2, false, false, 0, keyboardGroups);   //replyMarkup: new ReplyKeyboardRemove()
           
            BotRoma.OnMessage += Frog;
        }
        public void Frog(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            switch (message.Text)
            {
                case "1":
                    deleteMessage = new DeleteMessage(BotRoma, ApiKeyBot);
                    deleteMessage.DeleteMessageOfMenu(message);
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    IStepsForWorkFile lid = new Menu.MenuIDepartment.ListInformationDepartmentGroup(BotRoma, ApiKeyBot);
                    lid.ViewListGroups(sender, e);
                    break;
                case "2":
                    deleteMessage = new DeleteMessage(BotRoma, ApiKeyBot);
                    deleteMessage.DeleteMessageOfMenu(message);
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    IStepsForWorkFile lsd = new Menu.MenuSDepartment.ListSewingDepartmentGroups(BotRoma, ApiKeyBot);
                    lsd.ViewListGroups(sender, e);
                    break;
                case "3":
                    deleteMessage = new DeleteMessage(BotRoma, ApiKeyBot);
                    deleteMessage.DeleteMessageOfMenu(message);
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    IStepsForWorkFile lemd = new Menu.MenuEMDepartment.ListElectoMechanicDepartmentGroups(BotRoma, ApiKeyBot);
                    lemd.ViewListGroups(sender, e);
                    break;
                case "4":
                    deleteMessage = new DeleteMessage(BotRoma, ApiKeyBot);
                    deleteMessage.DeleteMessageOfMenu(message);
                    BotRoma.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                    IStepsForWorkFile lmd = new Menu.MenuMDepartment.ListMechatronicDepartmentGroups(BotRoma, ApiKeyBot);
                    lmd.ViewListGroups(sender, e);
                    break;
                default: BotRoma.OnMessage -= Frog; break;
            }
        }
    }
}
