namespace CUGOJ.Share.DAO;
public interface IUserGrainDAO : IGrainWithIntegerKey
{
    Task<long> CreateUser();
}