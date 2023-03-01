using CUGOJ.Tools;
using CUGOJ.Tools.Common;
using CUGOJ.Base.Organizations;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.Organizations;
using CUGOJ.Share.Common.VersionContainer;
using CUGOJ.Share.DAO;
using CUGOJ.Tools.Log;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.Organizations
{
    public class OrganizationGrain : Grain, IOrganizationGrain
    {
        #region Init
        readonly IVersionItemManager<IVersionItem> versionManager;
        readonly IOrganizationQuery organizationQuery;
        readonly IVersionItemStorage<IVersionItem> organizationStorage;
        readonly Logger? _logger;
        public OrganizationGrain(Logger? logger)
        {
            _logger = logger;
            OrganizationStorage storage = new();
            organizationStorage = storage;
            organizationQuery = storage;
            versionManager = new VersionItemManager<IVersionItem>(storage, _logger);
        }
        public override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            try
            {
                await organizationStorage.Init();
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
        public Task<IEnumerable<OrganizationPoBase>> GetOrganizationByOrg(long orgId, int maxDep = 0, IEnumerable<OrganizationPo.OrganizationTypeEnum>? TypeLimit = null)
        {
            return organizationQuery.GetOrganizationByOrg(orgId, maxDep, TypeLimit);
        }

        public Task<IEnumerable<OrganizationPoBase>> GetOrganizationByUser(long userId, int maxDep = 0, IEnumerable<OrganizationPo.OrganizationTypeEnum>? TypeLimit = null)
        {
            return organizationQuery.GetOrganizationByUser(userId, maxDep, TypeLimit);
        }
        #endregion
        #region Synch
        public Task<UpdateVersionItemResponse<IVersionItem>> SynchVersionItem(long version, long managerVersion)
        {
            return versionManager.SynchVersionItem(version, managerVersion);
        }
        #endregion
        #region Update
        private async Task updateUser(IEnumerable<long> userIdList, long organizationId, int Role, bool quit)
        {
            await versionManager.PushVersionItems(
                from user in userIdList.DistinctBy(a => a)
                select new UserOrganizationLinkPoBase
                {
                    Id = user,
                    OrganizationId = organizationId,
                    Role = Role,
                    UserOrganizationLinkBaseType = quit ?
                    UserOrganizationLinkPoBase.UserOrganizationLinkBaseTypeEnum.Remove :
                    UserOrganizationLinkPoBase.UserOrganizationLinkBaseTypeEnum.Add
                });
        }
        public async Task JoinOrganization(IEnumerable<long> userIdList, long organizationId, int Role, ITxHandler? txHandler = null)
        {
            await updateUser(userIdList, organizationId, Role, false);
        }

        public async Task QuitOrganization(IEnumerable<long> userIdList, long organizationId, ITxHandler? txHandler = null)
        {
            await updateUser(userIdList, organizationId, 0, true);
        }

        public async Task UpdateOrganizationInfo(OrganizationPoBase organizationInfo, ITxHandler? txHandler = null)
        {
            if ((from o in await GetOrganizationByOrg(organizationInfo.Parent) where o.Id == organizationInfo.Id select o).Any())
                throw new Exception("当前组织的下级组织不能成为新的父组织");
            await versionManager.PushVersionItems(new OrganizationPoBase[] { organizationInfo });
        }
        #endregion
    }
}
