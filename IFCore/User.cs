using System;
using System.Collections.Generic;

namespace IFCore
{
    public class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.UsersNotes = new HashSet<UserNotes>();
        }

        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual UserInfo UsersInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserNotes> UsersNotes { get; set; }
    }
}
