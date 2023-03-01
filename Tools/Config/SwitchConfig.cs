using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools
{
    public static partial class Config
    {
        [ConfigItem(ConfigType =ConfigItemAttribute.ConfigTypeEnum.All)]
        public static bool ShowLoginDetail { get; set; } = false;
    }
}
