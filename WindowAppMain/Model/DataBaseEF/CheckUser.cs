using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using WindowAppMain.Classes.WindowAuthClasses;

namespace WindowAppMain.Model.DataBaseEF
{
    class CheckUser
    {
        public List<string> userInfoList = new List<string>();
        public async Task<bool> CheckExclusiveUser(string userLogin)
        {
            try
            {
                using (managerbotDBContext context = new managerbotDBContext())
                {
                    List<string> checkUsers = new List<string>();
                    await context.UsersInfo.ForEachAsync(user =>
                    {
                        if (user.Email == userLogin)
                        {
                            checkUsers.Add(user.Email);
                        }
                    });
                    return checkUsers.Count == 0 ? true : false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> SearchUser(string userLogin, string userPassword)
        {
            bool userRange = false;
            try
            {
                using (managerbotDBContext context = new managerbotDBContext())
                {
                    CryptAndDecryptPassword cryptPassword = new CryptAndDecryptPassword(); 
                    await context.UsersInfo.ForEachAsync(user =>
                    {
                        if (user.Email == userLogin && user.Password == cryptPassword.CalculateMD5Hash(userPassword).ToString())
                        {
                            if(user.StatusUser == "Преподаватель")
                            {
                                userRange = true;
                                userInfoList.Add(user.Name);
                                userInfoList.Add(user.Email);
                                userInfoList.Add(user.StatusUser);
                            }
                            else
                            {
                                if(user.StatusUser == "Студент")
                                {
                                    userRange = true;
                                    userInfoList.Add(user.Name);
                                    userInfoList.Add(user.Email);
                                    userInfoList.Add(user.StatusUser);
                                    userInfoList.Add(user.Department);
                                    userInfoList.Add(user.DepartmentGroup);
                                }
                            }
                        }
                    });
                }
                return userRange ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}
