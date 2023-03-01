using CUGOJ.Share.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Authorize
{
    public interface IAuthorizeQuery
    {
        public Task<QueryAuthorizeResponse> QueryAuthorize(QueryAuthorizeRequest request);
    }
    [GenerateSerializer]
    public class QueryAuthorizeRequest
    {
        [Id(0)]
        public IEnumerable<AuthorizeGrantee> Grantees { get; set; } = new List<AuthorizeGrantee>();
        [Id(1)]
        public IEnumerable<AuthorizeResource> Resources { get; set; } = new List<AuthorizeResource>();
        [Id(2)]
        public AuthorizePo.ResourceTypeEnum? ResourceType { get; set; } = null;
    }
    [GenerateSerializer]
    public class QueryAuthorizeResponse
    {
        [Id(0)]
        public List<IAuthorize> Authorizes { get; set; } = new();
        [Id(1)]
        public List<AuthorizeGrantee> ChangedGrantees { get; set; } = new();
    }

    [GenerateSerializer]
    public class AuthorizeGrantee
    {
        [Id(0)]
        public AuthorizePo.GranteeTypeEnum GranteeType { get; set; }
        [Id(1)]
        public long GranteeId { get; set; }
        [Id(2)]
        public int Role { get; set; }
        [Id(3)]
        public long Version { get; set; }
    }
    [GenerateSerializer]
    public class AuthorizeResource
    {
        [Id(0)]
        public AuthorizePo.ResourceTypeEnum ResourceType { get; set; }
        [Id(1)]
        public long ResourceId { get; set; }
    }

}
