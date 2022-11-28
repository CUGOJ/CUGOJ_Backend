using CUGOJ.Backend.Share.Common.VersionContainer;
using CUGOJ.Backend.Share.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.Common.Organizations
{
    public interface IOrganizationGrain : IGrainWithIntegerKey, IOrganizationQuery
    {
        /// <summary>
        /// 用于同步组织架构,Iversion必然是OrganizationBase 或者 UserOrganizationLinkBase
        /// </summary>
        /// <param name="version">本地存储的最新快照版本,本地未存储则传0</param>
        /// <param name="managerVersion">本地存储的最新管理器版本,本地未存储则传0</param>
        /// <returns></returns>
        public Task<UpdateVersionItemResponse<IVersionItem>> UpdateVersionItem(long version, long managerVersion);
        /// <summary>
        /// 将一批用户注册以给定身份注册进组织
        /// </summary>
        /// <param name="userIdList">要注册的用户群Id</param>
        /// <param name="organizationId">要注册进的组织Id</param>
        /// <param name="Role">注册进组织的身份</param>
        /// <param name="txHandler">在更新结构前要执行的DB操作,会在同一事务内执行</param>
        public Task JoinOrganization(IEnumerable<long> userIdList, long organizationId, int Role,ITxHandler? txHandler = null);
        /// <summary>
        /// 将一批用户从给定组织退出
        /// </summary>
        /// <param name="userIdList">要退出的用户群Id</param>
        /// <param name="organizationId">要退出的组织Id</param>
        /// <param name="txHandler">在更新结构前要执行的DB操作,会在同一事务内执行</param>
        public Task QuitOrganization(IEnumerable<long> userIdList,long organizationId, ITxHandler? txHandler = null);
        /// <summary>
        /// 更新一个组织的架构
        /// </summary>
        /// <param name="organizationInfo">要更新的组织结构</param>
        /// <param name="txHandler">在更新结构前要执行的DB操作,会在同一事务内执行</param>
        /// <returns></returns>
        public Task UpdateOrganizationInfo(OrganizationBase organizationInfo, ITxHandler? txHandler = null);


    }

    public class UpdateOrganizationResponse
    {
        public enum UpdateOrganizationResponseTypeEnum
        {
            Increment,
            Rebase,
            Error,
        }
        public UpdateOrganizationResponseTypeEnum Type { get; set; }

    }

}
