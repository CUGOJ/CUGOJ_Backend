using CUGOJ.Share.Common.User;
using CUGOJ.Tools.Context;
using Microsoft.AspNetCore.Http;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Frontend.Middlewares
{
    public class LoginMiddleware : IMiddleware
    {
        private readonly IClusterClient client;
        public LoginMiddleware(IClusterClient _client)
        {
            client = _client;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path != "/api/User/Login")
            {
                var idStr = context.Request.Cookies["user_id"];
                var token = context.Request.Cookies["token"];
                long id = 0;
                if (idStr != null && token != null)
                {
                    try
                    {
                        id = long.Parse(idStr);
                        var grain = client.GetGrain<IUserGrain>(id);
                        if (await grain.CheckToken(token))
                        {
                            RequestContext.Set(ContextTools.UserIdKey, id);
                        }
                        else
                        {
                            id = 0;
                        }
                    }
                    catch (Exception)
                    {
                        id = 0;
                    }
                }
                if (id == 0)
                {
                    context.Response.Cookies.Delete("user_id");
                    context.Response.Cookies.Delete("token");
                }
            }
            await next.Invoke(context);
        }
    }
}
