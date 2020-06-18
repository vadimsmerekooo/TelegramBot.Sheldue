using System;
using Telegram_Bot.View;
using Telegram.Bot;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Collections.Generic;
using IFCore;
using System.Timers;
using System.Xml.Serialization;
using Telegram_Bot.View.Classes.Menu;
using System.Globalization;
using System.Security.Policy;
using System.Net.NetworkInformation;
using System.Linq;
using System.Runtime.InteropServices;

namespace Telegram_Bot.App
{
    class Program
    {
        private static BackgroundWorker bw;
        private static Dictionary<string, List<SheldueAllDaysTelegram>> allSheldue;
        private static Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> changeSheldue;
        private static Dictionary<string, List<SheldueAllDaysTelegram>> allSheldueCopy;
        private static List<int> idMessageClients;
        private static List<int> idMessageClientsBlackList;
        private static List<IFCore.DictionaryList> idMessageClientsWarningList;
        private static XmlSerializer serializer = new XmlSerializer(typeof(List<int>), new XmlRootAttribute() { ElementName = "MessageChatIdClients" });
        private static XmlSerializer serializerDictionary = new XmlSerializer(typeof(IFCore.DictionaryList), new XmlRootAttribute() { ElementName = "MessageChatIdClients" });
        private static string weekCheck = string.Empty;
        private static System.Timers.Timer timerChangesSheldue = new System.Timers.Timer(200000);//200000
        static string dayOldSheldue = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

        private static void TimerIntervalParseFile(object sender, ElapsedEventArgs e)
        {
            if (bw == null && bw.IsBusy == true)
            {
                timerChangesSheldue.Elapsed -= TimerIntervalParseFile;
                timerChangesSheldue.Stop();
            }
            try
            {
                var newSheldueAtTimer = new View.Classes.GetShelduePL().GetChangesSheldue(out weekCheck);
                if (newSheldueAtTimer.Keys == null)
                    return;
                ICollection<string> keys = newSheldueAtTimer.Keys;
                if (keys != null && !keys.Contains(dayOldSheldue.ToLower()))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nПрисутствуют замены к расписанию!");
                    Console.ResetColor();
                    allSheldue = allSheldueCopy;
                    if (newSheldueAtTimer != null)
                    {
                        allSheldue = ChangeMainSheldueWithNewSheldue(allSheldue, newSheldueAtTimer);
                        MainMenu.SetSheldue = allSheldue;
                        Program.dayOldSheldue = keys.ToArray()[0];
                        new SendAlertAllUsers(MainMenu.GetBot, MainMenu.GetApi, idMessageClients, allSheldue).AlertMessage("⚠️🚨 На сайте появились замены к расписанию 🌐 Узнай свое новое расписание на завтра ⚡");
                        Console.WriteLine("\nОпопвещение о новом расписании выполняется!");
                    }
                }
            }
            catch
            {

            }
        }

        static void Main()
        {
            Console.Title = "Запуск Telegram бота: бот Рома";
            Console.ForegroundColor = ConsoleColor.Green;
            PrintCenterText("Консоль запуска бота!\n");
            Console.ResetColor();
            IFCore.IFCore.loggerMain.Debug("Start Application Bot");
            try
            {
                using (FileStream fs = new FileStream("ListIdMessageChatClients.xml", FileMode.Open))
                {
                    idMessageClients = new List<int>();
                    idMessageClients = (List<int>)serializer.Deserialize(fs);
                }
                using (FileStream fs = new FileStream("BlackListIdMessageChatClients.xml", FileMode.Open))
                {
                    idMessageClientsBlackList = new List<int>();
                    idMessageClientsBlackList = (List<int>)serializer.Deserialize(fs);
                }
                using (FileStream fs = new FileStream("WarningListIdMessageChatClients.xml", FileMode.Open))
                {
                    idMessageClientsWarningList = new List<IFCore.DictionaryList>();
                    idMessageClientsWarningList = ((List<IFCore.DictionaryList>)serializerDictionary.Deserialize(fs));
                }
            }
            catch
            {

            }
            DefaultlPrint();
            Console.ReadKey();
        }


        private static void DefaultlPrint()
        {
            string bwDontNull = "Выберите команду:\n1. Запуск бота\n2. Просмотреть логи\n3. Проверить статус бота\n4. Очистить терминал\n5. Удалить все логи\n6. Вывести пользователей бота\n7. Оповестить всех пользователей\n0. Выход\n";
            string bwNull = "Выберите команду:\n1.Запуск бота\n2.Просмотреть логи\n3.Проверить статус бота\n4.Очистить терминал\n5.Удалить все логи\n6.Вывести пользователей бота\n0.Выход\n";

            Console.WriteLine(bw != null && bw.IsBusy != true ? bwDontNull : bwNull);
            try
            {
                Console.Write("Ваш выбор -> ");
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        #region Start Bot
                        IPStatus status = IPStatus.TimedOut;
                        try { status = new Ping().Send(@"google.by").Status; } catch { }
                        if (status != IPStatus.Success)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nОтсутсвует подключение к интренету!\n");
                            Console.ResetColor();
                            DefaultlPrint();
                            break;
                        }
                        if (!SetPassword())
                        {
                            Console.WriteLine("Неверный пароль!");
                            DefaultlPrint();
                            break;
                        }
                        Console.WriteLine("\nЗагрузка расписания. Не закрывайте консоль!!!");
                        allSheldue = new Telegram_Bot.View.Classes.GetShelduePL().GetSheldueAllGroup();
                        allSheldueCopy = allSheldue;
                        if (allSheldue != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Расписание загружено\n");
                            Console.ResetColor();
                            Console.WriteLine("Загрузка файла, изменение к расписанию, с сайта\n");
                            changeSheldue = new View.Classes.GetShelduePL().GetChangesSheldue(out weekCheck);
                            if (changeSheldue != null)
                            {
                                allSheldue = ChangeMainSheldueWithNewSheldue(allSheldue, changeSheldue);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Изменение к расписанию загружено\n");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Изменение к расписанию не загружено\n");
                                Console.ResetColor();
                            }
                            StartBotMethod();

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Расписание не загружено. Бот не будет запущен!\n");
                            Console.ResetColor();
                        }
                        DefaultlPrint();
                        break;
                    #endregion
                    case 2:
                        #region Show logs
                        Console.WriteLine("1. Вывести все логи\n2. Вывести DEBUG логи\n3. Вывести ERROR, FATAL логи\n4. Вывести остальные логи\n0. Отмена");
                        Console.Write("Ваш выбор -> ");
                        switch (int.Parse(Console.ReadLine()))
                        {
                            case 1:
                                string[] allLogs = File.ReadAllLines("../../Resource/NLog/Logs_project.log");
                                if (allLogs == null)
                                {
                                    Console.WriteLine("Файл с логами пуст!");
                                    break;
                                }
                                foreach (var item in allLogs)
                                {
                                    if (item.Contains("DEBUG"))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(item);
                                        Console.ResetColor();
                                    }
                                    if (item.Contains("ERROR") || item.Contains("FATAL"))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        PrintCenterText(item);
                                        Console.ResetColor();
                                    }
                                    if (!item.Contains("DEBUG") && !item.Contains("ERROR") && !item.Contains("FATAL"))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        PrintCenterText(item);
                                        Console.ResetColor();
                                    }
                                }
                                break;
                            case 2:
                                PrintLogs("DEBUG");
                                break;
                            case 3:
                                PrintLogs("ERROR");
                                break;
                            case 4:
                                PrintLogs("WARN");
                                break;
                            case 0:
                                break;
                        }
                        DefaultlPrint();
                        break;
                    #endregion
                    case 3:
                        #region Check bot status
                        if (bw != null && bw.IsBusy != true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Бот запущен\n");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Бот не запущен\n");
                            Console.ResetColor();
                        }
                        DefaultlPrint();
                        break;
                    #endregion
                    case 4:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        PrintCenterText("Консоль запуска бота!\n");
                        Console.ResetColor();
                        DefaultlPrint();
                        break;
                    case 5:
                        Console.Write("Вы действительно хотите удалить ВСЕ логи?! ( Y/N ) => ");
                        if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                        {
                            try { File.WriteAllText("../../Resource/NLog/Logs_project.log", string.Empty); } catch { Console.WriteLine("Ошибка при очистке файла!"); }
                        }
                        DefaultlPrint();
                        break;
                    case 6:
                        Console.WriteLine("1. Список всех пользователей\n2. Черный список пользователей\n0. Отмена");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                Console.WriteLine("Список пользователей!");
                                Console.WriteLine(File.ReadAllText("ListIdMessageChatClients.xml"));
                                break;
                            case 2:
                                Console.WriteLine("Список пользователей!");
                                Console.WriteLine(File.ReadAllText("BlackListIdMessageChatClients.xml"));
                                break;
                        }
                        DefaultlPrint();
                        break;
                    case 7:
                        #region Send message at user
                        if (bw != null && bw.IsBusy != true)
                        {
                            Console.Write("1. Сообщение c изображения\n2. Сообщение без изображения\n0. Отмена\nВаш выбор => ");
                            switch (Convert.ToInt32(Console.ReadLine()))
                            {
                                case 1:
                                    Console.Write("Введите текст, для оповещения, в одну строку => ");
                                    string allertTextWithPhoto = Console.ReadLine();
                                    Console.Write("Введите ссылку на фотографию => ");
                                    string allertTextWithPhotoRef = Console.ReadLine();
                                    if (String.IsNullOrEmpty(allertTextWithPhotoRef))
                                        break;
                                    WebRequest request = WebRequest.Create(allertTextWithPhotoRef);
                                    try
                                    {
                                        HttpWebResponse res = request.GetResponse() as HttpWebResponse;

                                        if (res.StatusDescription == "OK")
                                        {
                                            Console.Write("Вы действительно хотите оповестить всех?! ( Y/N ) => ");
                                            if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                                            {
                                                new SendAlertAllUsers(MainMenu.GetBot, MainMenu.GetApi, idMessageClients, allSheldue).AlertMessage(allertTextWithPhoto, allertTextWithPhotoRef);
                                                Console.WriteLine("Сообщения отправляются!/");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ссылка не валидна!");
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    break;
                                case 2:
                                    Console.Write("Введите текст, для оповещения, в одну строку => ");
                                    string allertText = Console.ReadLine();
                                    Console.Write("Вы действительно хотите оповестить всех?! ( Y/N ) => ");
                                    if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                                    {
                                        new SendAlertAllUsers(MainMenu.GetBot, MainMenu.GetApi, idMessageClients, allSheldue).AlertMessage(allertText);
                                        Console.WriteLine("Сообщения отправляются!/");
                                    }
                                    break;
                            }
                        }
                        DefaultlPrint();
                        break;
                    #endregion
                    case 0:
                        if (SetPassword())
                            Environment.Exit(0);
                        else
                        {
                            Console.WriteLine("\nНе верный пароль!");
                            DefaultlPrint();
                        }
                        break;
                    default:
                        DefaultlPrint();
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введен не верный формат!\n");
                    Console.ResetColor();
                }
                DefaultlPrint();
            }
        }

        private static void PrintCenterText(string text)
        {
            var width = Console.WindowWidth;
            var padding = width / 2 + text.Length / 2;
            Console.WriteLine("{0," + padding + "}", text);
        }
        private static void PrintLogs(string modif)
        {
            string[] allLogs = File.ReadAllLines("../../Resource/NLog/Logs_project.log");
            foreach (var item in allLogs)
            {
                if (item.Contains(modif) && modif == "DEBUG")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item);
                    Console.ResetColor();
                }
                else
                if (item.Contains(modif) && modif == "ERROR" || modif == "FATAL")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    PrintCenterText(item);
                    Console.ResetColor();
                }
                else
                if (!item.Contains("DEBUG") && !item.Contains("ERROR") && !item.Contains("FATAL"))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    PrintCenterText(item);
                    Console.ResetColor();
                }
            }
        }

        private static bool StartBotMethod()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                bw = new BackgroundWorker();
                MainMenu menuLibriary = new MainMenu(Const.GetsetBot, Const.GetSetApiKey, ref allSheldue);
                MainMenu.Week = weekCheck;
                bw.DoWork += menuLibriary.StartedMenu;
                Const.GetsetBot = new TelegramBotClient(Const.GetSetApiKey);
                if (bw.IsBusy != true)
                {
                    if (idMessageClients != null)
                        menuLibriary.idMessageClients = idMessageClients;
                    if (idMessageClientsBlackList != null)
                        menuLibriary.idMessageClientsBlackList = idMessageClientsBlackList;
                    if (idMessageClientsWarningList != null)
                        menuLibriary.idMessageClientsWarn = idMessageClientsWarningList;
                    bw.RunWorkerAsync(Const.GetSetApiKey);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Бот запущен...\n");
                    Console.ResetColor();
                    IFCore.IFCore.loggerMain.Debug("Bot: Status - Run");
                    timerChangesSheldue.Elapsed += TimerIntervalParseFile;
                    timerChangesSheldue.Start();
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Бот не запущен\n");
                    Console.ResetColor();
                    IFCore.IFCore.loggerMain.Debug("Bot: Status - Stop");
                    DefaultlPrint();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: " + ex.ToString());
                Console.ResetColor();
                IFCore.IFCore.loggerMain.Fatal("Throw Exception in Main Console.App");
                IFCore.IFCore.loggerMain.Error("Bot: Status - Stop");
                DefaultlPrint();
                return false;
            }
        }
        private static bool SetPassword()
        {
            string pass = "";
            Console.Write("\nВведите пароль: ");
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            return pass != "Roma" ? false : true;
        }
        public static Dictionary<string, List<SheldueAllDaysTelegram>> ChangeMainSheldueWithNewSheldue(
                      Dictionary<string, List<SheldueAllDaysTelegram>> mainSheldue,
                      Dictionary<string, Dictionary<string, List<SheldueAllDaysTelegram>>> shangeSheldues)
        {
            try
            {
                foreach (var changeSheldueItem in shangeSheldues)
                {
                    foreach (var changeSheldueItemValue in changeSheldueItem.Value)
                    {
                        foreach (var itemMain in mainSheldue)
                        {
                            if (changeSheldueItemValue.Key.ToLower() == itemMain.Key.Split(' ')[1].ToLower())
                            {
                                foreach (var itemChangeValue in changeSheldueItemValue.Value)
                                {
                                    foreach (var itemMainValue in itemMain.Value)
                                    {
                                        if (itemChangeValue.DayName.ToLower() == itemMainValue.DayName.ToLower())
                                        {
                                            try
                                            {
                                                itemMainValue.Day[0].ChangeSheldue = itemChangeValue.Day[0]?.ChangeSheldue;
                                                if (itemMainValue.Day[0].Para1 != null)
                                                    itemMainValue.Day[0].Para1[0] = null;
                                                if (itemMainValue.Day[0].Para2 != null)
                                                    itemMainValue.Day[0].Para2[0] = null;
                                                if (itemMainValue.Day[0].Para3 != null)
                                                    itemMainValue.Day[0].Para3[0] = null;
                                                if (itemMainValue.Day[0].Para4 != null)
                                                    itemMainValue.Day[0].Para4[0] = null;
                                                if (itemMainValue.Day[0].Para5 != null)
                                                    itemMainValue.Day[0].Para5[0] = null;
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
               
            }
            return mainSheldue;
            //try
            //{
            //    itemMainValue.Day[0].ChangeSheldue = itemChangeValueValue.Day[0]?.ChangeSheldue;
            //    if (itemMainValue.Day[0].Para1 != null)
            //        itemMainValue.Day[0].Para1[0] = null;
            //    if (itemMainValue.Day[0].Para2 != null)
            //        itemMainValue.Day[0].Para2[0] = null;
            //    if (itemMainValue.Day[0].Para3 != null)
            //        itemMainValue.Day[0].Para3[0] = null;
            //    if (itemMainValue.Day[0].Para4 != null)
            //        itemMainValue.Day[0].Para4[0] = null;
            //    if (itemMainValue.Day[0].Para5 != null)
            //        itemMainValue.Day[0].Para5[0] = null;
            //}
            //catch
            //{
            //    continue;
            //}
        }
    }
}