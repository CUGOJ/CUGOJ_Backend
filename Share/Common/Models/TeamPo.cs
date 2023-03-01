using System;
using System.Collections.Generic;

namespace CUGOJ.Share.Common.Models
{
    /// <summary>
    /// 队伍信息表
    /// </summary>
    public partial class TeamPo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 队伍名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 个性签名
        /// </summary>
        public string? Signature { get; set; }
        /// <summary>
        /// 队伍介绍
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 队长
        /// </summary>
        public long Leader { get; set; }
        /// <summary>
        /// 所属组织
        /// </summary>
        public long OrganizationId { get; set; }
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
}
