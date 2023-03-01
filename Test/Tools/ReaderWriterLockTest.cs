using CUGOJ.Tools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Test.Tools
{
    [TestClass]
    public class ReaderWriterLockTest
    {
        public class TestClass
        {
            [ReaderWriterLock(ReaderWriterLockAttribute.LockTypeEnum.Read)]
            public void TestMethod()
            {
                throw new Exception("Test");
            }
        }

        [TestMethod]
        public void TestThrow()
        {
            TestClass a = new();
            try
            {

                a.TestMethod();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
