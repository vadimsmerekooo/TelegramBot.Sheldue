using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot.View.Classes.Menu.PiarClasses
{
    class PiarInstagram : MainMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;

        public PiarInstagram(TelegramBotClient Bot, string api) : base(Bot, api)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
        }
        public async void InstagramDeveloper(MessageEventArgs e)
        {
            var message = e.Message;
            var keyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("Инстаграм","https://www.instagram.com/tvoy_prostoy_malchik/?hl=ru"),
                            InlineKeyboardButton.WithUrl("Вконтакте","https://vk.com/lovebonsticks"),
                        }
                    });
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"[Поддержи моего, молодого, создателя {convertEmoji = new Emoji(0x1F474)}🖤](https://www.instagram.com/p/B8gbrHrnmto/)", ParseMode.Markdown, replyMarkup: keyboard);
            
        }
    }
}
