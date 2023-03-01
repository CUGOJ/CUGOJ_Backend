using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.VersionContainer
{
    /// <summary>
    /// 用于增量分发模块的统一模型接口
    /// </summary>
    public interface IVersionItem
    {
        public long Version { get; set; }
    }
}
