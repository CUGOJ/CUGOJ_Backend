using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 得分表
    /// </summary>
    public partial class Score
    {
        /// <summary>
        /// ScoreID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Score名称
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 目标主体类型
        /// </summary>
        public int TargetType { get; set; }
        /// <summary>
        /// 目标主体ID
        /// </summary>
        public long TargetId { get; set; }
        /// <summary>
        /// 聚合基准
        /// </summary>
        public long AggId { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public long Value { get; set; }
        /// <summary>
        /// 状态枚举
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
    }
}
