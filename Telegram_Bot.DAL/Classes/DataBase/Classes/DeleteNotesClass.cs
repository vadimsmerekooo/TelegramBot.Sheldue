using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Telegram_Bot.DAL.DataBase;

namespace Telegram_Bot.DAL.Classes.DataBase.Classes
{
    public class DeleteNotesClass
    {
        public async void DeleteOldNotes(DateTime firstDayOfWeek)
        {
            using (managerdbContext context = new managerdbContext())
            {
                try
                {
                    await context.UsersNotes.ForEachAsync(note => 
                    {
                        if (note.DateNote < firstDayOfWeek)
                        {
                            context.UsersNotes.Remove(note);
                        }
                    });
                    context.SaveChanges();
                }
                catch
                {

                }
            }
        }
    }
}
