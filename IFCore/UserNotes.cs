using System;

namespace IFCore
{
    public class UserNotes
    {
        public int IDNotes { get; set; }
        public int IDUser { get; set; }
        public System.DateTime DateNote { get; set; }
        public string Para { get; set; }
        public int ParaNumber { get; set; }
        public string NoteText { get; set; }

        public virtual User Users { get; set; }
    }
}
