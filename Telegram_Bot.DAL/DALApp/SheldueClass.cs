using System;
using System.Collections.Generic;
using Telegram_Bot.DAL.Interfaces;
using Microsoft.Office.Interop.Word;

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
            Application app = new Application();
            string group = $"ГРУППА {_group.Replace(" ", "")}";
            Document doc = app.Documents.Open(departmnetListReferensToFile[_department], Visible: false);
            try
            {
                foreach (Table table in doc.Tables)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            try
                            {
                                string textCells = table.Cell(i, j).Range.Text.Replace("\r\a", "").Replace("\r", "");
                                if (textCells == group)
                                {
                                    if (j > 4)
                                    {
                                        GetRightColumnSheldue(table, j);
                                    }
                                    else
                                    {
                                        GetLeftColumnSheldue(table, j);
                                    }
                                }
                                //if (table.Cell(i, j).Range.Text.Replace("\r\a", "") == group)
                                //{

                                //}
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            catch
            {
                doc.Close();
                app.Quit();
            }
            finally
            {
                doc.Close();
                app.Quit();
            }
        }


        private void GetRightColumnSheldue(Table table, int columnj)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = columnj; j < table.Columns.Count; j++)
                {

                }
            }
        }
        private void GetLeftColumnSheldue(Table table, int columnj)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = columnj; j < table.Columns.Count; j++)
                {

                }
            }
        }
    }
}
