using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Tools.Common
{
    public static partial class CommonTools
    {
        public static long GetUTCUnixSec()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public static long GetUTCUnixMilli()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public static string ToJsonString(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                return "不支持序列化的对象" + ex.Message;
            }
        }
    }
}
