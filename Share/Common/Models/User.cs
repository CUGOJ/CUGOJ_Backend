using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 用户元信息表
    /// </summary>
    [GenerateSerializer]
    public partial class User
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
        /// 用户名
        /// </summary>
        [Id(2)]
        public string Username { get; set; } = null!;

        /// <summary>
        /// 密码
        /// </summary>
        [Id(3)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// 密码加盐
        /// </summary>
        [Id(4)]
        public string Salt { get; set; } = null!;

        /// <summary>
        /// 电话号码
        /// </summary>
        [Id(5)]
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Id(6)]
        public string? Email { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        [Id(7)]
        public string? Signature { get; set; }

        /// <summary>
        /// 所属组织
        /// </summary>
        [Id(8)]
        public long OrganizationId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Id(9)]
        public string? Nickname { get; set; }

        /// <summary>
        /// 真名
        /// </summary>
        [Id(10)]
        public string? Realname { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Id(11)]
        public string? Avatar { get; set; }

        /// <summary>
        /// 用户类型1:super admin,2:admin,3:user
        /// </summary>
        [Id(12)]
        public int UserType { get; set; }

        /// <summary>
        /// 额外信息
        /// </summary>
        [Id(13)]
        public string? Extra { get; set; }

        /// <summary>
        /// 允许访问的IP
        /// </summary>
        [Id(14)]
        public string? AllowedIp { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Id(15)]
        public int Status { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Id(16)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Id(17)]
        public DateTime CreateTime { get; set; }
    }
}
