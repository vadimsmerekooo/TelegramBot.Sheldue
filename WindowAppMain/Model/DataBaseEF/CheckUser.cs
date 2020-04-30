using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using WindowAppMain.Classes.WindowAuthClasses;
using WindowAppMain.Model.DataBaseEF.DBManagerbot;

namespace WindowAppMain.Model.DataBaseEF
{
    class CheckUser
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
                    await context.Users.ForEachAsync(user =>
                    {
                        if (user.Email == userLogin)
                        {
                            checkRangeUser = false;
                        }
                    });
                    //checkUsers = context.UsersInfo.Where(user => user.Email == userLogin).ToList();

                    return checkRangeUser;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SearchUser(string userLogin, string userPassword)
        {
            bool checkRangeUser = false;
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    await context.Users.ForEachAsync(user =>
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
                                        Login = user.Email,
                                        Name = userInfo[0].UserName,
                                        Status = userInfo[0].UserStatus
                                    }; break;
                                case "Студент":
                                    userListInformantion = new Person()
                                    {
                                        Login = user.Email,
                                        Name = userInfo[0].UserName,
                                        Status = userInfo[0].UserStatus,
                                        Department = userInfo[0].UserDepartment,
                                        Group = userInfo[0].UserGroup
                                    }; break;
                            }
                            //if(userInfo[0].UserStatus == "Преподаватель")
                            //{
                            //    userListInformantion = new Person()
                            //    {
                            //        Login = user.Email,
                            //        Name = userInfo[0].UserName,
                            //        Status = userInfo[0].UserStatus
                            //    };
                            //}
                            //if(userInfo[0].UserStatus == "Студент")
                            //{
                            //    userListInformantion = new Person()
                            //    {
                            //        Login = user.Email,
                            //        Name = userInfo[0].UserName,
                            //        Status = userInfo[0].UserStatus,
                            //        Department = userInfo[0].UserDepartment,
                            //        Group = userInfo[0].UserGroup
                            //    };
                            //}
                        }
                    });
                }
                return checkRangeUser;
            }
            catch
            {
                return false;
            }
        }
        public async void CollectionInformationUser(string userLogin)
        {
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    await context.Users.ForEachAsync(user =>
                    {
                        if (user.Email == userLogin)
                        {
                            var userInfo = context.UsersInfo.Where(userinf => userinf.ID == user.ID).ToList();
                            switch (userInfo[0].UserStatus)
                            {
                                case "Преподаватель":
                                    userListInformantion = new Person()
                                    {
                                        Login = user.Email,
                                        Name = userInfo[0].UserName,
                                        Status = userInfo[0].UserStatus
                                    }; break;
                                case "Студент":
                                    userListInformantion = new Person()
                                    {
                                        Login = user.Email,
                                        Name = userInfo[0].UserName,
                                        Status = userInfo[0].UserStatus,
                                        Department = userInfo[0].UserDepartment,
                                        Group = userInfo[0].UserGroup
                                    }; break;
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {

            }
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
    [Serializable()]
    public class Person
    {
        public string Name;
        public string Login;
        public string Status;
        public string Department;
        public string Group;

        // Пусто конструктор, необходимый для сериализации.
        public Person()
        {
        }

        // Инициализация конструктора.
        public Person(string login,
            string status, string department, string group)
        {
            Login = login;
            Status = status;
            Department = department;
            Group = group;
        }
        // Инициализация конструктора2.
        public Person(string name, string login,
            string status)
        {
            Name = name;
            Login = login;
            Status = status;
        }
    }
}
