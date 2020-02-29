using System;
using Telegram.Bot.Args;

namespace Telegram_Bot.View.Interface
{
    public interface IStepsForWorkFile
    {
        void ViewListGroups(object sender, MessageEventArgs e);
    }
    public interface IStepsForWorkFileInList
    {
        void NextStepSelectDay(object sender, MessageEventArgs e, string groupName);
    }
}
