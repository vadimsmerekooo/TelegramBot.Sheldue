using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WindowAppMain.Model.DataBaseEF.DBManagerbot;

namespace WindowAppMain.Model.DataBaseEF
{
    class DeleteNotesClass
    {
        public void DeleteOldNotes(DateTime firstDayOfWeek)
        {
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    foreach(var note in context.UsersNotes)
                    {
                        if (note.DateNote < firstDayOfWeek)
                        {
                            context.UsersNotes.Remove(note);
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
