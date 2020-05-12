using System;

namespace Telegram_Bot.DAL.Classes.DataBase.Classes
{
    public class DeleteNotesClass
    {
        public void DeleteOldNotes(DateTime firstDayOfWeek)
        {
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    foreach (var note in context.UsersNotes)
                    {
                        if (note.DateNote < firstDayOfWeek)
                        {
                            context.UsersNotes.Remove(note);
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch 
            {

            }
        }
    }
}
