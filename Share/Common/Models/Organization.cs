using CUGOJ.Backend.Share.Common.VersionContainer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 组织信息表
    /// </summary>
    public partial class Organization
    {
        public enum OrganizationTypeEnum
        {
            Normal = 1,
            Team = 2,
            TrainingTeam = 3,
            Out = 4,
        }
        /// <summary>
        /// 自增ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 组织名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 个性签名
        /// </summary>
        [Comment("个性签名")]
        [Column("signature")]
        public string? Signature { get; set; }
        /// <summary>
        /// 组织类型
        /// </summary>
        [Comment("组织类型")]
        [Column("organization_type")]
        public OrganizationTypeEnum OrganizationType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 组织所有人
        /// </summary>
        public long Owner { get; set; }
        /// <summary>
        /// 父组织
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 在父组织中的身份
        /// </summary>
        [Comment("父组织中的身份")]
        [Column("role")]
        public int Role { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    [GenerateSerializer]
    public class OrganizationBase:IVersionItem
    {
        [Id(0)]
        public long Id { get; set; }
        [Id(1)]
        public long Parent { get; set; }
        [Id(2)]
        public int Role { get; set; }
        [Id(3)]
        public Organization.OrganizationTypeEnum OrganizationType { get; set; }
        [Id(4)]
        public long Version { get; set; }
    }

}
