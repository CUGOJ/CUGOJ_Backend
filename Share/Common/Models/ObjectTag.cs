using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题目-标签关系表
    /// </summary>
    public partial class ObjectTag
    {
        /// <summary>
        /// 主体-标签ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 主体ID
        /// </summary>
        public long TargetId { get; set; }
        /// <summary>
        /// 目标主体类型
        /// </summary>
        public int TargetType { get; set; }
        /// <summary>
        /// 标签ID
        /// </summary>
        public long TagId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
