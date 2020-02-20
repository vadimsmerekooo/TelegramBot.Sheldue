using System;
using Telegram_Bot.View;
using Telegram.Bot;
using System.ComponentModel;
using System.Net;

namespace Telegram_Bot.App
{
    class Program
    {
        //904575664:AAHC4zbtgASlNF1GFxde8oFTXk1kaWHhppM
        private static BackgroundWorker bw;
        static void Main(string[] args)
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
            }
            else
            {
                Console.WriteLine("Bot Roma don't started");
            }
            Console.ReadKey(true);
        }
    }
}
