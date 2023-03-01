using CUGOJ.Base.DAO.Context;
using CUGOJ.Grains.Authorize;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.Problem;
using CUGOJ.Tools.Common;
using CUGOJ.Tools.Context;
using CUGOJ.Tools.Exceptions;
using CUGOJ.Tools.Log;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.Common.Problem
{
    public class ProblemServiceBase :IProblemServiceBase
    {
        private readonly Logger logger;
        private readonly AuthorizeService authorizeService;

        public ProblemServiceBase(Logger logger, AuthorizeService authorizeService)
        {
            this.logger = logger;
            this.authorizeService = authorizeService;
        }

        public async Task<long> CreateProblem(ProblemBasePo problemBase, ProblemContentPo problemContent)
        {
            var user = ContextTools.GetUserIdMust();
            problemBase.Id = 0;
            problemBase.WriterId = user;
            problemBase.SubmissionCount = 0;
            problemBase.AcceptedCount = 0;
            problemBase.Version = CommonTools.GetUTCUnixMilli();

            using var context = new CUGOJContext();
            using var tx = await context.Database.BeginTransactionAsync();

            try
            {
                problemBase = (await context.ProblemBases.AddAsync(problemBase)).Entity;
                await context.SaveChangesAsync();
                problemContent.Id = 0;
                problemContent.ProblemId = problemBase.Id;


                await context.ProblemContents.AddAsync(problemContent);
                await context.SaveChangesAsync();
                await tx.CommitAsync();
                return problemBase.Id;
            }
            catch (Exception e)
            {
                try
                {
                    await tx.RollbackAsync();
                }
                catch (Exception rollbackEx)
                {
                    logger.Exception(rollbackEx);
                }
                logger.Exception(e);
                throw Exceptions.Todo("插入数据失败,请稍后再试", ExceptionTypeEnum.SystemError);
            }
        }
    }
}
