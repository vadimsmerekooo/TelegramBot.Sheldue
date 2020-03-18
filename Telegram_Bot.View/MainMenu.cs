using System;
using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Classes.Menu;
using System.Threading;
using NLog;

namespace Telegram_Bot.View
{
    public class MainMenu
    { 
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public Emoji convertEmoji;
        private Keyboards keyboard = new Keyboards();
        private Logger logger;

        public MainMenu(TelegramBotClient Bot, string api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
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
                BotRoma.OnMessage += BotOnMessageReceived;
                BotRoma.StartReceiving();
                logger.Debug("Presentaition Layer: Status - enable");
            }
            catch
            {
                logger.Debug("Presentaition Layer: Status - disable");
            }
        }
        

        public async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null )
                return;
            switch (message.Text)
            {
                case "/start":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Привет - {message.From.FirstName}{convertEmoji = new Emoji(0x1F525)}!
Я - бот, меня зовут Рома {convertEmoji = new Emoji(0x1F916)};)
Я помогу составить тебе распиание пар на завтра {convertEmoji = new Emoji(0x1F4CB)}, без посещения сайта колледжа {convertEmoji = new Emoji(0x1F310)}!
Для промотра списка команд {convertEmoji = new Emoji(0x1F4DC)}, просто введи /help {convertEmoji = new Emoji(new int[] { 0x2139, 0xFE0F })}!
Для быстрого доступа к овновному меню, введи - Меню {convertEmoji = new Emoji(0x2714)}

Краткая информация о работе со мной:
{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} Если Я не отвечаю на команды, перезапусти меня командой /reset {convertEmoji = new Emoji(0x1F503)};)
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} Если после перезапуска, я не отвечаю на команды, свяжись с моим создателем {convertEmoji = new Emoji(0x1F4AD)}
");
                    Classes.Menu.PiarClasses.PiarInstagram piarInst = new Classes.Menu.PiarClasses.PiarInstagram(BotRoma, ApiKeyBot);
                    piarInst.InstagramDeveloper(e);
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери кнопку {convertEmoji = new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboard.Personality());
                    break;
                case "/help":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, keyboard.Help(), ParseMode.MarkdownV2);
                    break;
                case "Выбор личности 👥":
                    Classes.MenuPersonality menuSelectPerson = new Classes.MenuPersonality(BotRoma, ApiKeyBot);
                    menuSelectPerson.ViewkeyBoardButton(sender, e);
                    break;
                case "Помощь ❔":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, keyboard.Help(), ParseMode.MarkdownV2);
                    break;
                case "/personality":
                    Classes.MenuPersonality menuSelectPersonSecond = new Classes.MenuPersonality(BotRoma, ApiKeyBot);
                    menuSelectPersonSecond.ViewkeyBoardButton(sender, e);
                    break;
                case "/reset":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Привет - {message.From.FirstName}{convertEmoji = new Emoji(0x1F525)}
Я снова в строю {convertEmoji = new Emoji(0x2705)}", ParseMode.Markdown, false, false, 0, keyboard.Personality());
                    break;
                case "/contacts":
                    Classes.Menu.PiarClasses.PiarInstagram piarInstSlash = new Classes.Menu.PiarClasses.PiarInstagram(BotRoma, ApiKeyBot);
                    piarInstSlash.InstagramDeveloper(e);
                    break;
                case "Меню":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери кнопку {convertEmoji = new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboard.Personality());
                    break;
                case "меню":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Выбери кнопку {convertEmoji = new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboard.Personality());
                    break;
            }
           
        }
    }
}
