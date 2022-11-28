using CUGOJ.Backend.Share.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.Common.Authorize
{
    public interface IAuthorizeQuery
    {
        Task<IAuthorize> GetAuthorizeByUser(QueryAuthorizeGrantee grantee, Models.Authorize.ResourceTypeEnum resourceType, long resourceId);
        Task<IEnumerable<IAuthorize>> MulGetAuthorizeByResourceType(QueryAuthorizeGrantee grantee, Models.Authorize.ResourceTypeEnum resourceType);
        Task<IAuthorize> GetAuthorizeByUser(IEnumerable<QueryAuthorizeGrantee> grantees, Models.Authorize.ResourceTypeEnum resourceType, long resourceId);
        Task<IEnumerable<IAuthorize>> MulGetAuthorizeByResourceType(IEnumerable<QueryAuthorizeGrantee> grantees, Models.Authorize.ResourceTypeEnum resourceType);
    }

    [GenerateSerializer]
    public class QueryAuthorizeGrantee
    {
        [Id(0)]
        public Models.Authorize.GranteeTypeEnum GranteeType { get; set; }
        [Id(1)]
        public long GranteeId { get; set; }
        [Id(2)]
        public int Role { get; set; }
    }

}
