using CUGOJ.Grains.Authorize;
using CUGOJ.Grains.Common.Problem;
using CUGOJ.Grains.Common.User;
using CUGOJ.Grains.Organization;
using CUGOJ.Share.Common.Authorize;
using CUGOJ.Share.Common.Organizations;
using CUGOJ.Share.Common.Problem;
using CUGOJ.Share.Common.User;
using CUGOJ.Share.Infra;
using CUGOJ.Tools.Infra.ConfigProvider;
using CUGOJ.Tools.Infra.HttpProvider;
using CUGOJ.Tools.Log;

namespace CUGOJ.Silo.Init
{
    public class ServiceInit
    {
        public static void InitService(IServiceCollection service)
        {
            service.AddSingleton<IHttpProvider, HttpProvider>();
            service.AddSingleton<IConfigProvider, ConfigProvider>();
            service.AddSingleton<Logger, Logger>();
            service.AddSingleton<IUserService, UserServiceBase>();
            service.AddSingleton<IProblemServiceBase, ProblemServiceBase>();
            service.AddSingleton<IAuthorizeService, AuthorizeService>();
            service.AddSingleton<IOrganizationService, OrganizationService>();
        }

        public static void LoadSingletonService(IServiceProvider serviceProvider)
        {
            var configProvider = serviceProvider.GetRequiredService<IConfigProvider>();

            ConfigItemAttribute.ConfigProvider = configProvider;
        }
    }
}
