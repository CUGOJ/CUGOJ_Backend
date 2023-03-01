using CUGOJ.Base.DAO.Context;
using CUGOJ.Share.Common.Models;
using CUGOJ.Share.Common.User;
using CUGOJ.Share.Defines;
using CUGOJ.Tools.Common;
using CUGOJ.Tools.Validate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.Common.User
{
    public class UserServiceBase : IUserService
    {
        public MemoryCache usernameIdCache = new(new MemoryCacheOptions());
        public MemoryCache userBaseInfoCache = new(new MemoryCacheOptions());

        public async Task<long> GetUserIdByUserName(string username)
        {
            if (usernameIdCache.TryGetValue<long>(username, out var id))
                return id;
            using var context = new CUGOJContext();
            id = await context.Users.Where(u => u.Username == username).Select(u => u.Id).FirstOrDefaultAsync();
            if (id == 0)
                usernameIdCache.Set(username, id, new TimeSpan(0, 1, 0));
            else
                usernameIdCache.Set(username, id);
            return id;
        }

        public async Task<IEnumerable<UserBase>> MulGetUserBaseInfo(IEnumerable<long> idList)
        {
            List<UserBase> res = new();
            List<long> missIdList = new();
            foreach (var id in idList)
            {
                if (userBaseInfoCache.TryGetValue(id, out UserBase userBaseInfo))
                    res.Add(userBaseInfo);
                else
                    missIdList.Add(id);
            }
            using var context = new CUGOJContext();
            var missUserList = await (from u in context.Users
                                      where missIdList.Contains(u.Id)
                                      select new UserBase()
                                      {
                                          Id = u.Id,
                                          Signature = u.Signature,
                                          OrganizationId = u.OrganizationId,
                                          Nickname = u.Nickname,
                                          Avatar = u.Avatar,
                                          UserType = u.UserType,
                                          Status = u.Status
                                      }).ToListAsync();
            foreach (var user in missUserList)
            {
                userBaseInfoCache.Set(user.Id, user, new TimeSpan(0, 0, 30));
            }
            res.AddRange(missUserList);
            return res;
        }

        public async Task<ErrorCodes> RegisterNewUser(UserRegisterModel req)
        {
            if (!ValidateTools.CheckUsername(req.Username))
                return ErrorCodes.BadUsername;
            // TODO 实现非密码注册
            if (!ValidateTools.CheckPassword(req.Password!))
                return ErrorCodes.BadPassword;
            // TODO 校验验证码
            if (await GetUserIdByUserName(req.Username) != 0)
                return ErrorCodes.UserExist;

            var newUser = req.UserBaseInfo == null ? new UserPo() : req.UserBaseInfo.ToUserPo();
            newUser.Id = 0;
            newUser.Status = UserPo.UserStatusEnum.Online;
            newUser.AllowedIp = null;
            newUser.Extra = "{}";
            newUser.OrganizationId = 0;
            newUser.UserType = UserPo.UserTypeEnum.User;
            newUser.Username = req.Username;
            newUser.Salt = Guid.NewGuid().ToString();
            newUser.Password = CommonTools.GetMD5(req.Password!, newUser.Salt);
            using var context = new CUGOJContext();
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return ErrorCodes.Success;
        }
    }
}
