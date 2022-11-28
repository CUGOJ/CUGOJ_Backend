using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题解内容表
    /// </summary>
    [GenerateSerializer]
    public partial class SolutionContent
    {
        /// <summary>
        /// 题解内容ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 题解ID
        /// </summary>
        [Id(1)]
        public long SolutionId { get; set; }

        /// <summary>
        /// 题解内容
        /// </summary>
        [Id(2)]
        public string? Content { get; set; }
    }
}
