using CUGOJ.Test;
using CUGOJ.Tools.Common;
using CUGOJ.Share.Common.Organizations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Test.Grains.Organizations
{
    [TestClass]
    public class OrganizationGrainsTest
    {

        private async Task InitOrganization(IOrganizationGrain grain)
        {
            for (int i = 1; i <= 1000; i++)
            {
                await grain.UpdateOrganizationInfo(new Share.Common.Models.OrganizationPoBase
                {
                    Id = i,
                    OrganizationType = Share.Common.Models.OrganizationPo.OrganizationTypeEnum.Normal,
                    Parent = i >> 1,
                    Role = 1,
                    Version = CommonTools.GetUTCUnixMilli()
                });
            }
            for (int i = 1; i <= 1000; i++)
            {
                int joinNum = Random.Shared.Next(10);
                while (joinNum-- > 0)
                    await grain.JoinOrganization(new List<long> { i }, Random.Shared.NextInt64(1000), 2);
            }
        }

        [TestMethod]
        public async Task TestOrganization()
        {
            await Common.CommonTest(async client =>
             {
                 var grain = client.GetGrain<IOrganizationGrain>(1);
                 if (grain == null)
                 {
                     throw new Exception("获取客户端失败");
                 }
                 await InitOrganization(grain);
                 var res = await grain.GetOrganizationByOrg(1000);
                 Console.WriteLine(CommonTools.ToJsonString(res));
             });
        }

        [TestMethod]
        public async Task TestUser()
        {
            await Common.CommonTest(async client =>
            {
                var grain = client.GetGrain<IOrganizationGrain>(1);
                if (grain == null)
                {
                    throw new Exception("获取客户端失败");
                }
                await InitOrganization(grain);
                Stopwatch watch = new();
                watch.Start();
                for (int i = 0; i < 1000; i++)
                {
                    var res = await grain.GetOrganizationByUser(Random.Shared.NextInt64(1000));
                    Console.WriteLine(CommonTools.ToJsonString(res));
                }
                watch.Stop();
                Console.WriteLine($"耗时:{watch.ElapsedMilliseconds} ms ,平均耗时:{watch.ElapsedMilliseconds / 1000.0}ms");
            });
        }

        [TestMethod]
        public async Task TestErrorUpdate()
        {
            await Common.CommonTest(async client =>
            {
                var grain = client.GetGrain<IOrganizationGrain>(1);
                if (grain == null)
                {
                    throw new Exception("获取客户端失败");
                }
                try
                {
                    await InitOrganization(grain);
                    await grain.UpdateOrganizationInfo(new Share.Common.Models.OrganizationPoBase
                    {
                        Id = 312,
                        OrganizationType = Share.Common.Models.OrganizationPo.OrganizationTypeEnum.Normal,
                        Parent = 625,
                        Role = 1,
                        Version = CommonTools.GetUTCUnixMilli()
                    });

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
    }
}
