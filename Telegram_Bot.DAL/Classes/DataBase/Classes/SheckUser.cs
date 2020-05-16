using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IFCore;
using Telegram_Bot.DAL.DataBase;

namespace Telegram_Bot.DAL.Classes.DataBase.Classes
{
    public class CheckUser
    {
        public Person userListInformantion;
        private CryptAndDecryptPassword cryptPassword = new CryptAndDecryptPassword();
        public async Task<bool> CheckExclusiveUser(string userLogin)
        {
            bool checkRangeUser = true;
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    foreach (var user in context.Users)
                    {
                        if (user.Email == userLogin)
                        {
                            checkRangeUser = false;
                        }
                    }
                    //checkUsers = context.UsersInfo.Where(user => user.Email == userLogin).ToList();

                    return checkRangeUser;
                }
            }
            catch 
            {
                return false;
            }
        }


        public async Task<bool> RegistrationUser(IFCore.User user, IFCore.UserInfo userInfo)
        {
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    Users userEF = new Users()
                    {
                        Email = user.Email,
                        Password = user.Password
                    };
                    UsersInfo userInfEF = new UsersInfo()
                    {
                        UserName = userInfo.UserName,
                        UserStatus = userInfo.UserStatus,
                        UserDepartment = userInfo.UserDepartment,
                        UserGroup = userInfo.UserGroup
                    };
                    context.Users.Add(userEF);
                    context.UsersInfo.Add(userInfEF);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }



        public bool SearchUser(string userLogin, string userPassword)
        {
            bool checkRangeUser = false;
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    foreach (var user in context.Users)
                    {
                        if (user.Email == userLogin && user.Password == cryptPassword.CalculateMD5Hash(userPassword).ToString())
                        {
                            checkRangeUser = true;
                            var userInfo = context.UsersInfo.Where(userinf => userinf.ID == user.ID).ToList();
                            switch (userInfo[0].UserStatus)
                            {
                                case "Преподаватель":
                                    userListInformantion = new Person()
                                    {
                                        ID = user.ID,
                                        Login = user.Email,
                                        Name = userInfo[0].UserName,
                                        Status = userInfo[0].UserStatus
                                    }; break;
                                case "Студент":
                                    userListInformantion = new Person()
                                    {
                                        ID = user.ID,
                                        Login = user.Email,
                                        Name = userInfo[0].UserName,
                                        Status = userInfo[0].UserStatus,
                                        Department = userInfo[0].UserDepartment,
                                        Group = userInfo[0].UserGroup
                                    }; break;
                            }
                        }
                    }
                }
                return checkRangeUser;
            }
            catch
            {
                return false;
            }
        }
        public Person CollectionInformationUser(string userLogin)
        {
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    foreach (var user in context.Users)
                    {
                        if (user.Email == userLogin)
                        {
                            var userInfo = context.UsersInfo.Where(userinf => userinf.ID == user.ID).ToList();
                            switch (userInfo[0].UserStatus)
                            {
                                case "Преподаватель":
                                    userListInformantion = new Person()
                                    {
                                        ID = user.ID,
                                        Login = user.Email,
                                        Name = userInfo[0].UserName,
                                        Status = userInfo[0].UserStatus
                                    }; break;
                                case "Студент":
                                    userListInformantion = new Person()
                                    {
                                        ID = user.ID,
                                        Login = user.Email,
                                        Name = userInfo[0].UserName,
                                        Status = userInfo[0].UserStatus,
                                        Department = userInfo[0].UserDepartment,
                                        Group = userInfo[0].UserGroup
                                    }; break;
                            }
                        }
                    }
                }
            }
            catch 
            {

            }
            return userListInformantion;
        }
        public void ChangePasswordUser(string userLogin, string userNewPassword)
        {
            using (var context = new managerdbContext())
            {
                var usersInfo = context.Users.SingleOrDefault(user => user.Email == userLogin);
                usersInfo.Password = cryptPassword.CalculateMD5Hash(userNewPassword).ToString();
                context.SaveChanges();
            }
        }
    }
}
