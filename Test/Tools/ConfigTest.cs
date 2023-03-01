using CUGOJ.Test;
using CUGOJ.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Test.Tools
{
    [TestClass]
    public class ConfigTest
    {
        [TestMethod]
        public async Task TestInit()
        {
            await Common.CommonTest(() => Task.Delay(1000));
        }

        [TestMethod]
        public void TestAttr()
        {
            Type t1 = typeof(Config);
            Type t2 = typeof(Config);
            PropertyInfo p1 = t1.GetProperty("Debug")!;
            PropertyInfo p2 = t2.GetProperty("Debug")!;
            PropertyInfo p3 = t1.GetProperty("Env")!;
            var a1 = Attribute.GetCustomAttributes(p1);
            var a2 = Attribute.GetCustomAttributes(p2);
            var a3 = Attribute.GetCustomAttributes(p3);
            if (a1[0] == a2[0])
            {
                Console.WriteLine("Yes");
            }
            if (a1[0].TypeId.Equals(a2[0].TypeId))
            {
                Console.WriteLine("Equals");
            }
            if (a1[0].TypeId.Equals(a3[0].TypeId))
            {
                Console.WriteLine("Error");
            }
        }
    }
}
