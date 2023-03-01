using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools
{
    public class TimeoutDictionary<K,V> where K: notnull
    {
        private record TimeoutKey
        {
            DateTime time;

        }

        private readonly ConcurrentDictionary<K, V> dic = new();
        private readonly SortedList timeoutList = SortedList.Synchronized(new SortedList());

        
        public bool Push(K key,V value)
        {
            return dic.TryAdd(key, value);
        }

        public bool Push(K key,V value,TimeSpan time)
        {
            if (!dic.TryAdd(key, value))
                return false;
            timeoutList.Add(DateTime.Now.Add(time), key);
            return true;
        }
    }
}
