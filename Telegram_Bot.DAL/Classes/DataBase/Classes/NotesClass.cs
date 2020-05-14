using System;
using System.Collections.Generic;
using System.Linq;
using Telegram_Bot.DAL.DataBase;

namespace Telegram_Bot.DAL.Classes.DataBase.Classes
{
    public class NotesClass
    {
        public bool AddNewNoteUser(int userid, string textNote, DateTime dateDayNote, string para, int paraNumber)
        {
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    UsersNotes userNotes = new UsersNotes()
                    {
                        IDUser = userid,
                        DateNote = dateDayNote,
                        Para = para,
                        ParaNumber = paraNumber,
                        NoteText = textNote
                    };
                    context.UsersNotes.Add(userNotes);
                    context.SaveChanges();
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool ChangeNoteUser(int idNote, string changeText)
        {
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    var noteUser = context.UsersNotes.SingleOrDefault(note => note.IDNotes == idNote);
                    noteUser.NoteText = changeText;
                    context.SaveChanges();
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public List<UsersNotes> listNote;
        public bool GetAllNotesUser(int idUser)
        {
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    listNote = context.UsersNotes.Where(note => note.IDUser == idUser).ToList();
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }
        //public bool ChangeNoteUser()
        //{

        //}
    }
}
