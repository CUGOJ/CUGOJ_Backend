using CUGOJ.Grains.Common;
using CUGOJ.Grains.Common.Problem;
using CUGOJ.Tools;
using CUGOJ.Share.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Test.Grains.CommonGrain
{
    [TestClass]
    public class ProblemGrainTest
    {
        [TestMethod]
        public void TestTimerLock()
        {
            ContestProblemPo p1 = new();
            ContestProblemPo p2 = new();

            Console.WriteLine(p1.GetHashCode());
            Console.WriteLine(p2.GetHashCode());
        }

        [TestMethod]
        public void TestObject()
        {
            object obj = new ProblemGrainBase();
            Console.WriteLine(((dynamic)obj).Semaphore);
            ((dynamic)obj).Semaphore = new SemaphoreSlim(1, 1);

            Console.WriteLine(((dynamic)obj).Semaphore);
        }
    }
}
