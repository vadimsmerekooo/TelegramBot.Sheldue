using System;
using Telegram_Bot.View;
using Telegram.Bot;
using System.ComponentModel;
using System.Net;
using NLog;
using System.IO;

namespace Telegram_Bot.App
{
    class Program
    {
        private static BackgroundWorker bw;
        private static Logger loggerMain = LogManager.GetCurrentClassLogger();
        private static MainMenu menuLibriary = new MainMenu(Const.GetsetBot, Const.GetSetApiKey);
        static void Main(string[] args)
        {
            loggerMain.Debug("Console Application: Status - Run");
            Console.ForegroundColor = ConsoleColor.Green;
            PrintCenterText("Консоль запуска бота!\n");
            Console.ResetColor();
            DefaultlPrint();
            Console.ReadKey();
        }

        private static void DefaultlPrint()
        {
            Console.WriteLine("Выберите команду:\n1. Запуск бота\n2. Просмотреть логи\n3. Проверить статус бота\n4. Очистить терминал\n5. Выход\n");
            try
            {
                Console.Write("Ваш выбор -> ");
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        StartBotMethod();
                        DefaultlPrint();
                        break;
                    case 2:
                        Console.WriteLine(File.ReadAllText("../../Resource/NLog/Logs_project.log"));
                        DefaultlPrint();
                        break;
                    case 3:
                        if (bw.IsBusy != true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Бот запущен\n");
                            Console.ResetColor();
                        }
                        DefaultlPrint();
                        break;
                    case 4:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        PrintCenterText("Консоль запуска бота!\n");
                        Console.ResetColor();
                        DefaultlPrint();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }
            }
            catch(Exception ex)
            {
                if(ex == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введен не верный формат!\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Бот не запущен\n");
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


        private static void StartBotMethod()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                bw = new BackgroundWorker();
                bw.DoWork += menuLibriary.StartedMenu;
                Const.GetsetBot = new TelegramBotClient(Const.GetSetApiKey);
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync(Const.GetSetApiKey);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bot Roma started...\n");
                    Console.ResetColor();
                    loggerMain.Debug("Server: Status - Run");
                    loggerMain.Debug("Bot: Status - Run");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bot Roma don't started\n");
                    Console.ResetColor();
                    loggerMain.Debug("Bot: Status - Stop");
                    DefaultlPrint();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("error: " + ex);
                Console.ResetColor();
                loggerMain.Fatal("Throw Exception in Main Console.App");
                loggerMain.Error("Bot: Status - Stop");
                loggerMain.Error("Server: Status - Stop");
                DefaultlPrint();
            }
        }
    }
}
