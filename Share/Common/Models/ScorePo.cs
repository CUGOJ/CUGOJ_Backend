using System;
using System.Collections.Generic;

namespace CUGOJ.Share.Common.Models
{
    /// <summary>
    /// 得分表
    /// </summary>
    [GenerateSerializer]
    public partial class ScorePo
    {
        /// <summary>
        /// ScoreID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// Score名称
        /// </summary>
        [Id(1)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 目标主体类型
        /// </summary>
        [Id(2)]
        public int TargetType { get; set; }

        /// <summary>
        /// 目标主体ID
        /// </summary>
        [Id(3)]
        public long TargetId { get; set; }

        /// <summary>
        /// 聚合基准
        /// </summary>
        [Id(4)]
        public long AggId { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        [Id(5)]
        public long Value { get; set; }

        /// <summary>
        /// 状态枚举
        /// </summary>
        [Id(6)]
        public int Status { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Id(7)]
        public int Type { get; set; }
    }
}
