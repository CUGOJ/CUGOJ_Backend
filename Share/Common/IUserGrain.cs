namespace CUGOJ.Backend.Share.Common;
public interface IUserGrain : Orleans.IGrainWithIntegerKey
{
    Task<bool> Login(string username, string password);
    Task<bool> Logout();
    Task<bool> ChangePassword(string oldPassword, string newPassword);
    Task<bool> UpdateInfo(User userInfo);
}