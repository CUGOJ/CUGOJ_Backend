using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CUGOJ.Tools.Validate
{
    public static class ValidateTools
    {
        public static bool CheckUsername(string username)
        {
            return Regex.IsMatch(username,Config.UsernameValidatePattern);
        }
        
        public static bool CheckPassword(string password)
        {
            if (Config.PasswordValidatePatternList.Length == 0)
                throw Exceptions.Exceptions.Todo("系统出错,请稍后重试", Exceptions.ExceptionTypeEnum.SystemError);
            if (!Regex.IsMatch(password, Config.PasswordValidatePatternList[0]))
                return false;
            int level = 0;
            foreach (var parttern in Config.PasswordValidatePatternList.Skip(1))
            {
                if (Regex.IsMatch(password, parttern))
                    level++;
            }
            return level >= Config.PasswordValidateMinLevel;
        }
    }
}
