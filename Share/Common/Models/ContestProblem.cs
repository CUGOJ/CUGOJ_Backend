using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 赛题列表
    /// </summary>
    [GenerateSerializer]
    public partial class ContestProblem
    {
        /// <summary>
        /// 赛题ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 比赛ID
        /// </summary>
        [Id(1)]
        public long ContestId { get; set; }

        /// <summary>
        /// 题目ID
        /// </summary>
        [Id(2)]
        public long ProblemId { get; set; }

        /// <summary>
        /// 提交数
        /// </summary>
        [Id(3)]
        public long SubmissionCount { get; set; }

        /// <summary>
        /// AC数
        /// </summary>
        [Id(4)]
        public long AcceptedCount { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [Id(5)]
        public long Version { get; set; }

        /// <summary>
        /// 状态枚举
        /// </summary>
        [Id(6)]
        public int Status { get; set; }

        /// <summary>
        /// 分数、语言等信息的JSON格式
        /// </summary>
        [Id(7)]
        public string? Properties { get; set; }
    }
}
