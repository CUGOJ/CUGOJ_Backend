using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUGOJ.Test;
using CUGOJ.Tools;

namespace CUGOJ.Test.Base.Infra
{
    [TestClass]
    public class ConfigProviderTest
    {
        [TestMethod]
        public async Task TestStringConfig()
        {
            await Common.CommonTest(async () =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine(Config.TraceAddress);
                    Thread.Sleep(500);
                }
            });
        }
    }
}
