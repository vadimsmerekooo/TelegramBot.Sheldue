using IFCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram_Bot.BL;

namespace Telegram_Bot.View.Classes
{
    public class GetShelduePL
    {
        public Dictionary<string, List<SheldueAllDaysTelegram>> GetSheldueAllGroup()
        {
            return new Telegram_Bot.BL.Classes.GetSheldueBL().GetSheldueAllGroup();
        }
        public Dictionary<string, List<SheldueAllDaysTelegram>> GetChangesSheldue(out string week, out string dayNewSheldue)
        {
            return new Telegram_Bot.BL.Classes.GetSheldueBL().GetChangesSheldue(out week, out dayNewSheldue);
        }
    }
}
