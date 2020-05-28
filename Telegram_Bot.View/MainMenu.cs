using System;
using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Classes.Menu;
using System.Threading;
using NLog;
using System.Collections.Generic;
using IFCore;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View
{
    public class MainMenu: IMenu,IMainMenu
    { 
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public Emoji convertEmoji;
        private Keyboards keyboard = new Keyboards();
        private Logger logger;
        private Dictionary<string, List<SheldueAllDaysTelegram>> sheldue = new Dictionary<string, List<SheldueAllDaysTelegram>>();
        public static string week { get; set; }
        public Dictionary<string, List<SheldueAllDaysTelegram>> GetSheldue { get { return sheldue; } }
        

        public MainMenu(TelegramBotClient Bot, string api, Dictionary<string, List<SheldueAllDaysTelegram>> sheldue)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
            this.sheldue = sheldue;
        }


        public async void StartedMenu(object sender, DoWorkEventArgs e)
        {
            logger = LogManager.GetCurrentClassLogger();
            try
            {
                var worker = sender as BackgroundWorker;
                var key = e.Argument as String;
                BotRoma = new TelegramBotClient(key);
                await BotRoma.SetWebhookAsync("");
                BotRoma.OnMessage += SendMessage;
                BotRoma.StartReceiving();
                logger.Debug("Presentaition Layer: Status - enable");
            }
            catch(Exception ex)
            {
                logger.Debug("Presentaition Layer: Status - disable");
                Console.WriteLine("Бот остановлен: причина "+ ex.ToString());
            }
        }
        

        public virtual async void SendMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null )
            {
                await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"К сожалению😱, данная команда не понятна мне😥", ParseMode.MarkdownV2);
                return;
            }
            switch (message.Text)
            {
                case "/start":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Привет - {message.From.FirstName}{new Emoji(0x1F525)}!
Я - бот, меня зовут Рома {new Emoji(0x1F916)};)
Я покажу тебе распиание пар на завтра, или на любой день {new Emoji(0x1F4CB)}, без посещения сайта колледжа {new Emoji(0x1F310)}!
Для промотра списка команд {new Emoji(0x1F4DC)}, просто введи /help {new Emoji(new int[] { 0x2139, 0xFE0F })}!
Для быстрого доступа к овновному меню, введи - Меню {new Emoji(0x2714)}

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
            }
           
        }
    }
}
