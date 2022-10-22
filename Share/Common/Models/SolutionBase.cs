using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题解基本信息表
    /// </summary>
    public partial class SolutionBase
    {
        /// <summary>
        /// 题解ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 作者ID
        /// </summary>
        public long WriterId { get; set; }
        /// <summary>
        /// 关联的比赛
        /// </summary>
        public long? ContestId { get; set; }
        /// <summary>
        /// 关联的题目
        /// </summary>
        public long? ProblemId { get; set; }
        /// <summary>
        /// 状态枚举
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
