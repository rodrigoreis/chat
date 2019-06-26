using Chat.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Chat.Repositories.DependencyInjections
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryAddSingleton<IConnectionsRepository, ConnectionsRepository>();

            return services;
        }
    }
}
