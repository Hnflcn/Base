using System;
using System.Collections.Generic;
using System.Linq;

namespace _Main.Scripts._Base.SaveSystem
{
    [Serializable]
    public class TupleList<T1, T2>
    {
        [Serializable]
        public struct SerializableTuple
        {
            public T1 item1;
            public T2 item2;
        }

        public List<SerializableTuple> tuples = new List<SerializableTuple>();
        
        public void Add(Tuple<T1, T2> tuple)
        {
            tuples.Add(new SerializableTuple { item1 = tuple.Item1, item2 = tuple.Item2});
        }

        public List<Tuple<T1, T2>> ToList()
        {
            return tuples.Select(t => new Tuple<T1, T2>(t.item1, t.item2)).ToList();
        }
    }

}