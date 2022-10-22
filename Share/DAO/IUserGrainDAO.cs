namespace CUGOJ.Backend.Share.DAO;
public interface IUserGrainDAO : Orleans.IGrainWithIntegerKey
{
    Task<long> CreateUser();
}