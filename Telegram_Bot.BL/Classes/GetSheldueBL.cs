using IFCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Telegram_Bot.BL.Classes
{
    public class GetSheldueBL
    {
        public Dictionary<string, List<SheldueAllDaysTelegram>> GetSheldueAllGroup()
        {
            return new Telegram_Bot.DAL.Classes.GetShledueFolder.GetSheldueDAL().GetSheldueAllGroup();
        }
        public Dictionary<string, List<SheldueAllDaysTelegram>> GetChangesSheldue(out string week, out string dayNewSheldue)
        {
            return new Telegram_Bot.DAL.Classes.GetShledueFolder.GetChangesForSheldue().GetChangesSheldue(out week, out dayNewSheldue);
        }        
    }
}
