using CUGOJ.Base.DAO.Context;

namespace CUGOJ.Silo.Init;
public static class DBInit
{
    public static void Init()
    {
        using var context = new CUGOJContext();
        if (!context.Database.CanConnect())
        {
            throw new Exception("数据库无法连接,请检查数据库连接字符串");
        }
    }
}