using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Authorize
{
    [GenerateSerializer]
    public class AuthorizeBase : IAuthorize
    {
        [Id(0)]
        public long Id { get; set; }

        [Id(1)]
        protected long auth = 0;

        public void Init(long _authorize)
        {
            auth = _authorize;
        }

        public long Action => auth;

        public long ToLong()
        {
            return auth;
        }

        public static IAuthorize ParseAuthorizeBase(AuthorizePoBase auth)
        {
            IAuthorize res = auth.ResourceType switch
            {
                AuthorizePo.ResourceTypeEnum.Problem => new ProblemAuthorize(),
                AuthorizePo.ResourceTypeEnum.Contest => new ContestAuthorize(),
                AuthorizePo.ResourceTypeEnum.Organization => new OrganizationAuthorize(),
                AuthorizePo.ResourceTypeEnum.Problemset => new ProblemsetAuthorize(),
                AuthorizePo.ResourceTypeEnum.Solution => new SolutionAuthorize(),
                _ => throw new Exception("未知的权限类型")
            };
            res.Init(auth.Action);
            return res;
        }

    }
}
