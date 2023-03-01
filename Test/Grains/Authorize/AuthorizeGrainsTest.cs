using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Models;
using CUGOJ.Tools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Test.Grains.Authorize
{
    [TestClass]
    public class AuthorizeGrainsTest
    {
        [TestMethod]
        public async Task Test1()
        {
            await Common.CommonTest(async client =>
            {
                var grain = client.GetGrain<IAuthorizeGrain>(1);
            //    await grain.UpdateAuthorize(new List<AuthorizePoBase>()
            //{
            //    new()
            //    {
            //        GranteeType = AuthorizePo.GranteeTypeEnum.User,
            //        GranteeId = 1,
            //        Role = 0,
            //        Action = 2,
            //        ResourceType = AuthorizePo.ResourceTypeEnum.Problem,
            //        ResourceId = 1
            //    }
            //});
                Console.WriteLine(CommonTools.ToJsonString(await grain.QueryAuthorize(new QueryAuthorizeRequest()
                {
                    Grantees = new List<AuthorizeGrantee>()
                {
                    new AuthorizeGrantee()
                    {
                        GranteeId = 1,
                        GranteeType = AuthorizePo.GranteeTypeEnum.User,
                        Role = 0
                    }
                },
                    Resources = new List<AuthorizeResource>()
                {
                    new AuthorizeResource()
                    {
                        ResourceId = 1,
                        ResourceType = AuthorizePo.ResourceTypeEnum.Problem
                    }
                },
                })));
                Console.WriteLine(CommonTools.ToJsonString(await grain.QueryAuthorize(new QueryAuthorizeRequest()
                {
                    Grantees = new List<AuthorizeGrantee>()
                {
                    new AuthorizeGrantee()
                    {
                        GranteeId = 1,
                        GranteeType = AuthorizePo.GranteeTypeEnum.User,
                        Role = 0
                    }
                },
                    ResourceType = AuthorizePo.ResourceTypeEnum.Problem
                })));
                Console.WriteLine(CommonTools.ToJsonString(await grain.QueryAuthorize(new QueryAuthorizeRequest()
                {
                    Grantees = new List<AuthorizeGrantee>()
                {
                    new AuthorizeGrantee()
                    {
                        GranteeId = 1,
                        GranteeType = AuthorizePo.GranteeTypeEnum.User,
                        Role = 0
                    }
                },
                    Resources = new List<AuthorizeResource>()
                {
                    new AuthorizeResource()
                    {
                        ResourceId = 2,
                        ResourceType = AuthorizePo.ResourceTypeEnum.Problem
                    }
                },
                })));
            });

        }
    }
}
