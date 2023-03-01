using System;
using System.Collections.Generic;

namespace CUGOJ.Share.Common.Models
{
    /// <summary>
    /// 标签表
    /// </summary>
    [GenerateSerializer]
    public partial class TagPo
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [Id(1)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 标签颜色
        /// </summary>
        [Id(2)]
        public string? Color { get; set; }

        /// <summary>
        /// 目标主体类型
        /// </summary>
        [Id(3)]
        public int TargetType { get; set; }

        /// <summary>
        /// 配置项JSON
        /// </summary>
        [Id(4)]
        public string? Properties { get; set; }

        /// <summary>
        /// 状态枚举
        /// </summary>
        [Id(5)]
        public int Status { get; set; }
    }
}
