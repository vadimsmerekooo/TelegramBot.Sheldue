using System;

namespace IFCore
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserStatus { get; set; }
        public string UserDepartment { get; set; }
        public string UserGroup { get; set; }

        public virtual User Users { get; set; }
    }
}
