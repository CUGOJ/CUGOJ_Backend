using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Common
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
                return System.Text.Json.JsonSerializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                return "不支持序列化的对象" + ex.Message;
            }
        }

        /// <summary>
        /// 将给定的两个对象进行合并，仅对可空字段有效
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldObj"></param>
        /// <param name="newObj"></param>
        /// <returns></returns>
        public static T MergeObject<T>(T? oldObj,T newObj) where T:notnull
        {
            if (oldObj == null) return newObj;
            var oldJsonObj = JToken.FromObject(oldObj) as JObject;
            var newJsonObj = JToken.FromObject(newObj) as JObject;
            oldJsonObj!.Merge(newJsonObj!, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Replace,
                MergeNullValueHandling = MergeNullValueHandling.Ignore,
            });
            return oldJsonObj.ToObject<T>();
        }

        public static string GetMD5(string src,string? salt)
        {
            if (salt != null) src += salt;
            MD5 md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(src));
            return Convert.ToBase64String(bytes);
        }
    }
}
