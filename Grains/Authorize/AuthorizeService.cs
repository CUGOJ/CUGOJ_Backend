using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.DAO;
using CUGOJ.Tools.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.Authorize
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IClusterClient client;

        private IAuthorizeGrain GetAuthorizeGrain => client.GetGrain<IAuthorizeGrain>(1);
        public AuthorizeService(IClusterClient client)
        {
            this.client = client;
        }

        public Task<bool> CheckProblemAuthorize(ProblemAuthorize authorize)
        {
            throw new NotImplementedException();
        }

        public async Task<ProblemAuthorize> GetProblemAuthorize(long problemId)
        {
            long userId = ContextTools.GetUserIdMust();
            var authGrain = GetAuthorizeGrain;
            var authResp = await authGrain.QueryAuthorize(new QueryAuthorizeRequest()
            {
                Grantees = new List<AuthorizeGrantee>() { new() { GranteeId = userId, GranteeType = Share.Common.Models.AuthorizePo.GranteeTypeEnum.User, Role = 0, Version = 0 } },
                Resources = new List<AuthorizeResource>() { new() { ResourceId = problemId, ResourceType = Share.Common.Models.AuthorizePo.ResourceTypeEnum.Problem } }
            });
            if(authResp.Authorizes.Count!=0)
            {
                return (ProblemAuthorize)authResp.Authorizes[0];
            }
            return ProblemAuthorize.Default;
        }

        public async Task SetProblemAuthorize(long problemId, ProblemAuthorize auth, ITxHandler? handler = null, long? userId = null)
        {
            userId ??= ContextTools.GetUserIdMust();
            var authGrain = GetAuthorizeGrain;
            await authGrain.UpdateAuthorize(new List<AuthorizePoBase>() { new() { Action=auth.Action,GranteeId=(long)userId,GranteeType=AuthorizePo.GranteeTypeEnum.User,ResourceId=problemId,ResourceType=AuthorizePo.ResourceTypeEnum.Problem,Role=0} }, handler);
        }

        public async Task<QueryAuthorizeResponse> QueryAuthorize(QueryAuthorizeRequest request)
        {
            var authGrain = GetAuthorizeGrain;
            return await authGrain.QueryAuthorize(request);
        }
    }
}
