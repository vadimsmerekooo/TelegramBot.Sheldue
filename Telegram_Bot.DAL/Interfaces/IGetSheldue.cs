using IFCore;
using System;
using System.Collections.Generic;

namespace Telegram_Bot.DAL.Interfaces
{
    public interface IGetSheldue
    {
        List<SheldueAllDays> GetListSheldue(string department, string group, List<UserNotes> notes, List<DateTime> dateTime);
        List<SheldueAllDays> GetListSheldue(string name, List<UserNotes> notes, List<DateTime> dateTime);
    }
}
