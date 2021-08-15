using Microsoft.Extensions.DependencyInjection;

namespace Api.Test.Partner
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPartnerServices(this IServiceCollection collection)
        {
            collection
                .Scan(
                    x => x
                        .FromApplicationDependencies()
                        .AddClasses(classes => classes.AssignableTo<IPartnerService>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                );
        }
    }
}