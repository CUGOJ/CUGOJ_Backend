using CUGOJ.Grains.Common;
using CUGOJ.Share.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Test.Base.Infra
{
    [TestClass]
    public class TimerLockAttributeTest
    {
        [TestMethod]
        public void TestTimerlock()
        {
            A a = new();
            A b = new B();
            a.Func2();
            b.Func2();
        }

        class A
        {
            public virtual void Func1()
            {
                Console.WriteLine("Father");
            }

            public void Func2()
            {
                Func1();
            }
        }

        class B : A
        {
            public override void Func1()
            {
                Console.WriteLine("Son");
            }
        }

    }
}
