using Api.Test.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Test.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection collection)
        {
            collection.AddDbContext<CustomerContext>(
                (provider, cfg) => cfg.UseSqlServer(
                    provider.GetService<IConfiguration>()
                        .GetConnectionString(Constraints.ApiConnectionStringName)
                )
            );

            collection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}