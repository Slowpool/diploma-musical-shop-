using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace ServiceLayer;
public static class ServicesRegisterExtension
{
    public static void RegisterServiceLayer(this IServiceCollection services)
    {
        services.RegisterAssemblyPublicNonGenericClasses()
                .Where(@class => @class.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();
    }
}
