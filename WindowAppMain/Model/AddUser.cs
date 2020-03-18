using System.Data.Entity;

namespace WindowAppMain.Model
{
    class AddUser : DbContext
    {
        public AddUser() : base("ConnectionToDBUser")
        {

        }
        public DbSet<Users> User { get; set; }
    }
}
