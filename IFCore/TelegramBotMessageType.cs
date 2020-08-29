using System;
using System.Xml.Serialization;
using Telegram.Bot.Types.Enums;

namespace IFCore
{
    [Serializable]
    public class TelegramBotMessageType
    {
        public string TextMessage { get; set; }
        public long ChatIdMessage { get; set; }
        public string ChatUserNameMessage { get; set; }
        public string ChatFirstName { get; set; }
        public string ChatLastName { get; set; }
        public MessageType  ChatMessageType { get; set; }


        public static TelegramBotMessageType GetMessageType(Telegram.Bot.Types.Message message)
        {
            return new TelegramBotMessageType()
            {
                TextMessage = message.Text,
                ChatIdMessage = message.Chat.Id,
                ChatUserNameMessage = message.Chat.Username,
                ChatFirstName = message.Chat.FirstName,
                ChatLastName = message.Chat.LastName,
                ChatMessageType = message.Type
            };
        }


        public static XmlSerializer serialize = new XmlSerializer(
            typeof(TelegramBotMessageType),
            new XmlRootAttribute() { ElementName = "MessageAdmin" });

    }
}
