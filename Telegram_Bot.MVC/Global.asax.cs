using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using studentassistant_bot.Models;
using System.Net;
using System.ComponentModel;
using Telegram.Bot;
using Telegram_Bot.View;

namespace studentassistant_bot
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static BackgroundWorker bw;
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            bw = new BackgroundWorker();
            MainMenu menuLibriary = new MainMenu(Const.GetsetBot, Const.GetSetApiKey);
            bw.DoWork += menuLibriary.StartedMenu;
            Const.GetsetBot = new TelegramBotClient(Const.GetSetApiKey);
            bw.RunWorkerAsync(Const.GetSetApiKey);
        }
    }
}
