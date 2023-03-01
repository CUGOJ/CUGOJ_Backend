using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools
{
    public  static partial class Config
    {
        /// <summary>
        /// 默认读锁超时时长
        /// </summary>
        public static int DefaultReaderLockTimeout { get; set; } = 1000;
        /// <summary>
        /// 默认写锁超时时长
        /// </summary>
        public static int DefaultWriterLockTimeout { get; set; } = 1000;

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

        [ConfigItem(ConfigType =ConfigItemAttribute.ConfigTypeEnum.All)]
        public static string UsernameValidatePattern { get; set; } = "^[a-zA-Z0-9_-]{4,20}";

        /// <summary>
        /// 校验密码可行度的正则表达式列表，其中第一个表达式是必须要满足的(满足后强度为0)，之后每满足一个表达式则强度+1
        /// </summary>
        [ConfigItem(ConfigType = ConfigItemAttribute.ConfigTypeEnum.All)]
        public static string[] PasswordValidatePatternList { get; set; } = new[]{
        ".{8,20}",
        "[0-9]",
        "[a-z]",
        "[A-Z]",
        "[^a-z0-9A-z]",
        };

        /// <summary>
        /// 校验密码通过的最小等级
        /// </summary>
        [ConfigItem(ConfigType = ConfigItemAttribute.ConfigTypeEnum.All)]
        public static int PasswordValidateMinLevel { get; set; } = 4;

    }
}
