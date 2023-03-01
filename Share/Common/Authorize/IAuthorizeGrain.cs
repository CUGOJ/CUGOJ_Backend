using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.VersionContainer;
using CUGOJ.Share.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Authorize
{
    public interface IAuthorizeGrain : IGrainWithIntegerKey, IAuthorizeQuery
    {
        public Task<UpdateVersionItemResponse<AuthorizePoBase>> SynchVersionItem(long version, long managerVersion);
        public Task UpdateAuthorize(IEnumerable<AuthorizePoBase> UpdateAuthorizes, ITxHandler? txHandler = null);
    }

    public class QueryProblemAuthorizeResponse
    {
        public IEnumerable<ProblemAuthorize> Authorizes { get; set; } = null!;
        public IEnumerable<AuthorizeGrantee> ChangeGrantees { get; set; } = null!;
    }

    public class QueryContestAuthorizeResponse
    {
        public IEnumerable<ContestAuthorize> Authorizes { get; set; } = null!;
        public IEnumerable<AuthorizeGrantee> ChangeGrantees { get; set; } = null!;
    }

    public class QueryOrganizationAuthorizeResponse
    {
        public IEnumerable<OrganizationAuthorize> Authorizes { get; set; } = null!;
        public IEnumerable<AuthorizeGrantee> ChangeGrantees { get; set; } = null!;
    }

    public class QueryProblemsetAuthorizeResponse
    {
        public IEnumerable<ProblemsetAuthorize> Authorizes { get; set; } = null!;
        public IEnumerable<AuthorizeGrantee> ChangeGrantees { get; set; } = null!;
    }

    public class QuerySolutionAuthorizeResponse
    {
        public IEnumerable<SolutionAuthorize> Authorizes { get; set; } = null!;
        public IEnumerable<AuthorizeGrantee> ChangeGrantees { get; set; } = null!;
    }

}
