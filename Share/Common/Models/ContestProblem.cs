﻿using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 赛题列表
    /// </summary>
    public partial class ContestProblem
    {
        /// <summary>
        /// 赛题ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 比赛ID
        /// </summary>
        public long ContestId { get; set; }
        /// <summary>
        /// 题目ID
        /// </summary>
        public long ProblemId { get; set; }
        /// <summary>
        /// 提交数
        /// </summary>
        public long SubmissionCount { get; set; }
        /// <summary>
        /// AC数
        /// </summary>
        public long AcceptedCount { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public long Version { get; set; }
        /// <summary>
        /// 状态枚举
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 分数、语言等信息的JSON格式
        /// </summary>
        public string? Properties { get; set; }
    }
}
