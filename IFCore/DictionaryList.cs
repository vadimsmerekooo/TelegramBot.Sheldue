using System;

namespace IFCore
{
    [Serializable]
    public class DictionaryList
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public DictionaryList()
        {

        }
        public DictionaryList(int id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}
