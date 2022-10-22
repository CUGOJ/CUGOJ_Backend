using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 标签表
    /// </summary>
    public partial class Tag
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 标签颜色
        /// </summary>
        public string? Color { get; set; }
        /// <summary>
        /// 目标主体类型
        /// </summary>
        public int TargetType { get; set; }
        /// <summary>
        /// 配置项JSON
        /// </summary>
        public string? Properties { get; set; }
        /// <summary>
        /// 状态枚举
        /// </summary>
        public int Status { get; set; }
    }
}
