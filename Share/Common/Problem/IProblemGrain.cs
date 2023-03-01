using CUGOJ.Share.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Problem
{
    public interface IProblemGrain : IGrainWithIntegerKey
    {
        public Task<(ProblemBasePo?, ProblemContentPo?)> GetProblem(bool withContent = false);
        public Task UpdateProblemContent(ProblemContentPo problemContent);
        public Task UpdateProblemBase(ProblemBasePo problemBase);
    }
}
