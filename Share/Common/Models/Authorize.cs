using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.Common.Models
{
    [Table("authorize")]
    [Index(nameof(GranteeId),nameof(ResourceId))]
    [GenerateSerializer]
    public class Authorize
    {
        public enum GranteeTypeEnum
        {
            User = 1,
            Organization = 2,
        }
        public enum ResourceTypeEnum
        {
            Problem = 1,
            Contest = 2,
            Organization = 3,
            Problemset = 4,
            Solution = 5,
        }
        [Comment("Id")]
        [Column("id")]
        [Id(0)]
        public long Id { get; set; }
        [Comment("被授权对象种类")]
        [Column("grantee_type")]
        [Id(1)]
        public GranteeTypeEnum GranteeType { get; set; }
        [Comment("被授权对象Id")]
        [Column("grantee_id")]
        [Id(2)]
        public long GranteeId { get; set; }
        [Comment("被授权对象的")]
        [Column("role")]
        [Id(3)]
        public int Role { get; set; }
        [Comment("资源类型")]
        [Column("resource_type")]
        [Id(4)]
        public ResourceTypeEnum ResourceType { get; set; }
        [Comment("资源Id")]
        [Column("resource_id")]
        [Id(5)]
        public long ResourceId { get; set; }
        [Comment("授权的权限集合,按位表示")]
        [Column("action")]
        [Id(6)]
        public long Action { get; set; }
        [Comment("创建时间")]
        [Column("create_time")]
        [Id(7)]
        public DateTime CreateTime { get; set; }
        [Comment("最后更新时间")]
        [Column("update_time")]
        [Id(8)]
        public DateTime UpdateTime { get; set; }
    }

    [GenerateSerializer]
    public class AuthorizeBase
    {
        [Id(0)]
        public long GranteeId { get; set; }
        [Id(1)]
        public int Role { get; set; }
        [Id(2)]
        public Authorize.ResourceTypeEnum ResourceType { get; set; }
        [Id(3)]
        public long ResourceId { get; set; }
        [Id(4)]
        public long Action { get; set; }
    }
}
