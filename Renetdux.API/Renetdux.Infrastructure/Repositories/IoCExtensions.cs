using Microsoft.Extensions.DependencyInjection;
using Renetdux.Infrastructure.Repositories.Photos;
using Renetdux.Infrastructure.Repositories.Users;

namespace Renetdux.Infrastructure.Repositories
{
    public static class IoCExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPhotoRepository, PhotoRepository>();
        }
    }
}
