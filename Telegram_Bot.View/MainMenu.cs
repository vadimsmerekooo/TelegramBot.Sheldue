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
using System.Reflection;
using System.Windows.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Telegram_Bot.View
{
    public class MainMenu : IMenu, IMainMenu
    {
        private static TelegramBotClient BotRoma;
        private static string ApiKeyBot;
        public List<int> idMessageClients = new List<int>();
        public List<int> idMessageClientsBlackList = new List<int>();
        public List<IFCore.DictionaryList> idMessageClientsWarn = new List<IFCore.DictionaryList>();
        public static Dictionary<int, int> idMessageClientsWarnings = new Dictionary<int, int>();
        private XmlSerializer serializer = new XmlSerializer(typeof(List<int>), new XmlRootAttribute() { ElementName = "MessageChatIdClients" });
        public static XmlSerializer serializerDictionary = new XmlSerializer(typeof(List<IFCore.DictionaryList>), new XmlRootAttribute() { ElementName = "MessageChatIdClients" });
        public Emoji convertEmoji;
        private Keyboards keyboard = new Keyboards();
        private static Dictionary<string, List<SheldueAllDaysTelegram>> sheldue = new Dictionary<string, List<SheldueAllDaysTelegram>>();
        public static string Week { get; set; }
        public Dictionary<string, List<SheldueAllDaysTelegram>> GetSheldue { get { return sheldue; } }
        public static Dictionary<string, List<SheldueAllDaysTelegram>> SetSheldue { set { sheldue = value; } }
        private Timer CheckMessageAntiDDOS = new Timer(CheckMessageAntiDDOSTimer, null, 0, 2000);
        private static int counterTimer = 0;
        public static TelegramBotClient GetBot { get { return BotRoma; } }
        public static string GetApi { get { return ApiKeyBot; } }
        public static List<string> error_Word;

        public MainMenu(TelegramBotClient Bot, string api, ref Dictionary<string, List<SheldueAllDaysTelegram>> sheldue)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
            MainMenu.sheldue = sheldue;
        }
        private static void CheckMessageAntiDDOSTimer(object obj)
        {
            if (counterTimer >= 20)
            {
                idMessageClientsWarnings.Clear();
                counterTimer = 0;
            }
            else
                counterTimer++;
        }
        public async void StartedMenu(object sender, DoWorkEventArgs e)
        {
            try
            {
                var worker = sender as BackgroundWorker;
                var key = e.Argument as String;
                if (File.Exists("Error_Word.txt"))
                    error_Word = File.ReadAllText("Error_Word.txt").ToLower().Split(new char[] { ',', ' ', '.' }).ToList();
                BotRoma = new TelegramBotClient(key);
                await BotRoma.SetWebhookAsync("");
                BotRoma.OnMessage += SendMessageAdminPanel;
                BotRoma.OnMessage += SendMessage;
                BotRoma.StartReceiving();
            }
            catch (Exception ex)
            {
                IFCore.IFCore.loggerMain.Error("Presentaition Layer: Error " + ex.ToString());
                Console.WriteLine("Бот остановлен: причина " + ex.ToString());
            }
        }
        public async void SendMessageAdminPanel(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            if (message.Chat.Id == 415226650)
            {
                switch (message.Text.GetHashCode())
                {
                    case 847733991:
                        try { await new SendAlertAllUsers(BotRoma, ApiKeyBot, idMessageClients, sheldue).AlertMessage("Бот - Рома🤖, на время остановлен🛑 PS: Admin - экстренное отключение!"); } catch { }
                        BotRoma.OnMessage -= SendMessage;
                        break;
                    case 137360049:
                        try { await new SendAlertAllUsers(BotRoma, ApiKeyBot, idMessageClients, sheldue).AlertMessage("Бот - Рома🤖, снова в строю🛎!"); } catch { }
                        BotRoma.OnMessage += SendMessage;
                        break;
                }
                if (message.Text.Contains("Ban"))
                {
                    try
                    {
                        var splitMessage = message.Text.Split(' ');
                        if (idMessageClientsBlackList.Contains(Convert.ToInt32(splitMessage[1])))
                        {
                            try { await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь уже находиться в бане!"); } catch { }
                            return;
                        }
                        if (idMessageClientsWarn != null && idMessageClientsWarn.Count != 0)
                        {
                            List<IFCore.DictionaryList> userList = idMessageClientsWarn.Where(ids => ids.Id == Convert.ToInt32(splitMessage[1])).ToList();
                            if (userList != null || userList.Count != 0)
                            {
                                IFCore.DictionaryList user = userList[0];
                                if (user.Count != 3)
                                {
                                    ((IFCore.DictionaryList)idMessageClientsWarn.Where(ids => ids.Id == user.Id)).Count++;
                                    using (StreamWriter sw = new StreamWriter("WarningListIdMessageChatClients.xml"))
                                    {
                                        sw.WriteLine(string.Empty);
                                    }
                                    using (FileStream fs = new FileStream("WarningListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                                    {
                                        MainMenu.serializerDictionary.Serialize(fs, idMessageClientsWarn);
                                    }
                                    string textMessageBan = string.Empty;
                                    for (int i = 2; i < splitMessage.Length; i++)
                                    {
                                        textMessageBan += splitMessage[i] + " ";
                                    }
                                    try { await BotRoma.SendTextMessageAsync(Convert.ToInt32(splitMessage[1]), $"Вам выданно {user.Count} предупреждение📛! Внимание! Если предупреждений будет более 3, вам будет выдан бан! Причина: " + textMessageBan); } catch { }
                                    try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Пользователю выданно {user.Count} предупреждение! Сообщение отправлено!"); } catch { }
                                    return;
                                }
                                else
                                {
                                    idMessageClientsWarn.Remove(user);
                                    using (StreamWriter sw = new StreamWriter("WarningListIdMessageChatClients.xml"))
                                    {
                                        sw.WriteLine(string.Empty);
                                    }
                                    using (FileStream fs = new FileStream("WarningListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                                    {
                                        MainMenu.serializerDictionary.Serialize(fs, idMessageClientsWarn);
                                    }
                                    idMessageClientsBlackList.Add(Convert.ToInt32(splitMessage[1]));
                                    using (StreamWriter sw = new StreamWriter("BlackListIdMessageChatClients.xml"))
                                    {
                                        sw.WriteLine(string.Empty);
                                    }
                                    using (FileStream fs = new FileStream("BlackListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                                    {
                                        serializer.Serialize(fs, idMessageClientsBlackList);
                                    }
                                    string textBanWarning = string.Empty;
                                    for (int i = 2; i < splitMessage.Length; i++)
                                    {
                                        textBanWarning += splitMessage[i] + " ";
                                    }
                                    await BotRoma.SendTextMessageAsync(Convert.ToInt32(splitMessage[1]), "Вы занесены в черный список📛! Причина: " + textBanWarning);
                                    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь занесен в черный список! Сообщение отправлено!");
                                    return;
                                }

                            }
                            else
                            {
                                idMessageClientsWarn.Add(new IFCore.DictionaryList(Convert.ToInt32(splitMessage[1]), 1));
                                using (StreamWriter sw = new StreamWriter("WarningListIdMessageChatClients.xml"))
                                {
                                    sw.WriteLine(string.Empty);
                                }
                                using (FileStream fs = new FileStream("WarningListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                                {
                                    MainMenu.serializerDictionary.Serialize(fs, idMessageClientsWarn);
                                }
                                string textMessageBan = string.Empty;
                                for (int i = 2; i < splitMessage.Length; i++)
                                {
                                    textMessageBan += splitMessage[i] + " ";
                                }
                                try { await BotRoma.SendTextMessageAsync(Convert.ToInt32(splitMessage[1]), $"Вам выданно 1 предупреждение📛! Внимание! Если предупреждений будет более 3, вам будет выдан бан! Причина: " + textMessageBan); } catch { }
                                try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Пользователю выданно 1 предупреждение! Сообщение отправлено!"); } catch { };
                            }
                        }
                        else
                        {
                            idMessageClientsWarn.Add(new IFCore.DictionaryList(Convert.ToInt32(splitMessage[1]), 1));
                            using (StreamWriter sw = new StreamWriter("WarningListIdMessageChatClients.xml"))
                            {
                                sw.WriteLine(string.Empty);
                            }
                            using (FileStream fs = new FileStream("WarningListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                            {
                                MainMenu.serializerDictionary.Serialize(fs, idMessageClientsWarn);
                            }
                            string textMessageBan = string.Empty;
                            for (int i = 2; i < splitMessage.Length; i++)
                            {
                                textMessageBan += splitMessage[i] + " ";
                            }
                            try { await BotRoma.SendTextMessageAsync(Convert.ToInt32(splitMessage[1]), $"Вам выданно 1 предупреждение📛! Внимание! Если предупреждений будет более 3, вам будет выдан бан! Причина: " + textMessageBan); } catch { }
                            try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Пользователю выданно 1 предупреждение! Сообщение отправлено!"); } catch { }
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex != null && ex.Message.Contains("bot was blocked by the user"))
                        {
                            await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь заблокировал бота!");
                            return;
                        }
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
                            await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь не найден в черном списке!");
                            return;
                        }
                        //ICollection<int> keys = idMessageClientsWarnings.Keys;
                        //if (!keys.Contains(Convert.ToInt32(splitMessage[1])))
                        //{
                        //    await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь не найден в списке предупреждений!");
                        //    return;
                        //}
                        idMessageClientsBlackList.Remove(Convert.ToInt32(splitMessage[1]));
                        using (StreamWriter sw = new StreamWriter("BlackListIdMessageChatClients.xml"))
                        {
                            sw.WriteLine(string.Empty);
                        }
                        using (FileStream fs = new FileStream("BlackListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                        {
                            serializer.Serialize(fs, idMessageClientsBlackList);
                        }
                        await BotRoma.SendTextMessageAsync(Convert.ToInt32(splitMessage[1]), "Вы удалены из черного списка👍! За повторные нарушения, бан, сниматься не будет👤! Хорошего дня!👾");
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь удален из черного списка! Сообщение отправлено!");
                    }
                    catch (Exception ex)
                    {
                        if (ex != null && ex.Message.Contains("bot was blocked by the user"))
                        {
                            await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь заблокировал бота!");
                            return;
                        }
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
                        await BotRoma.SendTextMessageAsync(chatId, $"Доброго времени суток👾! Ответ на ваше обращение💨 к админу бота: {textSend}");
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ответ отправлен пользователю!");
                    }
                    catch (Exception ex)
                    {
                        if (ex != null && ex.Message.Contains("bot was blocked by the user"))
                        {
                            await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь заблокировал бота!");
                            return;
                        }
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                    }
                }
                if (message.Text.Contains("MessageTo"))
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
                        await BotRoma.SendTextMessageAsync(chatId, $"Доброго времени суток👾! Это, админ бота, {textSend}");
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Сообщение отправлен пользователю!");
                    }
                    catch (Exception ex)
                    {
                        if (ex != null && ex.Message.Contains("bot was blocked by the user"))
                        {
                            await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь заблокировал бота!");
                            return;
                        }
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                    }
                }
                if (message.Text.Contains("Allsend"))
                {
                    try
                    {
                        new SendAlertAllUsers(BotRoma, ApiKeyBot, idMessageClients, sheldue).AlertMessage(message.Text.Replace("Allsend", "") + ". Рассылку сообщений ✉, можно отключить командой /stop!");
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Сообщения отправляются!");
                    }
                    catch (Exception ex)
                    {
                        if (ex != null && ex.Message.Contains("bot was blocked by the user"))
                        {
                            await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь заблокировал бота!");
                            return;
                        }
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                    }
                }
                if (message.Text.Contains("List"))
                {
                    try
                    {
                        string listClients = @"Cписок пользователей:
";
                        foreach (var id in idMessageClients)
                        {
                            listClients += id + @"
";
                        }
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, listClients);
                    }
                    catch (Exception ex)
                    {
                        if (ex != null && ex.Message.Contains("bot was blocked by the user"))
                        {
                            await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь заблокировал бота!");
                            return;
                        }
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                    }
                }
                if (message.Text.Contains("Blacklist"))
                {
                    try
                    {
                        string blackListClients = @"Черный список:
";
                        foreach (var id in idMessageClientsBlackList)
                        {
                            blackListClients += id + @"
";
                        }
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, blackListClients);
                    }
                    catch (Exception ex)
                    {
                        if (ex != null && ex.Message.Contains("bot was blocked by the user"))
                        {
                            await BotRoma.SendTextMessageAsync(message.Chat.Id, "Пользователь заблокировал бота!");
                            return;
                        }
                        await BotRoma.SendTextMessageAsync(message.Chat.Id, "Ошибка в команде!");
                    }
                }
            }
        }

        public async void SendMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (!await CheckCountMessage(message))
                return;

            if (idMessageClientsBlackList.Contains(Convert.ToInt32(message.Chat.Id)))
            {
                try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Вы находитесь в черном списке📛\!", ParseMode.MarkdownV2); } catch { }
                return;
            }
            if (message.Type != MessageType.Text || message == null)
            {
                try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2); } catch { }
                return;
            }

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
            switch (message.Text)
            {
                case "/start":
                    try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Привет - {message.From.FirstName}{new Emoji(0x1F525)}!
Я - бот, меня зовут Рома {new Emoji(0x1F916)};)
Я покажу тебе распиание пар на завтра, или на любой день {new Emoji(0x1F4CB)}, без посещения сайта колледжа {new Emoji(0x1F310)}!
Для промотра списка команд {new Emoji(0x1F4DC)}, просто введи /help {new Emoji(new int[] { 0x2139, 0xFE0F })}!
Для быстрого доступа к овновному меню, введи - Меню {new Emoji(0x2714)}

⚠️⚡!!!ВНИМАНИЕ!!!⚡⚠️ ПРИ РАБОТЕ С БОТОМ 🅱️, Ваш Id 🆔, будет записан на локальную машину 🏧. Он требуется для оповещения 💬 о каких либо сообщениях от разработчика 👨🏽‍💻, оповещении нового расписания 📅! Данная информация ℹ️, полность КОНФИДЕНЦИАЛЬНА 🔒!
Для удаления Вас из базы, введите команду /stop 👨🏽‍💻!

Краткая информация о работе со мной:
{new Emoji(new int[] { 0x0031, 0x20E3 })} Если Я не отвечаю на команды, перезапусти меня командой /reset {new Emoji(0x1F503)};)
{new Emoji(new int[] { 0x0032, 0x20E3 })} Если после перезапуска, я не отвечаю на команды, свяжись с моим создателем {new Emoji(0x1F4AD)}"); } catch { }
                    new Classes.Menu.PiarClasses.PiarInstagram(BotRoma, ApiKeyBot, sheldue).SendMessagePiarInst(sender, e);
                    try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери кнопку {new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboard.Personality()); } catch { }
                    break;
                case "/help":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, keyboard.Help(), ParseMode.MarkdownV2);
                    break;
                case "Выбор личности 👥":
                    new Classes.MenuPersonality(BotRoma, ApiKeyBot, sheldue).SendMessagePersonality(sender, e);
                    break;
                case "Помощь ❔":
                    try { await BotRoma.SendTextMessageAsync(message.Chat.Id, keyboard.Help(), ParseMode.MarkdownV2); } catch { }
                    break;
                case "/personality":
                    new Classes.MenuPersonality(BotRoma, ApiKeyBot, sheldue).SendMessagePersonality(sender, e);
                    break;
                case "/reset":
                    try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Привет - {message.From.FirstName}{new Emoji(0x1F525)}
Я снова в строю {new Emoji(0x2705)}", ParseMode.Markdown, false, false, 0, keyboard.Personality()); } catch { }
                    break;
                case "/contacts":
                    new Classes.Menu.PiarClasses.PiarInstagram(BotRoma, ApiKeyBot, sheldue).SendMessagePiarInst(sender, e);
                    break;
                case "/stop":
                    idMessageClients.Remove(Convert.ToInt32(message.Chat.Id));
                    using (StreamWriter sw = new StreamWriter("ListIdMessageChatClients.xml"))
                    {
                        sw.WriteLine(string.Empty);
                    }
                    using (FileStream fs = new FileStream("ListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                    {
                        serializer.Serialize(fs, idMessageClients);
                    }
                    try { await BotRoma.SendTextMessageAsync(message.Chat.Id, "Вы удалены из базы данных ☑! Рассылка больше не будет приходить ⚡! Хорошего дня 🙈!"); } catch { }
                    break;
                case "Меню":
                    try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери кнопку {new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboard.Personality()); } catch { }
                    break;
                case "меню":
                    try { await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери кнопку {new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboard.Personality()); } catch { }
                    break;
                case "Расписание звонков ⌚":
                    new ListCallsLessons(BotRoma, ApiKeyBot, sheldue).SendMessageCallLesson(sender, e);
                    break;
                case "Сотрудники колледжа 👨‍💼":
                    new ListCollegeWorker(BotRoma, ApiKeyBot, sheldue).SendMessageCollegeWorker(sender, e);
                    break;
                case "Написать разработчику 💬👨🏻‍💻":
                    new SendMessageToDeveloper(BotRoma, ApiKeyBot, ref sheldue).SendMessageToDev(sender, e);
                    break;
            }
        }
        public async Task<bool> CheckCountMessage(Telegram.Bot.Types.Message message)
        {
            try
            {
                ICollection<int> keys = idMessageClientsWarnings.Keys;
                if (keys.Contains(Convert.ToInt32(message.Chat.Id)))
                {
                    idMessageClientsWarnings[Convert.ToInt32(message.Chat.Id)]++;
                    if (idMessageClientsWarnings[Convert.ToInt32(message.Chat.Id)] >= 20)
                    {
                        if (idMessageClientsBlackList.Contains(Convert.ToInt32(message.Chat.Id)))
                        {
                            try { await BotRoma.SendTextMessageAsync(Convert.ToInt32(message.Chat.Id), "Вы занесены в черный список📛! Причина: Спам сообщений!"); }

                            catch
                            {
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Попытка отправки более 70 сообщений!!! Пользователь занесен в черный список!!!");
                            Console.ResetColor();
                            try { if (!idMessageClientsBlackList.Contains(Convert.ToInt32(message.Chat.Id))) await BotRoma.SendTextMessageAsync(Convert.ToInt32(415226650), "Попытка отправки более 70 сообщений📛! Пользователь добавлен в бан! ID: " + message.Chat.Id); } catch { }
                            return false;
                        }
                        idMessageClientsBlackList.Add(Convert.ToInt32(message.Chat.Id));
                        using (StreamWriter sw = new StreamWriter("BlackListIdMessageChatClients.xml"))
                        {
                            sw.WriteLine(string.Empty);
                        }
                        using (FileStream fs = new FileStream("BlackListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                        {
                            serializer.Serialize(fs, idMessageClientsBlackList);
                        }
                        try { await BotRoma.SendTextMessageAsync(Convert.ToInt32(message.Chat.Id), "Вы занесены в черный список📛! Причина: Спам сообщений!"); }

                        catch
                        {
                        }

                        try { await BotRoma.SendTextMessageAsync(Convert.ToInt32(415226650), "Попытка отправки более 70 сообщений📛! Пользователь добавлен в бан! ID: " + message.Chat.Id); } catch { }
                        return false;
                    }
                }
                else
                {
                    idMessageClientsWarnings.Add(Convert.ToInt32(message.Chat.Id), 1);
                }
                return true;
            }
            catch
            {
                return true;
            }
        }
    }
}
