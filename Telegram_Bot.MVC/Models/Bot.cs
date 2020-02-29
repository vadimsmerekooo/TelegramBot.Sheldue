using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Telegram.Bot;
using studentassistant_bot.Models.Commands;
using System.ComponentModel;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace studentassistant_bot.Models
{
    public class Bot
    {
        private static TelegramBotClient client;
        private string ApiKeyBot;
        public Bot(TelegramBotClient Bot, string api)
        {
            TelegramBotClient client = Bot;
            this.ApiKeyBot = api;
        }
        
        public async void Get(object sender, DoWorkEventArgs e)
        {
            try
            {
                var worker = sender as BackgroundWorker;
                var key = e.Argument as String;
                client = new TelegramBotClient(key);
                await client.SetWebhookAsync("");
                client.OnMessage += BotOnMessageReceived;
                client.StartReceiving();
            }
            catch (Exception ex)
            {

            }
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;
            await client.SendTextMessageAsync(message.Chat.Id, "Hello");
        }
    }
}