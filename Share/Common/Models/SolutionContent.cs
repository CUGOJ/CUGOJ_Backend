using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题解内容表
    /// </summary>
    public partial class SolutionContent
    {
        /// <summary>
        /// 题解内容ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 题解ID
        /// </summary>
        public long SolutionId { get; set; }
        /// <summary>
        /// 题解内容
        /// </summary>
        public string? Content { get; set; }
    }
}
