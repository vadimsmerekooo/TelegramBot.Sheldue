using System;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot.View.Classes.Menu
{
    class Keyboards
    {
        Emoji convertEmoji;
        public ReplyKeyboardMarkup Personality()
        {
            var keyboardMainMenu = new ReplyKeyboardMarkup
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
            return keyboardMainMenu;
        }

        public string Help()
        {
            return $@"*Список доступных команд:*

{convertEmoji = new Emoji(new int[] { 0x0031, 0x20E3 })} /help \- просмотреть список команд
{convertEmoji = new Emoji(new int[] { 0x0032, 0x20E3 })} /start \- старт/презагрузка бота
{convertEmoji = new Emoji(new int[] { 0x0033, 0x20E3 })} /personality \- выбор личности
{convertEmoji = new Emoji(new int[] { 0x0034, 0x20E3 })} /reset \- перезапуск бота
{convertEmoji = new Emoji(new int[] { 0x0035, 0x20E3 })} /contacts \- контакты с разработчиком";
        }
    }
}
