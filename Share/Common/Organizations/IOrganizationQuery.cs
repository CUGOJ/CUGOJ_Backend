using CUGOJ.Share.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Organizations
{
    public interface IOrganizationQuery
    {
        /// <summary>
        /// 获取用户所属的组织结构列表
        /// </summary>
        /// <param name="userId">要查询的用户id</param>
        /// <param name="maxDep">限定查询的最大深度,0代表不限制</param>
        /// <param name="TypeLimit">限定查询的组织类型,null代表不限制</param>
        /// <returns>返回符合条件的所有组织的基本信息,特别的,OrganizationPoBase中的Role表示该用户在该组织中的角色而非该组织在父组织中的角色</returns>
        public Task<IEnumerable<OrganizationPoBase>> GetOrganizationByUser(long userId, int maxDep = 0, IEnumerable<OrganizationPo.OrganizationTypeEnum>? TypeLimit = null);

        /// <summary>
        /// 获取组织所属的组织结构列表
        /// </summary>
        /// <param name="orgId">要查询的组织id</param>
        /// <param name="maxDep">限定查询的最大深度,0代表不限制</param>
        /// <param name="TypeLimit">限定查询的组织类型,null代表不限制</param>
        /// <returns>返回符合条件的所有组织的基本信息,特别的,OrganizationPoBase中的Role表示查询组织在该组织中的角色而非该组织在父组织中的角色</returns>
        public Task<IEnumerable<OrganizationPoBase>> GetOrganizationByOrg(long orgId, int maxDep = 0, IEnumerable<OrganizationPo.OrganizationTypeEnum>? TypeLimit = null);
    }
}
