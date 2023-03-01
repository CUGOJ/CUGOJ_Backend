using System;
using System.Collections.Generic;

namespace CUGOJ.Share.Common.Models
{
    /// <summary>
    /// 用户元信息表
    /// </summary>
    [GenerateSerializer]
    public partial class UserPo : IModel
    {
        public enum UserStatusEnum
        {
            Online,
            Ban,
            Del,
            Remember
        }

        public enum  UserTypeEnum
        {
            Su,
            Admin,
            User,
            Visitor
        }

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
        public UserTypeEnum UserType { get; set; }

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
        public UserStatusEnum Status { get; set; }

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

    [GenerateSerializer]
    public class UserBase
    {
        [Id(0)]
        public long? Id { get; set; }

        [Id(1)]
        public string? Username { get; set; } = null!;

        [Id(2)]
        public string? Phone { get; set; }

        [Id(3)]
        public string? Email { get; set; }

        [Id(4)]
        public string? Signature { get; set; }

        [Id(5)]
        public long? OrganizationId { get; set; }

        [Id(6)]
        public string? Nickname { get; set; }

        [Id(7)]
        public string? Realname { get; set; }
        [Id(8)]
        public string? Avatar { get; set; }
        [Id(9)]
        public UserPo.UserTypeEnum? UserType { get; set; }
        [Id(10)]
        public UserPo.UserStatusEnum? Status { get; set; }

        public UserPo ToUserPo()
        {
            return new UserPo
            {
                Id = Id ?? 0,
                Phone = Phone,
                Email = Email,
                Signature = Signature,
                OrganizationId = OrganizationId ?? 0,
                Nickname = Nickname,
                Realname = Realname,
                Avatar = Avatar,
                UserType = UserType ?? UserPo.UserTypeEnum.User,
                Status = Status ?? UserPo.UserStatusEnum.Online
            };
        }
    }
}
