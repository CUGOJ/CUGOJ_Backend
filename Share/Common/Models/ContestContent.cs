using System;
using System.Collections.Generic;

namespace CUGOJ.Backend.Share.Common.Models
{
    /// <summary>
    /// 比赛文字内容列表
    /// </summary>
    [GenerateSerializer]
    public partial class ContestContent
    {
        /// <summary>
        /// 比赛内容ID
        /// </summary>
        [Id(0)]
        public long Id { get; set; }

        /// <summary>
        /// 比赛ID
        /// </summary>
        [Id(1)]
        public long ContestId { get; set; }

        /// <summary>
        /// 赛事描述文字
        /// </summary>
        [Id(2)]
        public string? Content { get; set; }
    }
}
