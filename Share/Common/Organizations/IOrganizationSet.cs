using CUGOJ.Share.Common.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Organizations
{
    public interface IOrganizationSet
    {
        public Task<IEnumerable<OrganizationPoBase>> GetBelongsOrganizations();
        public Task<IEnumerable<AuthorizeGrantee>> GetAuthorizeGrantees();
    }
}
