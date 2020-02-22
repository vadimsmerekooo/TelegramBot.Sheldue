using System;
using System.Collections.Generic;
using WordText = Microsoft.Office.Interop.Word;


namespace Telegram_Bot.DAL.Classes.Student
{
    public class ParseWord
    {
        WordText.Application word = new WordText.Application();
        WordText.Document doc = new WordText.Document();
        

        public void SelectGroupFile()
        {
            string rowbuf = string.Empty;
            Console.WriteLine("Запуск метода ParseText");
            try
            {
                Object missing = System.Reflection.Missing.Value;
                Object confConv = false;
                Object readOnly = true;
                Object isVisible = false;
                Object saveChanges = false;
                Object filename = @"C:\Users\vadim\Desktop\Проекты и тд\Visual Studio проекты\botforraspis\botforraspis\File\Word\Information\Informatsionnoe_otdelenie1.docx";
                //Object filename = @"C:\Users\vadim\Desktop\Проекты и тд\Visual Studio проекты\botforraspis\botforraspis\File\Word\doxcfile.docx";


                doc = word.Documents.Open(ref filename, ref confConv, ref readOnly);
                word.Visible = false;
                WordText.Table tbl = doc.Tables[1];
                string row = string.Empty;
                int date = 3;
                List<string> listdate = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                DateTime dt = DateTime.Now;
                string daate = dt.DayOfWeek.ToString();
                for (int i = 0; i < listdate.Count; i++)
                {
                    if (daate == listdate[i])
                    {
                        if (i == listdate.Count)
                        {
                            daate = listdate[0];
                        }
                        else
                        {
                            if (daate == listdate[5])
                            {
                                daate = listdate[0];
                            }
                            else
                                daate = listdate[i + 1];
                            break;
                        }
                    }
                }
                int rowdate = 4;
                if (daate == "Monday") date = 3;
                if (daate == "Tuesday") date = 7;
                if (daate == "Wednesday") date = 11;
                if (daate == "Thursday") date = 15;
                if (daate == "Friday") date = 19;
                if (daate == "Saturday") { date = 23; rowdate = 2; }
                rowbuf = daate.ToString() + @"
";
                try
                {
                    for (int i = date; i < date + rowdate; i++)
                    {
                        for (int j = 6; j < 10; j++)
                        {
                            try
                            {
                                if (tbl.Cell(i, j).Range.Text != "\r\a")
                                {
                                    row = tbl.Cell(i, j).Range.Text;
                                    row = row.Replace("\r", " ");
                                    rowbuf += row.ToLower();
                                    if (j == 9) rowbuf += @"
";
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
                catch
                {

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                doc.Close();
                word.Quit();
                Console.WriteLine(rowbuf);
            }
        }
    }
}
