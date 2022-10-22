using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 提交内容表
    /// </summary>
    public partial class SubmissionContent
    {
        /// <summary>
        /// 提交内容ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 提交ID
        /// </summary>
        public long SubmissionId { get; set; }
        /// <summary>
        /// 提交内容
        /// </summary>
        public string? Content { get; set; }
    }
}
