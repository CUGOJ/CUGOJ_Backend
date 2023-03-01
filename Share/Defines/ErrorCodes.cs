using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Defines
{
    public enum ErrorCodes
    {
        Success,
        LoginError,
        UserNotFound,
        PasswordError,
        BadParameter,
        UserExist,
        BadUsername,
        BadPassword,
        LogoutError,
        LoginNeed,
        SystemError,
        PermissionDenied,
        CreateProblemBaseNull,
        CreateProblemContentNull,
    }
}
