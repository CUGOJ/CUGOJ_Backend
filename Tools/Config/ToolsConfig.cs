using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Tools
{
    public  static partial class Config
    {
        /// <summary>
        /// 在版本管理模块中,请求读锁超时时长，单位ms
        /// </summary>
        [ConfigItem]
        public static int VersionItemMangerReaderLockTimeout { get; set; } = 1000;

        /// <summary>
        /// 在版本管理模块中，请求写锁超时时长，单位ms
        /// </summary>
        [ConfigItem]
        public static int VersionItemManagerWriterLockTimeout { get; set; } = 3000;

        /// <summary>
        /// 版本管理维护的最大快照数量,成本较低，10000快照约需要1MB内存
        /// </summary>
        [ConfigItem]
        public static int VersionItemSnapshotLimit { get; set; } = 10000;

    }
}
