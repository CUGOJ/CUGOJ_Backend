using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 用户元信息表
    /// </summary>
    public partial class User
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
        /// 用户名
        /// </summary>
        public string Username { get; set; } = null!;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = null!;
        /// <summary>
        /// 密码加盐
        /// </summary>
        public string Salt { get; set; } = null!;
        /// <summary>
        /// 电话号码
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string? Signature { get; set; }
        /// <summary>
        /// 所属组织
        /// </summary>
        public long OrganizationId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string? Nickname { get; set; }
        /// <summary>
        /// 真名
        /// </summary>
        public string? Realname { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }
        /// <summary>
        /// 用户类型1:super admin,2:admin,3:user
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 额外信息
        /// </summary>
        public string? Extra { get; set; }
        /// <summary>
        /// 允许访问的IP
        /// </summary>
        public string? AllowedIp { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
