using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Common
{
    public static partial class CommonTools
    {
        public static T ConvertString<T>(string src) where T : IConvertible
        {
            return (T)Convert.ChangeType(src, typeof(T));
        }

        public static T? ParseString<T>(string src)
        {
            return (T?)ParseString(typeof(T), src);
        }
        public static object? ParseString(Type type,string src)
        {
            string? msg = null;
            if (typeof(IConvertible).IsAssignableFrom(type))
            {
                try
                {
                    return Convert.ChangeType(src, type);
                }
                catch (Exception ex)
                {
                    msg = $"{src}无法被直接解析为{type.Name}类型,Exception={ex.Message}";
                }
            }
            try
            {
                return JsonSerializer.Deserialize(src, type);
            }
            catch (Exception ex)
            {
                if (msg != null)
                    msg += $"\n且无法作为Json串被解析为{type.Name}类型,Exception={ex.Message}";
                else
                    msg = $"{src}无法作为Json被解析为{type.Name}类型,Exception={ex.Message}";
                throw new Exception(msg);
            }
        }
        public static T? TryParseJson<T>(string src)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(src);
            }
            catch (Exception) 
            {

            }
            return default;
        }
    }
}
