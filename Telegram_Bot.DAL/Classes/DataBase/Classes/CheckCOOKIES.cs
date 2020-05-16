using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram_Bot.DAL.DataBase;

namespace Telegram_Bot.DAL.Classes.DataBase.Classes
{
    public class CheckCOOKIES
    {
        public bool GetCOOKIESUser(string login)
        {
            using (managerdbContext context = new managerdbContext())
            {
                var checkUserInDB = context.Users.Where(userLogin => userLogin.Email == login).ToList();
                return checkUserInDB.Count != 0 ? true : false;
            }
        }
    }
}
