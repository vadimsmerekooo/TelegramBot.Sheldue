using System;
using Telegram.Bot.Args;

namespace Telegram_Bot.View.Interface
{
    public interface IStepsOnMenu
    {
        void ListDay(object sender, MessageEventArgs e, string department);
    }
}
