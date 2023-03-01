using CUGOJ.Grains.OJ.Problem;
using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.Problem;
using CUGOJ.Share.Common.User;
using CUGOJ.Share.Defines;
using CUGOJ.Share.OJ;
using CUGOJ.Tools.Context;
using CUGOJ.Tools.Log;
using Microsoft.AspNetCore.Mvc;
using Orleans.Serialization.Invocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Frontend.Controllers.ProblemController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OJProblemController : ControllerBase
    {
        private readonly IClusterClient client;
        private readonly Logger logger;
        private readonly IAuthorizeService authorize;
        private readonly IProblemServiceBase problemService;

        public OJProblemController(IClusterClient client, Logger logger, IAuthorizeService authorize, IProblemServiceBase problemService)
        {
            this.client = client;
            this.logger = logger;
            this.authorize = authorize;
            this.problemService = problemService;
        }

        private readonly ProblemAuthorize getProblemAuth = new()
        {
            Visitable = true
        };

        [HttpGet]
        public async Resp GetProblem([FromQuery] long id)
        {
            try
            {
                if (!await authorize.CheckProblemAuthorize(getProblemAuth))
                    return Code(Share.Defines.ErrorCodes.PermissionDenied);
                var problemGrain = client.GetGrain<IOJProblemGrain>(id);
                return Content(await problemGrain.GetProblem(true));
            }
            catch (Exception e)
            {
                logger.Exception(e);
                return Exception(e);
            }
        }

        [HttpGet]
        public async Resp GetProblemList([FromQuery]int offset, [FromQuery] int limit)
        {
            try
            {
                var userId = ContextTools.GetUserId();
                var userGrain = client.GetGrain<IUserGrain>(userId??0);
                return Content(await userGrain.GetProblemIdList(offset, limit));
            }
            catch(Exception e)
            {
                logger.Exception(e);
                return Exception(e);
            }
        }

        public record ProblemDto
        {
            public ProblemBasePo? ProblemBase { get; set; }
            public ProblemContentPo? ProblemContent { get; set; }
        }

        [HttpPost]
        public async Resp CreateProblem([FromBody] ProblemDto problem)
        {
            try
            {
                if (problem.ProblemBase == null)
                    return Code(ErrorCodes.CreateProblemBaseNull);
                if (problem.ProblemContent == null)
                    return Code(ErrorCodes.CreateProblemContentNull);
                return Content(await problemService.CreateProblem(problem.ProblemBase, problem.ProblemContent));   
            }
            catch(Exception e)
            {
                logger.Exception(e);
                return Exception(e);
            }
        }
    }
}
