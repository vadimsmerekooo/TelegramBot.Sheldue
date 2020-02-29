using Telegram.Bot;
using Telegram.Bot.Types;

namespace studentassistant_bot.Models.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "/start";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            await client.SendTextMessageAsync(chatId, "Hello!", replyToMessageId: messageId);
        }
    }
}