using CUGOJ.Base.DAO.Context;
using CUGOJ.Tools.Common;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.Problem;
using CUGOJ.Tools.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.Common.Problem
{
    public class ProblemGrainBase : Grain, IProblemGrain
    {
        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }
        protected readonly Logger? _logger;
        public long ProblemId { get => this.GetPrimaryKeyLong(); }
        public ProblemBasePo? problemBase { get; private set; }
        public ProblemContentPo? problemContent { get; private set; }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        protected virtual async Task SaveProblemBase()
        {
            using var context = new CUGOJContext();
            if (problemBase != null)
            {
                context.ProblemBases.Update(problemBase);
            }
            await context.SaveChangesAsync();
        }

        protected virtual async Task SaveProblemContent()
        {
            using var context = new CUGOJContext();
            if (problemContent != null)
            {
                context.ProblemContents.Update(problemContent);
            }
            await context.SaveChangesAsync();
        }

        [TimerLock(10000)]
        protected virtual async Task LoadProblemBase()
        {
            using var context = new CUGOJContext();
            problemBase = await context.ProblemBases.FirstOrDefaultAsync(x => x.Id == ProblemId);
        }

        [TimerLock(10000)]
        protected virtual async Task LoadProblemContent()
        {
            using var context = new CUGOJContext();
            problemContent = await context.ProblemContents.FirstOrDefaultAsync(x => x.ProblemId == ProblemId);
        }

        public virtual async Task<ProblemBasePo?> GetProblemBase()
        {
            if (problemBase == null)
                await LoadProblemBase();
            return problemBase;
        }

        public virtual async Task<ProblemContentPo?> GetProblemContent()
        {
            if (problemContent == null)
                await LoadProblemContent();
            return problemContent;
        }

        public virtual async Task<(ProblemBasePo?, ProblemContentPo?)> GetProblem(bool withContent = false)
        {
            (ProblemBasePo?, ProblemContentPo?) res = (null, null);
            res.Item1 = await GetProblemBase();
            if (withContent)
                res.Item2 = await GetProblemContent();
            return res;
        }

        public virtual async Task UpdateProblemContent(ProblemContentPo problemContent)
        {
            problemContent.Id = ProblemId;
            await SaveProblemContent();
            this.problemContent = problemContent;
        }

        public async Task UpdateProblemBase(ProblemBasePo problemBase)
        {
            problemBase.Id = ProblemId;
            await SaveProblemBase();
            this.problemBase = problemBase;
        }

    }
}
