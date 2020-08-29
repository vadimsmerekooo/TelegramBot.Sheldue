using System;

namespace IFCore
{
    public class MessageDelegate
    {
        public delegate void MethodMessage(string message);

        public event MethodMessage onMessage;

        public static bool SendMessageAdminPc = true;
    }
}
