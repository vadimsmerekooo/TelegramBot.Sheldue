using System;
using System.Collections.Generic;

namespace IFCore
{
    [Serializable]
    public class GetSheldueDic
    {
        public string Name { get; set; }
        public List<SheldueAllDaysTelegram> Sheldue { get; set; }
        public GetSheldueDic()
        {

        }
    }
}
