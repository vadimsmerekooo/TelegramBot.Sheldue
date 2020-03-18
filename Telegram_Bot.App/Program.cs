using System;
using Telegram_Bot.View;
using Telegram.Bot;
using System.ComponentModel;
using System.Net;
using NLog;

namespace Telegram_Bot.App
{
    class Program
    {
        private static BackgroundWorker bw;
        static void Main(string[] args)
        {
            Logger loggerMain = LogManager.GetCurrentClassLogger();
            loggerMain.Debug("Console Application: Status - Run");
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                bw = new BackgroundWorker();
                MainMenu menuLibriary = new MainMenu(Const.GetsetBot, Const.GetSetApiKey);
                bw.DoWork += menuLibriary.StartedMenu;
                Const.GetsetBot = new TelegramBotClient(Const.GetSetApiKey);
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync(Const.GetSetApiKey);
                    Console.WriteLine("Bot Roma started...");
                    loggerMain.Debug("Server: Status - Run");
                    loggerMain.Debug("Bot: Status - Run");
                }
                else
                {
                    Console.WriteLine("Bot Roma don't started");
                    loggerMain.Debug("Bot: Status - Stop");
                }
                Console.ReadKey(true);
            }
            catch
            {
                loggerMain.Fatal("Throw Exception in Main Console.App");
                loggerMain.Error("Bot: Status - Stop");
                loggerMain.Error("Server: Status - Stop");
                loggerMain.Debug("Close Main Console.App");
            }
        }
    }
}
