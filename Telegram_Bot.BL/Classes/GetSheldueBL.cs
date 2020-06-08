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
        public Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> GetChangesSheldue(out string week)
        {
            return new Telegram_Bot.DAL.Classes.GetShledueFolder.GetChangesForSheldue().GetChangesSheldue(out week);
        }        
    }
}
