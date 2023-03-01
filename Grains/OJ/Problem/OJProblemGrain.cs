using CUGOJ.Base.DAO.Context;
using CUGOJ.Grains.Common.Problem;
using CUGOJ.Share.OJ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.OJ.Problem
{
    public partial class OJProblemGrain : ProblemGrainBase, IOJProblemGrain
    {
        protected override async Task LoadProblemBase()
        {
            await base.LoadProblemBase();
            using var context = new CUGOJContext();
            if (problemBase != null)
            {
                var submissions = await (from s in context.SubmissionBases where s.ProblemId == ProblemId select s.Status).ToListAsync();
                problemBase.SubmissionCount = submissions.Count;
                problemBase.AcceptedCount = (from s in submissions where s == (int)OJSubmissionStatus.Accepted select s).Count();
            }
        }
    }
}
