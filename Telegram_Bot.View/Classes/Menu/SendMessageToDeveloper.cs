using System;
using System.Collections.Generic;
using IFCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram_Bot.View.Interface;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Telegram_Bot.View.Classes.Menu
{
    public class SendMessageToDeveloper : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        private XmlSerializer serializer = new XmlSerializer(typeof(List<int>), new XmlRootAttribute() { ElementName = "MessageChatIdClients" });
        private List<string> listCommand = new List<string>()
        {
            "/start", "/help", "Выбор личности 👥", "Помощь ❔", "/personality",
            "/reset", "/contacts", "Меню", "меню", "Расписание звонков ⌚", "Сотрудники колледжа 👨‍💼",
            "Написать разработчику 💬👨🏻‍💻", "Учащийся 🎓", "Преподаватель 👨‍🏫"
        };
        public SendMessageToDeveloper(TelegramBotClient Bot, string api, ref Dictionary<string, List<SheldueAllDaysTelegram>> sheldue) : base(Bot, api, ref sheldue)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
        }
        public async void SendMessageToDev(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (idMessageClientsBlackList.Contains(Convert.ToInt32(e.Message.Chat.Id)))
            {
                try { await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, $@"Вы находитесь в черном списке", ParseMode.MarkdownV2); } catch { }
                BotRoma.OnMessage -= SendMessageToDevp;
                return;
            }
            try { await BotRoma.SendTextMessageAsync(message.Chat.Id, "⚠ВНИМАНИЕ!⚠ Сообщение не должно содержать нецензурную брань❗, или оскорбления в адрес разработчика❗ Если одно из условий будет нарушено⛔, вы автоматически попадаете в черный список❌! Введите сообщение⬇"); } catch { }
            BotRoma.OnMessage += SendMessageToDevp;
        }

        private async void SendMessageToDevp(object sender, MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.Text || e.Message == null)
            {
                try { await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2); } catch { }
                BotRoma.OnMessage -= SendMessageToDevp;
                return;
            }
            if (idMessageClientsBlackList.Contains(Convert.ToInt32(e.Message.Chat.Id)))
            {
                await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, $@"Вы находитесь в черном списке", ParseMode.MarkdownV2);
                BotRoma.OnMessage -= SendMessageToDevp;
                return;
            }
            if (listCommand.Contains(e.Message.Text))
            {
                BotRoma.OnMessage -= SendMessageToDevp;
                return;
            }
            foreach (var word in e.Message.Text.Split(' '))
            {
                if (MainMenu.error_Word.Contains(word.ToLower()))
                {
                    if (idMessageClientsWarn != null && idMessageClientsWarn.Count != 0)
                    {
                        List<IFCore.DictionaryList> user = idMessageClientsWarn.Where(ids => ids.Id == Convert.ToInt32(e.Message.Chat.Id)).ToList();
                        if (user != null)
                        {
                            if (user[0].Count != 3)
                            {
                                ((IFCore.DictionaryList)idMessageClientsWarn.Where(ids => ids.Id == user[0].Id)).Count++;
                                using (StreamWriter sw = new StreamWriter("WarningListIdMessageChatClients.xml"))
                                {
                                    sw.WriteLine(string.Empty);
                                }
                                using (FileStream fs = new FileStream("WarningListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                                {
                                    MainMenu.serializerDictionary.Serialize(fs, idMessageClientsWarn);
                                }
                                try { await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, $"Вам выданно {user[0].Count} предупреждение📛! Внимание! Если предупреждений будет более 3, вам будет выдан бан! Причина: запрещенные слова 📛!"); } catch { }
                                break;
                            }
                            else
                            {
                                idMessageClientsWarn.Remove(user[0]);
                                using (StreamWriter sw = new StreamWriter("WarningListIdMessageChatClients.xml"))
                                {
                                    sw.WriteLine(string.Empty);
                                }
                                using (FileStream fs = new FileStream("WarningListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                                {
                                    MainMenu.serializerDictionary.Serialize(fs, idMessageClientsWarn);
                                }
                                idMessageClientsBlackList.Add(Convert.ToInt32(e.Message.Chat.Id));
                                using (StreamWriter sw = new StreamWriter("BlackListIdMessageChatClients.xml"))
                                {
                                    sw.WriteLine(string.Empty);
                                }
                                using (FileStream fs = new FileStream("BlackListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                                {
                                    serializer.Serialize(fs, idMessageClientsBlackList);
                                }
                                await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, "Вы занесены в черный список📛! Причина: максимальнок кол-во предупрдеждений!");
                                break;
                            }
                        }
                        else
                        {
                            idMessageClientsWarn.Add(new IFCore.DictionaryList(Convert.ToInt32(e.Message.Chat.Id), 1));
                            using (StreamWriter sw = new StreamWriter("WarningListIdMessageChatClients.xml"))
                            {
                                sw.WriteLine(string.Empty);
                            }
                            using (FileStream fs = new FileStream("WarningListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                            {
                                MainMenu.serializerDictionary.Serialize(fs, idMessageClientsWarn);
                            }
                            try { await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, $"Вам выданно {user[0].Count} предупреждение📛! Внимание! Если предупреждений будет более 3, вам будет выдан бан! Причина: запрещенные слова 📛!"); } catch { }
                            break;
                        }
                    }
                    else
                    {
                        idMessageClientsWarn.Add(new IFCore.DictionaryList(Convert.ToInt32(e.Message.Chat.Id), 1));
                        using (StreamWriter sw = new StreamWriter("WarningListIdMessageChatClients.xml"))
                        {
                            sw.WriteLine(string.Empty);
                        }
                        using (FileStream fs = new FileStream("WarningListIdMessageChatClients.xml", FileMode.OpenOrCreate))
                        {
                            MainMenu.serializerDictionary.Serialize(fs, idMessageClientsWarn);
                        }
                        try { await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, $"Вам выданно 1 предупреждение📛! Внимание! Если предупреждений будет более 3, вам будет выдан бан! Причина: запрещенные слова 📛"); } catch { }
                        break;
                    }
                }
            }
            try { await BotRoma.SendTextMessageAsync(415226650, "Сообщение от пользователя: " + e.Message.Text + $" От: {e.Message.Chat.Id} - {e.Message.Chat.FirstName}", replyMarkup: new ReplyKeyboardRemove()); } catch { }
            try { await BotRoma.SendTextMessageAsync(e.Message.Chat.Id, "Сообщение отправлено ✔"); } catch { }
            BotRoma.OnMessage -= SendMessageToDevp;
        }
    }
}
