using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 用户登录记录表
    /// </summary>
    public partial class UserLogin
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        public long Ip { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; } = null!;
        /// <summary>
        /// 平台
        /// </summary>
        public int Platform { get; set; }
        /// <summary>
        /// 登录类型
        /// </summary>
        public int LoginType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
