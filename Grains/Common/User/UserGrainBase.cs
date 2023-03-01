using CUGOJ.Tools.Common;
using CUGOJ.Base.DAO.Context;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.User;
using CUGOJ.Tools.Exceptions;
using CUGOJ.Tools.Log;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CUGOJ.Share.Common.Organizations;
using CUGOJ.Tools.Common.Organizations;
using CUGOJ.Tools.Common.AuthorizedList;
using CUGOJ.Share.Common.Authorize;

namespace CUGOJ.Grains.Common.User
{
    public partial class UserGrainBase : Grain, IUserGrain
    {
        private readonly Logger? logger;
        private readonly IOrganizationService organizationService;
        private readonly IAuthorizeService authorizeService;
        private readonly IOrganizationSet organizationSet;
        private readonly AuthorizedList<ProblemAuthorize> problemList;

        public UserGrainBase(Logger? logger, IOrganizationService organizationService, IAuthorizeService authorizeService)
        {
            this.logger = logger;
            this.organizationService = organizationService;
            this.authorizeService = authorizeService;
            organizationSet = new OrganizationSet(this.organizationService);
            problemList = new AuthorizedList<ProblemAuthorize>(this.authorizeService, organizationSet);
        }

        public long UserId { get=> this.GetPrimaryKeyLong(); }
        public UserPo? User { get; private set; }

        private UserExtra? extra;

        public async Task<UserExtra?> GetExtra()
        {
            if (extra == null)
                await ParseUserExtra();
            return extra;
        }

        [TimerLock(10000)]
        private async Task ParseUserExtra()
        {
            if (User == null)
                await LoadUser();
            if (User != null)
            {
                if (User.Extra == null)
                {
                    extra = new();
                }
                else
                {
                    extra = CommonTools.TryParseJson<UserExtra>(User.Extra);
                    if (extra==null)
                    {
                        logger?.Warn($"用户Extra信息出错,User = {CommonTools.ToJsonString(User)}");
                    }
                }
            }
            else
            {
                extra = null;
            }
        }

        [TimerLock(10000)]
        private async Task LoadUser()
        {
            try
            {
                using var context = new CUGOJContext();
                User = await context.Users.Where(u => u.Id == UserId).FirstOrDefaultAsync();
            }
            catch (Exception) { }
        }

        public async Task<UserLoginResponse> Login(UserLoginModel loginModel)
        {
            UserLoginResponse res = new()
            {
                Code = UserLoginResponse.UserLoginCodeEnum.Error
            };
            if(loginModel.UserLoginType==UserLoginModel.UserLoginTypeEnum.Password)
            {
                var user = await GetUser();
                if (user == null)
                {
                    res.Code = UserLoginResponse.UserLoginCodeEnum.NotFound;
                    return res;
                }
                if (CommonTools.GetMD5(loginModel.Password!, user.Salt) == user.Password) 
                {
                    await MergeExtra(new UserExtra { Token = Guid.NewGuid().ToString() });
                    var extra = await GetExtra();
                    if (extra == null)
                        throw Exceptions.Todo("系统出现错误", ExceptionTypeEnum.SystemError);
                    res.Id = UserId;
                    res.Token = extra.Token;
                    res.Code = UserLoginResponse.UserLoginCodeEnum.Success;
                    return res;
                }
                res.Code = UserLoginResponse.UserLoginCodeEnum.WrongPassword;
                return res;
            }
            return res;
        }

        public async Task<UserPo>GetUser()
        {
            if (User == null)
                await LoadUser();
            if (User == null)
                throw Exceptions.Todo("找不到用户", ExceptionTypeEnum.DataNotFound);
            return User;
        }

        public async Task SetExtra(UserExtra newExtra)
        {
            if (User == null)
                await LoadUser();
            if (User == null)
                throw Exceptions.New("找不到用户", ExceptionTypeEnum.DataNotFound,0L);
            using var context = new CUGOJContext();
            var user = await (from u in context.Users where u.Id == UserId select u).FirstOrDefaultAsync();
            if (user == null)
                throw Exceptions.Todo("找不到用户", ExceptionTypeEnum.DataNotFound);
            var extraStr = CommonTools.ToJsonString(newExtra);
            user.Extra = extraStr;
            await context.SaveChangesAsync();

            User.Extra = extraStr;
            extra = newExtra;
        }



        public async Task MergeExtra(UserExtra newExtra)
        {
            if (User == null)
                await LoadUser();
            if (User == null)
                throw Exceptions.Todo("找不到用户", ExceptionTypeEnum.DataNotFound);
            using var context = new CUGOJContext();
            var user = await (from u in context.Users where u.Id == UserId select u).FirstOrDefaultAsync();
            if (user == null)
                throw Exceptions.Todo("找不到用户", ExceptionTypeEnum.DataNotFound);
            newExtra = CommonTools.MergeObject(await GetExtra(), newExtra);
            var extraStr = CommonTools.ToJsonString(newExtra);
            user.Extra = extraStr;
            await context.SaveChangesAsync();

            User.Extra = extraStr;
            extra = newExtra;
        }

        public async Task<bool> CheckToken(string token)
        {
            var extra = await GetExtra();
            if (extra == null) return false;
            if (extra.Token == null || extra.Token == string.Empty) return false;
            return extra.Token == token;
        }

        public async Task Logout()
        {
            await MergeExtra(new UserExtra { Token = string.Empty });
        }

        public async Task<IEnumerable<long>> GetProblemIdList(long offset, long limit)
        {
            var items = await problemList.GetItems(offset, limit, auth =>
            {
                return auth.Listable;
            });
            return items.Select(p => p.Id);
        }
    }
}
