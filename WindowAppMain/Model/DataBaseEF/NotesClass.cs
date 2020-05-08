using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using WindowAppMain.Model.DataBaseEF.DBManagerbot;
using System.Data.Entity.Migrations;

namespace WindowAppMain.Model.DataBaseEF
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
            catch(Exception ex)
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
            catch (Exception exx)
            {
                return false;
            }
        }


        public List<UsersNotes> GetAllNotesUser(int idUser)
        {
            try
            {
                using (managerdbContext context = new managerdbContext())
                {
                    return context.UsersNotes.Where(note => note.IDUser == idUser).ToList();
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        //public bool ChangeNoteUser()
        //{

        //}
    }
}
