using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IFCore;
using Telegram_Bot.DAL.Classes.DataBase.Classes;

namespace Telegram_Bot.BL.Classes.App
{
    public class ReferenseDALClass
    {
        public IFCore.Person userListInformantion;
        public bool SetConnectionDBCheckUser(string login, string password)
        {
            CheckUser user = new CheckUser();
            bool userMeh = Task.Run(() => user.SearchUser(login, password)).Result;

            userListInformantion = user.userListInformantion as IFCore.Person;
            return userMeh;
        }

        public async Task<bool> SetConnectionDBCheckExcluziveUser(string login)
        {
            CheckUser user = new CheckUser();
            return await user.CheckExclusiveUser(login);
        }

        public bool SetConnectionDBCheckCOOKIESUser(string login)
        {
            CheckCOOKIES cookies = new CheckCOOKIES();
            return cookies.GetCOOKIESUser(login);
        }

        public async Task<bool> SetConnectionDBRegUser(User user, UserInfo userInfo)
        {
            CheckUser checkUser = new CheckUser();
            return await checkUser.RegistrationUser(user, userInfo);
        }

        public bool SetConnectionDBNoteClass(int noteUserID, string noteText)
        {
            NotesClass noteClass = new NotesClass();
            return noteClass.ChangeNoteUser(noteUserID, noteText);
        }
        public bool SetConnectionDBNoteAdd(int userID, string newTextNote, DateTime date, string work, int para)
        {
            NotesClass noteClass = new NotesClass();
            return noteClass.AddNewNoteUser(userID, newTextNote, date, work, para);
        }
        public List<UserNotes> SetConnectionDBSelectAll(int userID)
        {
            NotesClass noteClass = new NotesClass();
            noteClass.GetAllNotesUser(userID);
            List<UserNotes> listNote = new List<UserNotes>();
            foreach (var item in noteClass.listNote)
            {
                listNote.Add(item as UserNotes);
            }
            return listNote;
        }
        public void SetConnectionDBDeleteNote(DateTime day)
        {
            DeleteNotesClass delNotes = new DeleteNotesClass();
            delNotes.DeleteOldNotes(day);
        }
        public void SetConnectionDBGetSheldue(string department, string group)
        {
            Telegram_Bot.DAL.Interfaces.IGetSheldue getWordSheldue = new Telegram_Bot.DAL.DALApp.SheldueClass();
            getWordSheldue.GetListSheldue(department, group);
        }
        public void SetConnectionDBCollectionInformationUser(string login)
        {
            CheckUser checkUserClass = new CheckUser();
            userListInformantion = checkUserClass.CollectionInformationUser(login) as Person;
        }
        public void SetConnectionDBChangePassword(string login, string password)
        {
            CheckUser checkUser = new CheckUser();
            checkUser.ChangePasswordUser(login, password);
        }
    }
}
