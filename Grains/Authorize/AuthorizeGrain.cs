using CUGOJ.Base.DAO.Context;
using CUGOJ.Base.Organizations;
using CUGOJ.Share.Common.Organizations;
using CUGOJ.Tools.Common;
using CUGOJ.Base.Authorize;
using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.VersionContainer;
using CUGOJ.Share.DAO;
using CUGOJ.Tools.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.Authorize
{
    public class AuthorizeGrain : Grain, IAuthorizeGrain
    {
        public interface IAuthorizeUpdateTx : ITxHandler
        {
            public IEnumerable<AuthorizePoBase> UpdateAuthorize { get; }
        }

        #region Init
        readonly IVersionItemManager<AuthorizePoBase> versionManager;
        readonly IAuthorizeQuery authorizeQuery;
        readonly IVersionItemStorage<AuthorizePoBase> authorizeStorage;
        readonly Logger? _logger;
        public AuthorizeGrain(Logger? logger)
        {
            _logger = logger;
            AuthorizeStorage storage = new();
            authorizeStorage = storage;
            authorizeQuery = storage;
            versionManager = new VersionItemManager<AuthorizePoBase>(storage, _logger);
        }

        public override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            try
            {
                await authorizeStorage.Init();
            }
            catch (Exception ex)
            {
                _logger?.Error($"初始化组织中心失败,error={CommonTools.ToJsonString(ex)}");
                throw;
            }
            await base.OnActivateAsync(cancellationToken);
        }

        #endregion

        #region Query
        public Task<QueryAuthorizeResponse> QueryAuthorize(QueryAuthorizeRequest request)
        {
            return authorizeQuery.QueryAuthorize(request);
        }
        #endregion
        #region Synch
        public Task<UpdateVersionItemResponse<AuthorizePoBase>> SynchVersionItem(long version, long managerVersion)
        {
            return versionManager.SynchVersionItem(version, managerVersion);
        }
        #endregion

        #region Update

        public Task UpdateAuthorize(IEnumerable<AuthorizePoBase> UpdateAuthorizes, ITxHandler? txHandler = null)
        {
            using var context = new CUGOJContext();
            if (txHandler != null)
                txHandler.Handle(context);
            foreach (var auth in UpdateAuthorizes)
            {
                var exist = (from a in context.Authorizes
                             where a.GranteeType == auth.GranteeType
                             && a.GranteeId == auth.GranteeId
                             && a.Role == auth.Role
                             && a.ResourceType == auth.ResourceType
                             && a.ResourceId == auth.ResourceId
                             select a).FirstOrDefault();
                if (exist != null)
                {
                    exist.Action = auth.Action;
                }
                else
                {
                    context.Authorizes.Add(new AuthorizePo()
                    {
                        GranteeType = auth.GranteeType,
                        GranteeId = auth.GranteeId,
                        ResourceType = auth.ResourceType,
                        ResourceId = auth.ResourceId,
                        Action = auth.Action,
                        Role = auth.Role,
                    });
                }
            }
            context.SaveChanges();
            versionManager.PushVersionItems(UpdateAuthorizes);

            return Task.CompletedTask;
        }
        #endregion
    }
}
