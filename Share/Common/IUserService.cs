namespace CUGOJ.Backend.Share.Common;

public interface IUserService : Orleans.Services.IGrainService
{
    public enum SignUpResult
    {
        OK,
        ErrorCode,
    }
    public Task<SignUpResult> SignUp();
}