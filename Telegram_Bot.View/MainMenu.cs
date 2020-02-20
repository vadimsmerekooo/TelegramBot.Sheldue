using System;
using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot.View
{
    public class MainMenu
    { 
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public Emoji convertEmoji;
        public MainMenu(TelegramBotClient Bot, string api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }


        public async void StartedMenu(object sender, DoWorkEventArgs e)
        {
            try
            {
                var worker = sender as BackgroundWorker;
                var key = e.Argument as String;
                BotRoma = new TelegramBotClient(key);
                await BotRoma.SetWebhookAsync("");
                BotRoma.OnMessage += BotOnMessageReceived;
                BotRoma.OnCallbackQuery += BotOnMessageCallBack;
                BotRoma.StartReceiving();
            }
            catch (Exception ex)
            {

            }
        }

        private void BotOnMessageCallBack(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            if (e.CallbackQuery.Data == "callbackPersonality")
            {
                
            }
        }

        public async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
                return;

            switch (message.Text)
            {
                case "/start":
                    await BotRoma.SetWebhookAsync("");
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Привет - {message.From.FirstName}{convertEmoji = new Emoji(0x1F525)}!
Я - бот, меня зовут Рома {convertEmoji = new Emoji(0x1F916)};)
Я помогу составить тебе распиание пар на завтра {convertEmoji = new Emoji(0x1F4CB)}, без посещения сайта колледжа {convertEmoji = new Emoji(0x1F310)}!
Для промотра списка команд {convertEmoji = new Emoji(0x1F4DC)}, просто введи /help {convertEmoji = new Emoji(new int[] { 0x2139, 0xFE0F })}!

Краткая информация о работе со мной:
{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} Если Я не отвечаю на команды, перезапусти меня командой /reset {convertEmoji = new Emoji(0x1F503)};)
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} Если после перезапуска, я не отвечаю на команды, свяжись с моим создателем {convertEmoji = new Emoji(0x1F4AD)}
");
                    var keyboardMain = new ReplyKeyboardMarkup
                    {
                        Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton($"Выбор личности {convertEmoji = new Emoji(0x1F465)}"),
                                                    new KeyboardButton($"Помощь {convertEmoji = new Emoji(0x2754)}")
                                                },
                                            },
                        ResizeKeyboard = true
                    };
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $"Тыкай на кнопочку {convertEmoji = new Emoji(0x2B07)}", ParseMode.Markdown, false, false, 0, keyboardMain);
                    break;
                case "/help":
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"*Список доступных команд:*

{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} /help \- просмотреть список команд
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} /start \- старт/презагрузка бота
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} /personality \- выбор личности
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} /reset \- перезапуск бота", ParseMode.MarkdownV2);
                    break;
                case "Выбор личности 👥":
                    Classes.MenuPersonality menuSelectPerson = new Classes.MenuPersonality(BotRoma, ApiKeyBot);
                    menuSelectPerson.ViewkeyBoardButton(sender, e);
                    break;
                case "Помощь ❔":
                    break;
                case "/personality":
                    Classes.MenuPersonality menuSelectPersonSecond = new Classes.MenuPersonality(BotRoma, ApiKeyBot);
                    menuSelectPersonSecond.ViewkeyBoardButton(sender, e);
                    break;
                case "/reset":
                    await BotRoma.SetWebhookAsync("");
                    var keyboardMainMenuRestart = new ReplyKeyboardMarkup
                    {
                        Keyboard = new[] {
                                                new[]
                                                {
                                                    new KeyboardButton($"Выбор личности {convertEmoji = new Emoji(0x1F465)}"),
                                                    new KeyboardButton($"Помощь {convertEmoji = new Emoji(0x2754)}")
                                                },
                                            },
                        ResizeKeyboard = true
                    };
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Привет - {message.From.FirstName}{convertEmoji = new Emoji(0x1F525)}
Теперь я снова в строю {convertEmoji = new Emoji(0x2705)}", ParseMode.Markdown, false, false, 0, keyboardMainMenuRestart);
                    await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"Тыкай на кнопочку {convertEmoji = new Emoji(0x2B07)}");
                    break;
            }
           
        }
    }
}
