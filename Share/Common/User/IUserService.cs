using CUGOJ.Share.Defines;
using Orleans.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.User
{
    public interface IUserService
    {
        Task<long> GetUserIdByUserName(string usernmae);
        Task<ErrorCodes> RegisterNewUser(UserRegisterModel req);
        Task<IEnumerable<UserBase>> MulGetUserBaseInfo(IEnumerable<long> idList);

    }

    public class UserRegisterModel
    {
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public UserBase? UserBaseInfo { get; set; }
    }

}
