using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题目内容
    /// </summary>
    [GenerateSerializer]
    public partial class ProblemContent
    {
        /// <summary>
        /// 题目内容ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 题目ID
        /// </summary>
        [Id(1)]
        public long ProblemId { get; set; }

        /// <summary>
        /// 题目具体内容
        /// </summary>
        [Id(2)]
        public string? Content { get; set; }
    }
}
