using CUGOJ.Share.Common.Models;
using Orleans.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Problem
{
    public interface IProblemServiceBase
    {
        public Task<long> CreateProblem(ProblemBasePo problemBase, ProblemContentPo problemContent);
    }
}
