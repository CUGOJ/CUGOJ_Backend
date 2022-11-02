using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Tools.Common
{

    [PSerializable]
    [AttributeUsage(AttributeTargets.Method)]
    public class TimerLockAttribute : OnMethodBoundaryAspect
    {

        private TimeSpan _timeSpan = new TimeSpan(0, 0, 10);
        private SemaphoreSlim semphore = new(0, 1);
        private DateTime lastTick = DateTime.Now;
        public TimerLockAttribute(long milliseconds = 0)
        {
            if (milliseconds != 0)
                _timeSpan = new TimeSpan(0, 0, 0, 0, (int)milliseconds);
        }
      
        public override void OnEntry(MethodExecutionArgs args)
        {
            if ((DateTime.Now-lastTick)>_timeSpan)
            {
                if (semphore.Wait(0))
                {
                    lastTick = DateTime.Now;
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
            semphore.Release();
            base.OnExit(args);
        }


    }
}
