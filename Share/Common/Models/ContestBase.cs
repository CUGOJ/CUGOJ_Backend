using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 比赛列表
    /// </summary>
    [GenerateSerializer]
    public partial class ContestBase
    {
        /// <summary>
        /// 比赛ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 承办组织
        /// </summary>
        [Id(1)]
        public long OrganizationId { get; set; }

        /// <summary>
        /// 所有者
        /// </summary>
        [Id(2)]
        public long OwnerId { get; set; }

        /// <summary>
        /// 赛事类型
        /// </summary>
        [Id(3)]
        public int Type { get; set; }

        /// <summary>
        /// 出题人
        /// </summary>
        [Id(4)]
        public string? Writers { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Id(5)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Id(6)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 比赛名称
        /// </summary>
        [Id(7)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// 赛事的简单描述
        /// </summary>
        [Id(8)]
        public string? Profile { get; set; }

        /// <summary>
        /// 比赛状态枚举
        /// </summary>
        [Id(9)]
        public int Status { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Id(10)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Id(11)]
        public DateTime CreateTime { get; set; }
    }
}
