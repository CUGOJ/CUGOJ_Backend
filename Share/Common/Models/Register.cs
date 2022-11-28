using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 比赛注册表
    /// </summary>
    [GenerateSerializer]
    public partial class Register
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 比赛ID
        /// </summary>
        [Id(1)]
        public long ContestId { get; set; }

        /// <summary>
        /// 注册人ID
        /// </summary>
        [Id(2)]
        public long RegistrantId { get; set; }

        /// <summary>
        /// 注册人类型
        /// </summary>
        [Id(3)]
        public int RegistrantType { get; set; }

        /// <summary>
        /// 队伍ID
        /// </summary>
        [Id(4)]
        public long? TeamId { get; set; }

        /// <summary>
        /// 额外信息
        /// </summary>
        [Id(5)]
        public string? Extra { get; set; }

        /// <summary>
        /// 比赛状态枚举
        /// </summary>
        [Id(6)]
        public int Status { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Id(7)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Id(8)]
        public DateTime CreateTime { get; set; }
    }
}
