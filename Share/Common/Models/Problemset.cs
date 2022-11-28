using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 题单表
    /// </summary>
    [GenerateSerializer]
    public partial class Problemset
    {
        /// <summary>
        /// 题单ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 题单名称
        /// </summary>
        [Id(1)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// 创建者ID
        /// </summary>
        [Id(2)]
        public long CreatorId { get; set; }

        /// <summary>
        /// 简短描述
        /// </summary>
        [Id(3)]
        public string? Description { get; set; }

        /// <summary>
        /// 状态枚举
        /// </summary>
        [Id(4)]
        public int Status { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Id(5)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Id(6)]
        public DateTime CreateTime { get; set; }
    }
}
