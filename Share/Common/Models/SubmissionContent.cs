using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 提交内容表
    /// </summary>
    [GenerateSerializer]
    public partial class SubmissionContent
    {
        /// <summary>
        /// 提交内容ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 提交ID
        /// </summary>
        [Id(1)]
        public long SubmissionId { get; set; }

        /// <summary>
        /// 提交内容
        /// </summary>
        [Id(2)]
        public string? Content { get; set; }
    }
}
