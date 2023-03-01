using System;
using System.Collections.Generic;

namespace CUGOJ.Share.Common.Models
{
    /// <summary>
    /// 队员表
    /// </summary>
    public partial class TeamUserPo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 队伍Id
        /// </summary>
        public long TeamId { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
