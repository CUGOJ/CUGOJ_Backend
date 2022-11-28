using CUGOJ.Backend.Share.Common.VersionContainer;
using Microsoft.EntityFrameworkCore;
using Orleans.CodeGeneration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 用户组织关联表
    /// </summary>
    [Index(nameof(UserId))]
    [Index(nameof(OrganizationId), nameof(Role))]
    [Table("user_organization_link")]
    [GenerateSerializer]
    public class UserOrganizationLink
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Comment("主键")]
        [Column("id")]
        [Id(0)]
        public long Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        [Comment("用户Id")]
        [Id(1)]
        public long UserId { get; set; }
        /// <summary>
        /// 组织Id
        /// </summary>
        [Column("organization_id")]
        [Comment("组织Id")]
        [Id(2)]
        public long OrganizationId { get; set; }
        /// <summary>
        /// 用户在组织内的角色
        /// </summary>
        [Column("role")]
        [Comment("用户在组织内的角色")]
        [Id(3)]
        public int Role { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [Comment("创建时间")]
        [Id(4)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Column("update_time")]
        [Comment("最后更新时间")]
        [Id(5)]
        public DateTime UpdateTime { get; set; }

    }

    [GenerateSerializer]
    public class UserOrganizationLinkBase:IVersionItem
    {
        public enum UserOrganizationLinkBaseTypeEnum
        {
            Add = 1,
            Remove = 2,
        }
        [Id(1)]
        public long Id { get; set; }
        [Id(2)]
        public long OrganizationId { get; set; }
        [Id(3)]
        public int Role { get; set; }
        [Id(4)]
        public UserOrganizationLinkBaseTypeEnum UserOrganizationLinkBaseType { get; set; }
        [Id(5)]
        public long Version { get; set; }
    }
}
