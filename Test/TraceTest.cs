using CUGOJ.Backend.Tools;
using CUGOJ.Backend.Tools.Trace;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace CUGOJ.Backend.Test
{
    [TestClass]
    public class TraceTest
    {
        [TestMethod]
        public void ActivityTest()
        {
            TracedClass testClass = new TracedClass();
            testClass.Func1();
        }
        
        [TestMethod]
        public void OpenTelemetryTest()
        {
            var serviceName = "Backend";
            using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddSource(serviceName)
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault().AddService(serviceName: serviceName, serviceVersion:"1.0.0"))
                .AddConsoleExporter()
                .Build();
            TracedClass testClass = new TracedClass();
            testClass.Func1();
        }

        [TestMethod]
        
        public void TraceContainerTest()
        {
            Config.InitConfig(new string[] { "-debug" });
            var container = new TestContainer();
            container.Func1();
        }
    }
    

    [Traced(ClassName ="TracedClass")]
    class TracedClass
    {
        public void Func1()
        {
            var activity = Activity.Current;
            if (activity == null)
                Console.WriteLine("null");
            else Console.WriteLine(activity.TraceId.ToString());
        }
    }

    [TraceContainer]
    class TestContainer
    {
        public void Func1()
        {
            TracedClass testClass = new();
            testClass.Func1();
        }
    }
}