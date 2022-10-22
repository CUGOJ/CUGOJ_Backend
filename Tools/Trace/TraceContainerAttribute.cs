using CUGOJ.Backend.Tools.Params;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using PostSharp.Aspects;
using OpenTelemetry.Exporter;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Tools.Trace
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class TraceContainerAttribute :OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            var builder = Sdk.CreateTracerProviderBuilder()
                .AddSource(Config.ServiceName)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: Config.ServiceName, serviceVersion: Config.ServiceVersion));
            if (!string.IsNullOrEmpty(Config.TraceAddress))
            {
                builder.AddJaegerExporter(opt =>
                {
                    opt.Endpoint = new Uri(Config.TraceAddress);
                    opt.Protocol = JaegerExportProtocol.HttpBinaryThrift;
                });
            }
            if(Config.Debug)
            {
                builder.AddConsoleExporter();
            }
            var traceProvider = builder.Build();
        }
    }
}
