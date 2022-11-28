using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题解基本信息表
    /// </summary>
    [GenerateSerializer]
    public partial class SolutionBase
    {
        /// <summary>
        /// 题解ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 作者ID
        /// </summary>
        [Id(1)]
        public long WriterId { get; set; }

        /// <summary>
        /// 关联的比赛
        /// </summary>
        [Id(2)]
        public long? ContestId { get; set; }

        /// <summary>
        /// 关联的题目
        /// </summary>
        [Id(3)]
        public long? ProblemId { get; set; }

        /// <summary>
        /// 状态枚举
        /// </summary>
        [Id(4)]
        public int Status { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Id(5)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Id(6)]
        public DateTime CreateTime { get; set; }
    }
}
