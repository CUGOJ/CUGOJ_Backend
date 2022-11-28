using CUGOJ.Backend.Base.DAO.Context;
using CUGOJ.Backend.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Test.Base.DAO
{
    [TestClass]
    public class CUGOJContestTest
    {
        [TestMethod]
        public void TestContest()
        {
            Config.InitConfig(Common.CommonConfig, true);
            using var context = new CUGOJContext();
            context.Tags.ToList();
        }
    }
}
