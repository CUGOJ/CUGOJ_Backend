using CUGOJ.Backend.Tools.Infra.HttpProvider;
using CUGOJ.Backend.Tools.Log;
using CUGOJ.Backend.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Test.Base.Infra
{
    [TestClass]
    public class HttpProviderTest
    {
        [TestMethod]
        public async Task TestGet()
        {
            var provider = new HttpProvider(new Logger());
            await provider.Get<string>("https://www.baidu.com/:path",new Dictionary<string, string>
            {
                {"path","s"},
                {"wd","123"}
            });
        }

        [TestMethod]
        public async Task TestPost ()
        {
            Config.AllowUnsafeSSL = true;
            var provider = new HttpProvider(new Logger());
            await provider.Post<Dictionary<string, string>>("http://1.15.93.80:11451/api/conf/getconfig", "default");
         
        }
    }
}
