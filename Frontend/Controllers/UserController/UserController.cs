using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.User;
using CUGOJ.Share.Defines;
using CUGOJ.Tools;
using CUGOJ.Tools.Context;
using CUGOJ.Tools.Log;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Frontend.Controllers.UserController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController:ControllerBase
    {
        private readonly IClusterClient client;
        private readonly IUserService userService;
        private readonly Logger logger;

        public UserController(IUserService userService, IClusterClient client, Logger logger)
        {
            this.userService = userService;
            this.client = client;
            this.logger = logger;
        }

        [HttpPost]
        public async Resp Register(UserRegisterModel registerParam)
        {
            try
            {
                return Code(await userService.RegisterNewUser(registerParam));
            }
            catch (Exception e)
            {
                logger.Error($"{e.Message},{e.StackTrace}");
                if (Config.ShowLoginDetail)
                    return Exception(e);
                else
                    return Code(ErrorCodes.LoginError);
            }
        }

        [HttpPost]
        public async Resp Login([FromBody]UserLoginModel loginParam)
        {
            try
            {
                if (loginParam.UserLoginType == UserLoginModel.UserLoginTypeEnum.Password)
                {
                    if (loginParam.Username == null || loginParam.Password == null)
                        return Code(ErrorCodes.BadParameter);
                    var id = await userService.GetUserIdByUserName(loginParam.Username);
                    if (id == 0)
                    {
                        if (Config.ShowLoginDetail)
                            return Code(ErrorCodes.UserNotFound);
                        else
                            return Code(ErrorCodes.LoginError);
                    }
                    var grain = client.GetGrain<IUserGrain>(id);
                    var res = await grain.Login(loginParam);
                    if (res.Code == UserLoginResponse.UserLoginCodeEnum.Success)
                    {
                        var options = new Microsoft.AspNetCore.Http.CookieOptions
                        {
                        };

                        if (Config.Debug)
                        {
                            options.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                            options.Secure= true;
                        }

                        HttpContext.Response.Cookies.Append("user_id", res.Id.ToString(), options);
                        HttpContext.Response.Cookies.Append("token", res.Token!, options);
                        return Code(ErrorCodes.Success);
                    }
                    else
                    {
                        return Code(ErrorCodes.LoginError);
                    }
                }
                return Code(ErrorCodes.BadParameter);
            }
            catch (Exception e)
            {
                logger.Error($"{e.Message},{e.StackTrace}");
                //if (Config.ShowLoginDetail)
                //    return BadRequest(e.Message);
                //else
                return Exception(e, ErrorCodes.LoginError);
            }
            
        }

        [HttpPost]
        public async Resp Logout()
        {
            try
            {
                HttpContext.Response.Cookies.Delete("user_id");
                HttpContext.Response.Cookies.Delete("token");
                var id = ContextTools.GetUserId();
                if (id ==null||id==0)
                {
                    return Code(ErrorCodes.LoginNeed);
                }
                var grain = client.GetGrain<IUserGrain>((long)id);
                await grain.Logout();
                return Code(ErrorCodes.Success);
            }
            catch (Exception e)
            {
                logger.Error($"{e.Message},{e.StackTrace}");
                return Exception(e,ErrorCodes.LogoutError);
            }
        }

        [HttpGet]
        public async Resp GetUserBaseInfo([FromQuery] IEnumerable<long>idList)
        {
            try
            {
                if (idList.Count() > 20)
                    return Code(ErrorCodes.BadParameter);
                return Content(await userService.MulGetUserBaseInfo(idList));
            }
            catch(Exception e)
            {
                logger.Exception(e);
                return Exception(e);
            }
        }
    }
}
