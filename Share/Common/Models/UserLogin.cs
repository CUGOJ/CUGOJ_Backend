using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 用户登录记录表
    /// </summary>
    [GenerateSerializer]
    public partial class UserLogin
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Id(1)]
        public long UserId { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        [Id(2)]
        public long Ip { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [Id(3)]
        public string DeviceId { get; set; } = null!;

        /// <summary>
        /// 平台
        /// </summary>
        [Id(4)]
        public int Platform { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        [Id(5)]
        public int LoginType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Id(6)]
        public DateTime Time { get; set; }
    }
}
