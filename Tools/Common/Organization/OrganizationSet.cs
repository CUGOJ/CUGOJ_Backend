using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.Organizations;
using CUGOJ.Tools.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Common.Organizations
{
    public class OrganizationSet:IOrganizationSet
    {
        private readonly IOrganizationService organizationService;

        public OrganizationSet(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        private IEnumerable<OrganizationPoBase> Organizations { get; set; } = new List<OrganizationPoBase>();

        [TimerLock(3000)]
        private async Task Update()
        {
            var userId = ContextTools.GetUserId();
            if (userId==null)
            {
                Organizations = new List<OrganizationPoBase>();
                return;
            }
            Organizations = await organizationService.GetOrganizationByUser((long)userId);
        }

        public async Task<IEnumerable<OrganizationPoBase>> GetBelongsOrganizations()
        {
            await Update();
            return Organizations;
        }

        public async Task<IEnumerable<AuthorizeGrantee>> GetAuthorizeGrantees()
        {
            var userId = ContextTools.GetUserId();
            List<AuthorizeGrantee> res = new();
            res.Add(new AuthorizeGrantee { GranteeId = userId ?? 0, GranteeType = AuthorizePo.GranteeTypeEnum.User });
            var orgs = await GetBelongsOrganizations();
            res.AddRange(orgs.Select(o => new AuthorizeGrantee { GranteeId = o.Id, GranteeType = AuthorizePo.GranteeTypeEnum.Organization, Role = o.Role, Version = o.Version }));
            return res;
        }
    }
}
