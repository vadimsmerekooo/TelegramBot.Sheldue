using System;
using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Classes.Menu;
using System.Threading;
using System.Collections.Generic;
using IFCore;
using Telegram_Bot.View.Interface;
using System.IO;
using System.Xml.Serialization;

namespace Telegram_Bot.View
{
    public class MainMenu : IMenu, IMainMenu
    {
        private static TelegramBotClient BotRoma;
        private static string ApiKeyBot;
        public List<int> idMessageClients = new List<int>();
        public List<int> idMessageClientsBlackList = new List<int>();
        private XmlSerializer serializer = new XmlSerializer(typeof(List<int>), new XmlRootAttribute() { ElementName = "MessageChatIdClients" });
        public Emoji convertEmoji;
        private Keyboards keyboard = new Keyboards();
        private Dictionary<string, List<SheldueAllDaysTelegram>> sheldue = new Dictionary<string, List<SheldueAllDaysTelegram>>();
        public static string week { get; set; }
        public Dictionary<string, List<SheldueAllDaysTelegram>> GetSheldue { get { return sheldue; } }

        public static TelegramBotClient GetBot { get { return BotRoma; } }
        public static string GetApi { get { return ApiKeyBot; } }

        public MainMenu(TelegramBotClient Bot, string api, ref Dictionary<string, List<SheldueAllDaysTelegram>> sheldue)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
            this.sheldue = sheldue;
        }


        public async void StartedMenu(object sender, DoWorkEventArgs e)
        {
            try
            {
                var worker = sender as BackgroundWorker;
                var key = e.Argument as String;
                BotRoma = new TelegramBotClient(key);
                await BotRoma.SetWebhookAsync("");
                BotRoma.OnMessage += SendMessage;
                BotRoma.OnMessage += SendMessageAdminPanel;
                BotRoma.StartReceiving();
            }
            catch (Exception ex)
            {
                IFCore.IFCore.loggerMain.Error("Presentaition Layer: Error " + ex.ToString());
                Console.WriteLine("Бот остановлен: причина " + ex.ToString());
            }
        }


        public virtual async void SendMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (idMessageClientsBlackList.Contains(Convert.ToInt32(message.Chat.Id)))
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Вы находитесь в черном списке", ParseMode.MarkdownV2);
                return;
            }
            if (message.Type != MessageType.Text || message == null)
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            switch (message.Text)
            {
                case "/start":
                    if (idMessageClients == null)
                    {
                        idMessageClients.Add(Convert.ToInt32(message.Chat.Id));
                        using (StreamWriter sw = new StreamWriter("ListIdMessageChatClients.xml"))
                        {
                            sw.WriteLine(string.Empty);
                        }
                        using (FileStream fs = new FileStream("ListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                        {
                            serializer.Serialize(fs, idMessageClients);
                        }
                        Console.WriteLine("Новый пользователь!");
                    }
                    if (!idMessageClients.Contains(Convert.ToInt32(message.Chat.Id)))
                    {
                        idMessageClients.Add(Convert.ToInt32(message.Chat.Id));
                        using (StreamWriter sw = new StreamWriter("ListIdMessageChatClients.xml"))
                        {
                            sw.WriteLine(string.Empty);
                        }
                        using (FileStream fs = new FileStream("ListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                        {
                            serializer.Serialize(fs, idMessageClients);
                        }
                    }
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Привет - {message.From.FirstName}{new Emoji(0x1F525)}!
Я - бот, меня зовут Рома {new Emoji(0x1F916)};)
Я покажу тебе распиание пар на завтра, или на любой день {new Emoji(0x1F4CB)}, без посещения сайта колледжа {new Emoji(0x1F310)}!
Для промотра списка команд {new Emoji(0x1F4DC)}, просто введи /help {new Emoji(new int[] { 0x2139, 0xFE0F })}!
Для быстрого доступа к овновному меню, введи - Меню {new Emoji(0x2714)}

⚠️⚡!!!ВНИМАНИЕ!!!⚡⚠️ ПРИ НАЖАТИИ НА КОМАНДУ start 🅱️, Ваш Id 🆔, будет записан на локальную машину 🏧. Он требуется для оповещения 💬 о каких либо сообщениях от разработчика 👨🏽‍💻, оповещении нового расписания 📅! Данная информация ℹ️, полность КОНФИДЕНЦИАЛЬНА 🔒!
Для удаления Вас из базы, обратитесь к разработчику 👨🏽‍💻!

Краткая информация о работе со мной:
{new Emoji(new int[] { 0x0031, 0x20E3 })} Если Я не отвечаю на команды, перезапусти меня командой /reset {new Emoji(0x1F503)};)
{new Emoji(new int[] { 0x0032, 0x20E3 })} Если после перезапуска, я не отвечаю на команды, свяжись с моим создателем {new Emoji(0x1F4AD)}");
                    new Classes.Menu.PiarClasses.PiarInstagram(BotRoma, ApiKeyBot, sheldue).SendMessage(sender, e);
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери кнопку {new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboard.Personality());
                    break;
                case "/help":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, keyboard.Help(), ParseMode.MarkdownV2);
                    break;
                case "Выбор личности 👥":
                    new Classes.MenuPersonality(BotRoma, ApiKeyBot, sheldue).SendMessage(sender, e);
                    break;
                case "Помощь ❔":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, keyboard.Help(), ParseMode.MarkdownV2);
                    break;
                case "/personality":
                    new Classes.MenuPersonality(BotRoma, ApiKeyBot, sheldue).SendMessage(sender, e);
                    break;
                case "/reset":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Привет - {message.From.FirstName}{new Emoji(0x1F525)}
Я снова в строю {new Emoji(0x2705)}", ParseMode.Markdown, false, false, 0, keyboard.Personality());
                    break;
                case "/contacts":
                    new Classes.Menu.PiarClasses.PiarInstagram(BotRoma, ApiKeyBot, sheldue).SendMessage(sender, e);
                    break;
                case "Меню":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери кнопку {new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboard.Personality());
                    break;
                case "меню":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери кнопку {new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboard.Personality());
                    break;
                case "Расписание звонков ⌚":
                    new ListCallsLessons(BotRoma, ApiKeyBot, sheldue).SendMessage(sender, e);
                    break;
                case "Сотрудники колледжа 👨‍💼":
                    new ListCollegeWorker(BotRoma, ApiKeyBot, sheldue).SendMessage(sender, e);
                    break;
                case "Написать разработчику 💬👨🏻‍💻":
                    new SendMessageToDeveloper(BotRoma, ApiKeyBot, ref sheldue).SendMessage(sender, e);
                    break;
            }
        }
        public async void SendMessageAdminPanel(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            switch (message.Text.GetHashCode())
            {
                case 847733991:
                    await new SendAlertAllUsers(BotRoma, ApiKeyBot, idMessageClients, sheldue).AlertMessage("Бот - Рома🤖, на время остановлен🛑 PS: Admin - экстренное отключение!");
                    BotRoma.OnMessage -= SendMessage;
                    break;
                case 137360049:
                    await new SendAlertAllUsers(BotRoma, ApiKeyBot, idMessageClients, sheldue).AlertMessage("Бот - Рома🤖, снова в строю🛎!");
                    BotRoma.OnMessage += SendMessage;
                    break;
            }
            if (message.Text.Contains("Ban"))
            {
                try
                {
                    var splitMessage = message.Text.Split(' ');
                    idMessageClientsBlackList.Add(Convert.ToInt32(splitMessage[1]));
                    using (StreamWriter sw = new StreamWriter("BlackListIdMessageChatClients.xml"))
                    {
                        sw.WriteLine(string.Empty);
                    }
                    using (FileStream fs = new FileStream("BlackListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                    {
                        serializer.Serialize(fs, idMessageClientsBlackList);
                    }
                    string textBan = string.Empty;
                    for (int i = 2; i < splitMessage.Length; i++)
                    {
                        textBan += splitMessage[i] + " ";
                    }
                    await BotRoma.SendTextMessageAsync(Convert.ToInt32(splitMessage[1]), "Вы занесены в черный список! Причина: " + textBan);
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь занесен в черный список! Сообщение отправлено!");
                }
                catch
                {
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                }
            }
            if (message.Text.Contains("Dban"))
            {
                try
                {
                    var splitMessage = message.Text.Split(' ');
                    if (!idMessageClientsBlackList.Contains(Convert.ToInt32(splitMessage[1])))
                    {
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь не найден!");
                        return;
                    }
                    idMessageClientsBlackList.Remove(Convert.ToInt32(splitMessage[1]));

                    using (StreamWriter sw = new StreamWriter("BlackListIdMessageChatClients.xml"))
                    {
                        sw.WriteLine(string.Empty);
                    }
                    using (FileStream fs = new FileStream("BlackListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                    {
                        serializer.Serialize(fs, idMessageClientsBlackList);
                    }
                    await BotRoma.SendTextMessageAsync(Convert.ToInt32(splitMessage[1]), "Вы удалены из черного списка!");
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь удален из черного списка! Сообщение отправлено!");
                }
                catch
                {
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                }
            }
            if (message.Text.Contains("Send"))
            {
                try
                {
                    var splitMessage = message.Text.Split(' ');
                    int chatId = Convert.ToInt32(splitMessage[1]);
                    string textSend = string.Empty;
                    for (int i = 2; i < splitMessage.Length; i++)
                    {
                        textSend += splitMessage[i] + " ";
                    }
                    await BotRoma.SendTextMessageAsync(chatId, $"Доброго времени суток! Ответ на ваше обращение к админу бота: {textSend}");
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ответ отправлен пользователю!");
                }
                catch
                {
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                }
            }
            if (message.Text.Contains("Allsend"))
            {
                try
                {
                    var splitMessage = message.Text.Split(' ');
                    string textSend = string.Empty;
                    for (int i = 1; i < splitMessage.Length; i++)
                    {
                        textSend += splitMessage[i] + " ";
                    }
                    new SendAlertAllUsers(BotRoma, ApiKeyBot, idMessageClients, sheldue).AlertMessage(textSend);
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Сообщения отправляются!");
                }
                catch
                {
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                }
            }
            if (message.Text.Contains("List"))
            {
                try
                {
                    string listClients = string.Empty;
                    foreach (var id in idMessageClients)
                    {
                        listClients += id;
                    }
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, listClients);
                }
                catch
                {
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                }
            }
            if (message.Text.Contains("Blacklist"))
            {
                try
                {
                    string blackListClients = string.Empty;
                    foreach (var id in idMessageClientsBlackList)
                    {
                        blackListClients += id;
                    }
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, blackListClients);
                }
                catch
                {
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                }
            }
        }
    }
}
