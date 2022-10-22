using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题单-题目关系表
    /// </summary>
    public partial class ProblemsetProblem
    {
        /// <summary>
        /// 题单-题目ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 题单ID
        /// </summary>
        public long ProblemsetId { get; set; }
        /// <summary>
        /// 题目ID
        /// </summary>
        public long ProblemId { get; set; }
        /// <summary>
        /// JSON
        /// </summary>
        public string? Properties { get; set; }
        /// <summary>
        /// 状态枚举
        /// </summary>
        public int Status { get; set; }
    }
}
