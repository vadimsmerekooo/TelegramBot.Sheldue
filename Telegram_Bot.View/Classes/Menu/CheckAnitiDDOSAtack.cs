using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Telegram_Bot.View.Classes.Menu
{
    public static class CheckAnitiDDOSAtack
    {
        public static async void CheckCountMessage(Telegram.Bot.Types.Message message)
        {
            //ICollection<int> keys = MainMenu.idMessageClientsWarnings.Keys;
            //if (keys.Contains(Convert.ToInt32(message.Chat.Id)))
            //{
            //    MainMenu.idMessageClientsWarnings[Convert.ToInt32(message.Chat.Id)]++;
            //    if (MainMenu.idMessageClientsWarnings[Convert.ToInt32(message.Chat.Id)] >= 50)
            //    {
            //        idMessageClientsBlackList.Add(Convert.ToInt32(message.Chat.Id));
            //        using (StreamWriter sw = new StreamWriter("BlackListIdMessageChatClients.xml"))
            //        {
            //            sw.WriteLine(string.Empty);
            //        }
            //        using (FileStream fs = new FileStream("BlackListIdMessageChatClients.xml", FileMode.OpenOrCreate))
            //        {
            //            MainMenu.serializerStatic.Serialize(fs, MainMenu.idMessageClientsBlackList);
            //        }
            //        try { await MainMenu.GetBot.SendTextMessageAsync(Convert.ToInt32(message.Chat.Id), "Вы занесены в черный список📛! Причина: Спам сообщений!"); } catch { }

            //        try { await MainMenu.GetBot.SendTextMessageAsync(Convert.ToInt32(415226650), "Попытка отправки более 70 сообщений📛! Пользователь добавлен в бан! ID: " + message.Chat.Id); } catch { }
            //        return false;
            //    }
            //}
            //else
            //{
            //    MainMenu.idMessageClientsWarnings.Add(Convert.ToInt32(message.Chat.Id), 1);
            //}
            //return true;
        }
    }
}
