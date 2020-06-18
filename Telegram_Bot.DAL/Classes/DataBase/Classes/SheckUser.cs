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
        public bool CheckExclusiveUser(string userLogin)
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
                    context.Users.Add(new Users()
                    {
                        Email = user.Email,
                        Password = user.Password
                    });
                    context.UsersInfo.Add(new UsersInfo()
                    {
                        UserName = userInfo.UserName,
                        UserStatus = userInfo.UserStatus,
                        UserDepartment = userInfo.UserDepartment,
                        UserGroup = userInfo.UserGroup
                    });
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
            userPassword = new CryptAndDecryptPassword().CalculateMD5Hash(userPassword).ToString();
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    var userInfo = context.UsersInfo.FirstOrDefault(userinf => userinf.ID == (context.Users.FirstOrDefault(user => user.Email == userLogin && user.Password == userPassword) as Users).ID);
                    if (userInfo != null)
                    {
                        checkRangeUser = true;
                        userListInformantion = new Person()
                        {
                            ID = (context.Users.FirstOrDefault(user => user.Email == userLogin && user.Password == userPassword) as Users).ID,
                            Login = (context.Users.FirstOrDefault(user => user.Email == userLogin && user.Password == userPassword) as Users).Email,
                            Name = userInfo.UserName,
                            Status = userInfo.UserStatus,
                            Department = userInfo.UserDepartment,
                            Group = userInfo.UserGroup
                        };
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
                    var userInfo = context.UsersInfo.FirstOrDefault(userinf => userinf.ID == (context.Users.FirstOrDefault(user => user.Email == userLogin) as Users).ID);

                    userListInformantion = new Person()
                    {
                        ID = (context.Users.FirstOrDefault(user => user.Email == userLogin) as Users).ID,
                        Login = (context.Users.FirstOrDefault(user => user.Email == userLogin) as Users).Email,
                        Name = userInfo.UserName,
                        Status = userInfo.UserStatus,
                        Department = userInfo.UserDepartment,
                        Group = userInfo.UserGroup
                    };
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
                context.Users.FirstOrDefault(user => user.Email == userLogin).Password = new CryptAndDecryptPassword().CalculateMD5Hash(userNewPassword).ToString();
                context.SaveChanges();
            }
        }

        public bool GetPasswordUser(string userLogin, string password)
        {
            using (managerdbContext context = new managerdbContext())
            {
                var user = context.Users.Where(login => login.Email == userLogin).ToList()[0];
                return user.Password == new CryptAndDecryptPassword().CalculateMD5Hash(password).ToString() ? true : false;
            }
        }
    }
}
