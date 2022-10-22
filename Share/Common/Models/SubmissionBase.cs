using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 提交基本信息表
    /// </summary>
    public partial class SubmissionBase
    {
        /// <summary>
        /// 提交ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitTime { get; set; }
        /// <summary>
        /// 提交者ID
        /// </summary>
        public long SubmitterId { get; set; }
        /// <summary>
        /// 提交者类型（团队或个人）
        /// </summary>
        public int SubmitterType { get; set; }
        /// <summary>
        /// 提交结果
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 提交类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 关联的比赛
        /// </summary>
        public long? ContestId { get; set; }
        /// <summary>
        /// 关联的题目
        /// </summary>
        public long? ProblemId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 特定配置JSON
        /// </summary>
        public string? Properties { get; set; }
    }
}
