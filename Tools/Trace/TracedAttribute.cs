using CUGOJ.Backend.Tools.Params;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Tools.Trace
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class TracedAttribute : OnMethodBoundaryAspect
    {
        private static ActivitySource activitySource = new ActivitySource(Config.ServiceName);
        public string ClassName { get; set; } = "Unkown";
        public override void OnEntry(MethodExecutionArgs args)
        {
            Activity? activity = activitySource.StartActivity(ClassName);
            activity?.SetTag("Env", Config.Env);
            activity?.SetTag("ServiceID", Config.ServiceID);
            activity?.SetTag("Method", args.Method.Name);
            activity?.SetStatus(ActivityStatusCode.Ok);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Activity.Current?.Stop();
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Activity.Current?.SetStatus(ActivityStatusCode.Error);
        }

    }
}
