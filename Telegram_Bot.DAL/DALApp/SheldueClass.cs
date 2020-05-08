using System;
using System.Collections.Generic;
using Telegram_Bot.DAL.Interfaces;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Telegram_Bot.DAL.DALApp
{
    public class SheldueClass : IGetSheldue
    {
        string _department;
        string _group;
        Dictionary<string, string> departmnetListReferensToFile = new Dictionary<string, string>()
        {
            {"Информационное", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Information.docx"},
            {"Швейное", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Sewing.docx"},
            {"Электромеханическое", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\ElektroMechanic.docx"},
            {"Машиностроения", @"C:\Users\vadim\Desktop\GitHub\Student_Bot_Assistant\Telegram_Bot.DAL\Classes\Student\FileWord\ListShedule\Mechanic.docx"}
        };



        public void GetListSheldue(string department, string group)
        {
            _department = (department.Split(' '))[0];
            _group = group;
            ParseWordFileMethod();
        }


        private void ParseWordFileMethod()
        {
            DocX document = DocX.Load(departmnetListReferensToFile[_department]);
            try
            {
                foreach (Table tabel in document.Tables)
                {
                    
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                document.Dispose();
            }
        }
    }
}
