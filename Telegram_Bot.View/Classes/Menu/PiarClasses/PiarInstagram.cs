using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.View.Interface;

namespace Telegram_Bot.View.Classes.Menu.PiarClasses
{
    class PiarInstagram : MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue;
        public PiarInstagram(TelegramBotClient Bot, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue) : base(Bot, api, ref sheldue)
        {
            this.BotRoma = Bot;
            this.ApiKeyBot = api;
            this.sheldue = sheldue;
        }
        public async void SendMessagePiarInst(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            var keyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("Инстаграм","https://www.instagram.com/p/BzJgm4KI0Fv/?utm_source=ig_web_copy_link"),
                            InlineKeyboardButton.WithUrl("Вконтакте","https://vk.com/lovebonsticks"),
                        }
                    });
            await BotRoma.SendTextMessageAsync(message.Chat.Id, $@"[Поддержи моего, молодого, создателя {new Emoji(0x1F474)}🖤](https://www.instagram.com/p/B8gbrHrnmto/)", ParseMode.Markdown, replyMarkup: keyboard);
            
        }
    }
}
