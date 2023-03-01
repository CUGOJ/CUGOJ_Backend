using CUGOJ.Share.Common.Models;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Context
{
    public static class ContextTools
    {
        public readonly static string UserIdKey = "user_id";
        public static long? GetUserId()
        {
            var user = RequestContext.Get(UserIdKey);
            if (user == null) return null;
            if (user is long res)
                return res;
            return null;
        }

        public static long GetUserIdMust()
        {
            var user = GetUserId();
            if (user == null)
                throw new Exception("请先登录");
            return (long)user;
        }
    }
}
