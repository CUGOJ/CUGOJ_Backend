using CUGOJ.Backend.Share.Common.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Test.Grains.Organizations
{
    [TestClass]
    public class OrganizationGrainsTest
    {

        [TestMethod]
        public async Task TestOrganization()
        {
            await Common.CommonTest(async client =>
             {
                 var grain = client.GetGrain<IOrganizationGrain>(1);
                 if(grain == null)
                 {
                     throw new Exception("获取客户端失败");
                 }
             });
        }
    }
}
