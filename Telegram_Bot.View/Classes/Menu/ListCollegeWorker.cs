using System;
using System.Collections.Generic;
using IFCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram_Bot.View.Interface;
using Telegram.Bot.Types.Enums;

namespace Telegram_Bot.View.Classes.Menu
{
    public class ListCollegeWorker: MainMenu, IMenu
    {
        private TelegramBotClient BotRoma;
        private string ApiKeyBot;

        public ListCollegeWorker(TelegramBotClient Bot, string api, Dictionary<string, List<IFCore.SheldueAllDaysTelegram>> sheldue) : base(Bot, api, ref sheldue)
        {
            BotRoma = Bot;
            ApiKeyBot = api;
        }
        public override async void SendMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            string listWorkerCollege = $@"Сотрудники нашего колледжа👨‍💼

*Руководство*
*1\. Минько Галина Здиславовна*
Заместитель директора по хозяйственной работе
*2\. Чапля Наталья Васильевна*
Заведующий отделением машиностроения
*3\. Кривопуст Елена Евгеньевна*
Заведующий электромеханическим отделением
*4\. Вильчик Наталья Михайловна*
Заведующий швейным отделением
*5\. Купич Екатерина Викторовна*
Заведующий ресурсным центром
*6\. Максимчик Елена Мечиславовна*
Главный бухгалтер

*Отдел методической работы*
*1\. Лебедь Татьяна Михайловна*
Методист

*Преподаватели*
*1\. Алейникова Мария Петровна*
Преподаватель специальных дисциплин
*2\. Алексейченко Инна Валерьевна*
Преподаватель специальных дисциплин
*3\. Воронко Людмила Александровна*
Преподаватель специальных дисциплин
*4\. Карпович Татьяна Яновна*
Преподаватель физвоспитания
*5\. Качан Екатерина Ивановна*
Преподаватель географии и истории
*6\. Киреня Оксана Павловна*
Преподаватель математики
*7\. Лавкель Юлия Викторовна*
Преподаватель специальных дисциплин
*8\. Левицкая Людмила Валерьевна*
Преподаватель
*9\. Лис Наталья Николаевна*
Преподаватель иностранного языка
*10\. Лукошко Валентина Игнатовна*
Преподаватель психологии
*11\. Можейко Елена Анатольевна*
Преподаватель специальных дисциплин
*12\. Назарчук Татьяна Николаевна*
Преподаватель специальных дисциплин
*13\. Равбуть Людмила Александровна*
Преподаватель экономики
*14\. Снацкая Ирина Ивановна*
Преподаватель белорусского языка и литературы
*15\. Толочко Павел Станиславович*
Преподаватель специальных дисциплин
*16\. Шкута Оксана Георгиевна*
Преподаватель немецкого языка
*17\. Юргель Елена Александровна*
Преподаватель специальных дисциплин

*Мастера производственного обучения*
*1\. Котович Наталья Сергеевна*
Мастер производственного обучения
*2\. Сакович Оксана Иановна*
Мастер производственного обучения
*3\. Скачкова Елена Евгеньевна*
Мастер производственного обучения
*4\. Трайгель Виктория Валентиновна*
Мастер производственного обучения
*5\. Фахрутдинова Алина Яковлевна*
Мастер производственного обучения";
            await BotRoma.SendTextMessageAsync(message.Chat.Id, @"[*Шока Светлана Сергеевна* \- Директор😎](http://ggkttd.by/wp-content/uploads/2016/01/%D0%A8%D0%BE%D0%BA%D0%B0-%D0%A1.%D0%A1..jpg)", parseMode: ParseMode.MarkdownV2);
            await BotRoma.SendTextMessageAsync(message.Chat.Id, @"[*Анисько Роман Иосифович* \- Заместитель директора по учебно\-производственной работе😎](http://ggkttd.by/wp-content/uploads/2018/11/%D0%90%D0%BD%D0%B8%D1%81%D1%8C%D0%BA%D0%BE-%D0%A0%D0%BE%D0%BC%D0%B0%D0%BD-%D0%98%D0%BE%D1%81%D0%B8%D1%84%D0%BE%D0%B2%D0%B8%D1%87-1.jpg)",parseMode: ParseMode.MarkdownV2);
            await BotRoma.SendTextMessageAsync(message.Chat.Id, @"[*Ващилова Наталья Иосифовна* \- Заведующий информационным отделением😎](http://ggkttd.by/wp-content/uploads/2016/11/%D0%92%D0%B0%D1%89%D0%B8%D0%BB%D0%BE%D0%B2%D0%B0-%D0%9D%D0%B0%D1%82%D0%B0%D0%BB%D1%8C%D1%8F-%D0%98%D0%BE%D1%81%D0%B8%D1%84%D0%BE%D0%B2%D0%BD%D0%B0.jpg)", parseMode: ParseMode.MarkdownV2);
            await BotRoma.SendTextMessageAsync(message.Chat.Id, @"[*Шиманович Таисия Самсоновна* \- Заместитель директора по учебно\-методической работе😎](http://ggkttd.by/wp-content/uploads/2016/11/%D0%A8%D0%B8%D0%BC%D0%B0%D0%BD%D0%BE%D0%B2%D0%B8%D1%87-%D0%A2%D0%B0%D0%B8%D1%81%D0%B8%D1%8F-%D0%A1%D0%B0%D0%BC%D1%81%D0%BE%D0%BD%D0%BE%D0%B2%D0%BD%D0%B0.jpg)", parseMode: ParseMode.MarkdownV2);
            await BotRoma.SendTextMessageAsync(message.Chat.Id, @"[*Волоткевич Карина Леонидовна* \- Заместитель директора по учебно\-воспитательной работе😎](http://ggkttd.by/wp-content/uploads/2016/11/%D0%92%D0%BE%D0%BB%D0%BE%D1%82%D0%BA%D0%B5%D0%B2%D0%B8%D1%87-%D0%9A%D0%B0%D1%80%D0%B8%D0%BD%D0%B0-%D0%9B%D0%B5%D0%BE%D0%BD%D0%B8%D0%B4%D0%BE%D0%B2%D0%BD%D0%B0.jpg)", parseMode: ParseMode.MarkdownV2);
            await BotRoma.SendTextMessageAsync(message.Chat.Id, @"[*Сазанкова Маргарита Сергеевна* \- Заместитель директора по производственному обучению😎](http://ggkttd.by/wp-content/uploads/2016/11/%D0%A1%D0%B0%D0%B7%D0%B0%D0%BD%D0%BA%D0%BE%D0%B2%D0%B0-%D0%9C%D0%B0%D1%80%D0%B3%D0%B0%D1%80%D0%B8%D1%82%D0%B0-%D0%A1%D0%B5%D1%80%D0%B3%D0%B5%D0%B5%D0%B2%D0%BD%D0%B0.jpg)", parseMode: ParseMode.MarkdownV2);
            await BotRoma.SendTextMessageAsync(message.Chat.Id, listWorkerCollege, parseMode: ParseMode.MarkdownV2, replyMarkup: new Keyboards().Personality());
        }
    }
}
