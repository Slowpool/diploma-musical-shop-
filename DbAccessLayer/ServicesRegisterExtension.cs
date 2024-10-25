using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace DbAccessLayer;

public static class ServicesRegisterExtension
{
    public static void RegisterDbAccessLayer(this IServiceCollection services)
    {
        services.RegisterAssemblyPublicNonGenericClasses()
                .Where(@class => @class.Name.EndsWith("DbAccess"))
                .AsPublicImplementedInterfaces();
    }
}
