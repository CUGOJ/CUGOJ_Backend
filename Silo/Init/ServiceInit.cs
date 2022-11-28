using CUGOJ.Backend.Base.Infra.ConfigProvider;
using CUGOJ.Backend.Share.Infra;
using CUGOJ.Backend.Tools;
using CUGOJ.Backend.Tools.Infra.HttpProvider;
using CUGOJ.Backend.Tools.Log;

namespace CUGOJ.Backend.Silo.Init
{
    public class ServiceInit
    {
        public static void InitService(IServiceCollection service)
        {
            service.AddSingleton<IHttpProvider, HttpProvider>();
            service.AddSingleton<IConfigProvider, ConfigProvider>();
            service.AddSingleton<Logger, Logger>();
        }

        public static void LoadSingletonService(IServiceProvider serviceProvider)
        {
            var configProvider = serviceProvider.GetRequiredService<IConfigProvider>();

            ConfigItemAttribute.ConfigProvider = configProvider;
        }
    }
}
