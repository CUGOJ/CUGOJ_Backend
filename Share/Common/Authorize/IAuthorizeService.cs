using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Authorize
{
    public interface IAuthorizeService:IAuthorizeQuery
    {
        Task<ProblemAuthorize> GetProblemAuthorize(long problemId);
        Task<bool> CheckProblemAuthorize(ProblemAuthorize authorize);
    }
}
