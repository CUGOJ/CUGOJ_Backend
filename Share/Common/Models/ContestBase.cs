using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 比赛列表
    /// </summary>
    public partial class ContestBase
    {
        /// <summary>
        /// 比赛ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 承办组织
        /// </summary>
        public long OrganizationId { get; set; }
        /// <summary>
        /// 所有者
        /// </summary>
        public long OwnerId { get; set; }
        /// <summary>
        /// 赛事类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 出题人
        /// </summary>
        public string? Writers { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 比赛名称
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// 赛事的简单描述
        /// </summary>
        public string? Profile { get; set; }
        /// <summary>
        /// 比赛状态枚举
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
