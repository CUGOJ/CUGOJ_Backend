using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题目来源表
    /// </summary>
    public partial class ProblemSource
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 题目来源名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 题目源主页
        /// </summary>
        public string Url { get; set; } = null!;
        /// <summary>
        /// 题目show_id组合源链接策略
        /// </summary>
        public string? Properties { get; set; }
    }
}
