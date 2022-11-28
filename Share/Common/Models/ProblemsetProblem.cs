using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题单-题目关系表
    /// </summary>
    [GenerateSerializer]
    public partial class ProblemsetProblem
    {
        /// <summary>
        /// 题单-题目ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 题单ID
        /// </summary>
        [Id(1)]
        public long ProblemsetId { get; set; }

        /// <summary>
        /// 题目ID
        /// </summary>
        [Id(2)]
        public long ProblemId { get; set; }

        /// <summary>
        /// JSON
        /// </summary>
        [Id(3)]
        public string? Properties { get; set; }

        /// <summary>
        /// 状态枚举
        /// </summary>
        [Id(4)]
        public int Status { get; set; }
    }
}
