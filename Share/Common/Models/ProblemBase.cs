using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题目基本信息表
    /// </summary>
    [GenerateSerializer]
    public partial class ProblemBase
    {
        /// <summary>
        /// 题目ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 题目标题
        /// </summary>
        [Id(1)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// 出题人ID
        /// </summary>
        [Id(2)]
        public long WriterId { get; set; }

        /// <summary>
        /// 针对不同题目类型的描述JSON
        /// </summary>
        [Id(3)]
        public string? Properties { get; set; }

        /// <summary>
        /// 展示的题号
        /// </summary>
        [Id(4)]
        public string ShowId { get; set; } = null!;

        /// <summary>
        /// 题目来源
        /// </summary>
        [Id(5)]
        public long SourceId { get; set; }

        /// <summary>
        /// 提交数
        /// </summary>
        [Id(6)]
        public long? SubmissionCount { get; set; }

        /// <summary>
        /// 通过数
        /// </summary>
        [Id(7)]
        public long? AcceptedCount { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>
        [Id(8)]
        public int Type { get; set; }

        /// <summary>
        /// 题目状态
        /// </summary>
        [Id(9)]
        public int Status { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [Id(10)]
        public long? Version { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Id(11)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Id(12)]
        public DateTime CreateTime { get; set; }
    }
}
