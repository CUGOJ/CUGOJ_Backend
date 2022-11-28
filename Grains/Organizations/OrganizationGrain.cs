using CUGOJ.Backend.Base.Organizations;
using CUGOJ.Backend.Share.Common.Models;
using CUGOJ.Backend.Share.Common.Organizations;
using CUGOJ.Backend.Share.Common.VersionContainer;
using CUGOJ.Backend.Share.DAO;
using CUGOJ.Backend.Tools;
using CUGOJ.Backend.Tools.Common;
using CUGOJ.Backend.Tools.Common.VersionContainer;
using CUGOJ.Backend.Tools.Log;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Grains.Organizations
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
            catch(Exception ex)
            {
                _logger?.Error($"初始化组织中心失败,error={CommonTools.ToJsonString(ex)}");
                throw;
            }
            await base.OnActivateAsync(cancellationToken);
        }
        #endregion
        #region Query
        public Task<IEnumerable<OrganizationBase>> GetOrganizationByOrg(long orgId, int maxDep = 0, IEnumerable<Share.Common.Models.Organization.OrganizationTypeEnum>? TypeLimit = null)
        {
            if(organizationStorage.Lock.TryEnterReadLock(Config.VersionItemMangerReaderLockTimeout))
            {
                try
                {
                    return organizationQuery.GetOrganizationByOrg(orgId, maxDep, TypeLimit);
                }
                finally
                {
                    organizationStorage.Lock.ExitReadLock();
                }
            }
            else
            {
                throw new Exception("系统繁忙,请稍后再试");
            }
        }

        public Task<IEnumerable<OrganizationBase>> GetOrganizationByUser(long userId, int maxDep = 0, IEnumerable<Share.Common.Models.Organization.OrganizationTypeEnum>? TypeLimit = null)
        {
            if (organizationStorage.Lock.TryEnterReadLock(Config.VersionItemMangerReaderLockTimeout))
            {
                try
                {
                    return organizationQuery.GetOrganizationByUser(userId, maxDep, TypeLimit);
                }
                finally
                {
                    organizationStorage.Lock.ExitReadLock();
                }
            }
            else
            {
                throw new Exception("系统繁忙,请稍后再试");
            }
        }
        #endregion
        #region Synch
        public Task<UpdateVersionItemResponse<IVersionItem>> UpdateVersionItem(long version, long managerVersion)
        {
            return versionManager.UpdateVersionItem(version, managerVersion);
        }
        #endregion
        #region Update
        private async Task updateUser(IEnumerable<long> userIdList, long organizationId, int Role, bool quit)
        {
            await organizationStorage.PushVersionItems(
                from user in userIdList.DistinctBy(a => a)
                select new UserOrganizationLinkBase
                {
                    Id = user,
                    OrganizationId = organizationId,
                    Role = Role,
                    UserOrganizationLinkBaseType = quit ?
                    UserOrganizationLinkBase.UserOrganizationLinkBaseTypeEnum.Remove :
                    UserOrganizationLinkBase.UserOrganizationLinkBaseTypeEnum.Add
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

        public async Task UpdateOrganizationInfo(OrganizationBase organizationInfo, ITxHandler? txHandler = null)
        {
            await organizationStorage.PushVersionItems(new OrganizationBase[] { organizationInfo });
        }
        #endregion
    }
}
