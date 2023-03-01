using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.User
{
    public interface IUserGrain : IGrainWithIntegerKey
    {
        public Task<UserLoginResponse> Login(UserLoginModel loginModel);
        public Task<bool> CheckToken(string token);
        public Task Logout();

        public Task<IEnumerable<long>> GetProblemIdList(long offset, long limit);
    }
    [GenerateSerializer]
    public class UserLoginModel
    {
        public enum UserLoginTypeEnum
        {
            Password,
            PhoneCode,
            EmailCode
        }

        [Id(0)]
        public UserLoginTypeEnum UserLoginType { get; set; }
        [Id(1)]
        public string? Username { get; set; }
        [Id(2)]
        public string? Password { get; set; }
        [Id(3)]
        public string? Phone { get; set; }
        [Id(4)]
        public string? Email { get; set; }
        [Id(5)]
        public string? Code { get; set; }
    }

    [GenerateSerializer]
    public class UserLoginResponse
    {
        public enum UserLoginCodeEnum
        {
            Success,
            Error,
            NotFound,
            WrongPassword
        }

        [Id(0)]
        public UserLoginCodeEnum Code { get; set; }
        [Id(1)]
        public long Id { get; set; }
        [Id(2)]
        public string? Token { get; set; }
    }
}
