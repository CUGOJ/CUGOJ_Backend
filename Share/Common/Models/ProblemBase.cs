using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题目基本信息表
    /// </summary>
    public partial class ProblemBase
    {
        /// <summary>
        /// 题目ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 题目标题
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// 出题人ID
        /// </summary>
        public long WriterId { get; set; }
        /// <summary>
        /// 针对不同题目类型的描述JSON
        /// </summary>
        public string? Properties { get; set; }
        /// <summary>
        /// 展示的题号
        /// </summary>
        public string ShowId { get; set; } = null!;
        /// <summary>
        /// 题目来源
        /// </summary>
        public long SourceId { get; set; }
        /// <summary>
        /// 提交数
        /// </summary>
        public long? SubmissionCount { get; set; }
        /// <summary>
        /// 通过数
        /// </summary>
        public long? AcceptedCount { get; set; }
        /// <summary>
        /// 题目类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 题目状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public long? Version { get; set; }
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
