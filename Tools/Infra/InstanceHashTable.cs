using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Infra
{
    public class InstanceHashTable<T>
    {
        private readonly ConcurrentDictionary<InstanceHashItem, T> dic = new();
        public void PushItem(object key, T value)
        {
            var item = new InstanceHashItem(key);
            dic.TryRemove(item, out var _);
            dic.TryAdd(item, value);
        }
        public T? GetItem(object key)
        {
            var item = new InstanceHashItem(key);
            dic.TryGetValue(item, out var value);
            return value;
        }
    }
    public class InstanceHashItem
    {
        private int hashCode;
        private WeakReference weakReference;
        public InstanceHashItem(object obj)
        {
            hashCode = obj.GetHashCode();
            weakReference = new WeakReference(obj);
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public object? Target { get => weakReference.Target; }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is InstanceHashItem @item)
            {
                object? target1 = weakReference.Target, target2 = item.Target;
                if (target1 is null || target2 is null) return false;
                return target1.Equals(target2);
            }
            return base.Equals(obj);
        }
    }
}
