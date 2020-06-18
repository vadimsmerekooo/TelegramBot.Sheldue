﻿using IFCore;
using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram_Bot.View.Interface;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Telegram_Bot.View.Classes.Menu
{
    public class ListCallsLessons: MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;
        public ListCallsLessons(TelegramBotClient Bot, string api, Dictionary<string, List<SheldueAllDaysTelegram>> sheldue) : base(Bot, api, ref sheldue)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
        }

        public async void SendMessageCallLesson(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            string callsLessonTextChetDay = $@"Расписание звонков *2019\/2020*, 2 полугодие

*Понеделник, Среда, Пятница*
1,3 курс *ПТО*, 2 курс *ПТО, ССО*
                 *По 45 минуту*
*1\.*               9\.00 \-  9\.45
                  9\.55 \- 10\.40
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*2\.*              10\.50 \- 11\.35
                 11\.45 \- 12\.30
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*3\.**3\.1*      12\.40 \| *Обед*   12\.30
               13\.25 \|                13\.00
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
 *Обед*   13\.25 \| *3\.1*   13\.00
               13\.55 \|          13\.45
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
   *3\.2*       13\.55 \| *3\.2*   13\.55
                14\.40 \|          14\.40
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*4\.*            14\.50 \- 15\.35
               15\.45 \- 16\.30
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*5\.*            16\.40 \- 17\.25
               17\.35 \- 18\.20
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*6\.*            18\.30 \- 19\.15
               19\.25 \- 20\.10";
            string callsLessonTextNoChetDay = $@"Расписание звонков 2019\/2020, 2 полугодие

*Вторник, Четверг*
1,3 курс *ПТО*, 2 курс *ПТО, ССО*
                 *По 45 минуту*

*1\.*               9\.00 \-  9\.45
                  9\.55 \- 10\.40
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*2\.*              10\.50 \- 11\.35
                 11\.45 \- 12\.30
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*3\.**3\.1*      12\.40 \| *Обед*   12\.30
               13\.25 \|                13\.00
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
 *Обед*   13\.25 \| *3\.1*   13\.00
               13\.55 \|          13\.45
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
   *3\.2*       13\.55 \| *3\.2*   13\.55
                14\.40 \|          14\.40
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
Кур\.\/         14\.50 \- 15\.35
Инф\.Час
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*4\.*            15\.45 \- 16\.30
               16\.40 \- 17\.25
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*5\.*            17\.35 \- 18\.20
               18\.30 \- 19\.15
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*6\.*            19\.25 \- 20\.10
               20\.20 \- 21\.05";
            string callsLessonTextSurthday = $@"Расписание звонков 2019\/2020, 2 полугодие

*Суббота*
        *По 45 минуту*
*1\.*               9\.00 \-  9\.45
                  9\.55 \- 10\.40
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*2\.*              10\.50 \- 11\.35
                 11\.45 \- 12\.30
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*3\.*              12\.45 \| 13\.30
                 13\.40 \| 14\.25
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*4\.*              14\.35 \- 15\.20
                 15\.30 \- 16\.15
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_
*5\.*              16\.25 \- 17\.10
                 17\.20 \- 18\.05";
            try { await BotRoma.SendTextMessageAsync(message.Chat.Id, callsLessonTextChetDay, ParseMode.MarkdownV2, replyMarkup: new Keyboards().Personality()); } catch { }
            try { await BotRoma.SendTextMessageAsync(message.Chat.Id, callsLessonTextNoChetDay, ParseMode.MarkdownV2, replyMarkup: new Keyboards().Personality()); } catch { }
            try { await BotRoma.SendTextMessageAsync(message.Chat.Id, callsLessonTextSurthday, ParseMode.MarkdownV2, replyMarkup: new Keyboards().Personality()); } catch { }
        }
    }
}
