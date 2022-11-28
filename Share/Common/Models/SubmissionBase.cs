using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 提交基本信息表
    /// </summary>
    [GenerateSerializer]
    public partial class SubmissionBase
    {
        /// <summary>
        /// 提交ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        [Id(1)]
        public DateTime SubmitTime { get; set; }

        /// <summary>
        /// 提交者ID
        /// </summary>
        [Id(2)]
        public long SubmitterId { get; set; }

        /// <summary>
        /// 提交者类型（团队或个人）
        /// </summary>
        [Id(3)]
        public int SubmitterType { get; set; }

        /// <summary>
        /// 提交结果
        /// </summary>
        [Id(4)]
        public int Status { get; set; }

        /// <summary>
        /// 提交类型
        /// </summary>
        [Id(5)]
        public int Type { get; set; }

        /// <summary>
        /// 关联的比赛
        /// </summary>
        [Id(6)]
        public long? ContestId { get; set; }

        /// <summary>
        /// 关联的题目
        /// </summary>
        [Id(7)]
        public long? ProblemId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Id(8)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Id(9)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 特定配置JSON
        /// </summary>
        [Id(10)]
        public string? Properties { get; set; }
    }
}
