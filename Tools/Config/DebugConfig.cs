using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Tools
{
    public static partial class Config
    {
        [ConfigItem(ConfigType =ConfigItemAttribute.ConfigTypeEnum.Inject)]
        public static bool Debug { get; set; }

    }
}
