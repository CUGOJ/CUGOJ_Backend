using CUGOJ.Tools.Infra;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Common
{

    [PSerializable]
    [AttributeUsage(AttributeTargets.Method)]
    public class TimerLockAttribute : OnMethodBoundaryAspect
    {
        private class TimerLock
        {
            public SemaphoreSlim Semaphore = new(1, 1);
            public DateTime LastTick = DateTime.MinValue;
        }

        private TimeSpan _timeSpan = new TimeSpan(0, 0, 10);

        private static InstanceHashTable<TimerLock> semaphores = new();
        private static Dictionary<string, TimerLock> namedSemaphores = new();
        public string? Key { get; set; }
        public TimerLockAttribute(long milliseconds = 0)
        {
            if (milliseconds != 0)
                _timeSpan = new TimeSpan(0, 0, 0, 0, (int)milliseconds);
        }

        private TimerLock GetSemophore(object obj)
        {
            if (Key != null)
            {
                if (namedSemaphores.TryGetValue(Key, out TimerLock? value))
                    return value;
                else
                {
                    var sem = new TimerLock();
                    namedSemaphores.Add(Key, sem);
                    return sem;
                }
            }
            else
            {
                var semaphore = semaphores.GetItem(obj);
                if (semaphore is null)
                {
                    semaphore = new TimerLock();
                    semaphores.PushItem(obj, semaphore);
                }
                return semaphore;
            }
        }

        private TimerLock? GetSemaphoreWithoutCreate(object obj)
        {
            if (Key != null)
            {
                if (namedSemaphores.TryGetValue(Key, out TimerLock? value))
                    return value;
                else
                    return null;
            }
            else
            {
                return semaphores.GetItem(obj);
            }
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var semaphore = GetSemophore(args.Instance);
            if (DateTime.Now - semaphore.LastTick > _timeSpan)
            {
                if (semaphore.Semaphore.Wait(0))
                {
                    semaphore.LastTick = DateTime.Now;
                }
                else
                {
                    args.FlowBehavior = FlowBehavior.Return;
                }
            }
            else
            {
                args.FlowBehavior = FlowBehavior.Return;
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            GetSemaphoreWithoutCreate(args.Instance)?.Semaphore.Release();
            base.OnExit(args);
        }


    }
}
