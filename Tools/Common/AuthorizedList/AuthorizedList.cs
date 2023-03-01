using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.Organizations;
using CUGOJ.Share.Common.VersionContainer;
using CUGOJ.Tools.Context;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Common.AuthorizedList
{
    public class AuthorizedList<T> where T : class, IAuthorize
    {
        private readonly IAuthorizeService authorizeService;
        private readonly IOrganizationSet organizationSet;
        private readonly Dictionary<long, long> organizationVersoin = new();
        public AuthorizedList(IAuthorizeService authorizeService, IOrganizationSet organizationSet)
        {
            this.authorizeService = authorizeService;
            this.organizationSet = organizationSet;
        }

        public AuthorizePo.ResourceTypeEnum ResourceType { get; init; }
        private long GetVersion(long orgId)
        {
            if (organizationVersoin.TryGetValue(orgId, out var version))
                return version;
            return 0;
        }

        private void SetVersion(long orgId, long version)
        {
            if (GetVersion(orgId) < version)
                organizationVersoin[orgId] = version;
        }

        private List<IAuthorize> items = new();

        public IComparer<IAuthorize>? Comparer { get; set; }

        [ReaderWriterLock(ReaderWriterLockAttribute.LockTypeEnum.Write, WriterLockTimeOut = 10000)]
        private async Task Update()
        {
            var grantees = await organizationSet.GetAuthorizeGrantees();
            foreach (var grantee in grantees)
            {
                grantee.Version = GetVersion(grantee.GranteeId);
            }
            var res = await authorizeService.QueryAuthorize(new QueryAuthorizeRequest
            {
                Grantees = grantees,
                ResourceType = ResourceType
            });
            if (Comparer != null)
                res.Authorizes.Sort(Comparer);
            items = res.Authorizes;
            foreach (var changedOrg in res.ChangedGrantees)
            {
                SetVersion(changedOrg.GranteeId, changedOrg.Version);
            }
        }

        public async Task<IEnumerable<T>> GetItems(long offset, long limit, Func<T,bool>? filter)
        {
            await Update();
            var res = new List<T>();
            List<IAuthorize> itemsBack = items;
            if (offset > itemsBack.Count) return res;
            foreach (var item in itemsBack)
            {
                if (limit == 0) break;
                if (filter != null)
                {
                    if (!filter((T)item))
                        continue;
                }
                if (offset==0)
                {
                    res.Add((T)item);
                    limit--;
                }
                else
                {
                    offset--;
                }
            }
            return res;
        }
    }
}
