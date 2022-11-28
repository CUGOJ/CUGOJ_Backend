using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题目-标签关系表
    /// </summary>
    [GenerateSerializer]
    public partial class ObjectTag
    {
        /// <summary>
        /// 主体-标签ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 主体ID
        /// </summary>
        [Id(1)]
        public long TargetId { get; set; }

        /// <summary>
        /// 目标主体类型
        /// </summary>
        [Id(2)]
        public int TargetType { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        [Id(3)]
        public long TagId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Id(4)]
        public int Status { get; set; }
    }
}
