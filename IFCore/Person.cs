using System;

namespace IFCore
{
    [Serializable()]
    public  class Person
    {
        public int ID;
        public string Name;
        public string Login;
        public string Status;
        public string Department;
        public string Group;

        // ПустоЙ конструктор, необходимый для сериализации.
        public Person()
        {
        }

        // Инициализация конструктора.
        public Person(int id, string login,
            string status, string department, string group)
        {
            ID = id;
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
