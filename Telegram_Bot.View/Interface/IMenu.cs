using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Telegram_Bot.View.Interface
{
    interface IMenu
    {
        void SendMessage(object sender, MessageEventArgs e);
    }
}
