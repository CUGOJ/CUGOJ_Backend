using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题目内容
    /// </summary>
    public partial class ProblemContent
    {
        /// <summary>
        /// 题目内容ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 题目ID
        /// </summary>
        public long ProblemId { get; set; }
        /// <summary>
        /// 题目具体内容
        /// </summary>
        public string? Content { get; set; }
    }
}
