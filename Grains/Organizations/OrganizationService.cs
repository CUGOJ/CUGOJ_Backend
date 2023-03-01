using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.Organization
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IClusterClient client;

        private IOrganizationGrain GetOrganizationGrain => client.GetGrain<IOrganizationGrain>(1);
        public OrganizationService(IClusterClient client)
        {
            this.client = client;
        }
        public async Task<IEnumerable<OrganizationPoBase>> GetOrganizationByOrg(long orgId, int maxDep = 0, IEnumerable<OrganizationPo.OrganizationTypeEnum>? TypeLimit = null)
        {
            return await GetOrganizationGrain.GetOrganizationByOrg(orgId, maxDep, TypeLimit);
        }

        public async Task<IEnumerable<OrganizationPoBase>> GetOrganizationByUser(long userId, int maxDep = 0, IEnumerable<OrganizationPo.OrganizationTypeEnum>? TypeLimit = null)
        {
            return await GetOrganizationGrain.GetOrganizationByUser(userId, maxDep, TypeLimit);
        }
    }
}
