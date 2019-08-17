using Microsoft.Extensions.DependencyInjection;
using Renetdux.Infrastructure.Commands.Users;
using Renetdux.Infrastructure.Repositories;
using Renetdux.Infrastructure.Services;

namespace Renetdux
{
    public static class IoCContainerFactory
    {
        public static void MapInterfaces(IServiceCollection services)
        {
            services.AddUsersCommands();

            services.AddRepositories();
            services.AddServices();
        }
    }
}
