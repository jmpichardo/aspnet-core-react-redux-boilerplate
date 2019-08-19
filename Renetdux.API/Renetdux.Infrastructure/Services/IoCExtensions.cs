using Microsoft.Extensions.DependencyInjection;
using Renetdux.Infrastructure.Services.Encryption;
using Renetdux.Infrastructure.Services.Logger;
using Renetdux.Infrastructure.Services.Users;

namespace Renetdux.Infrastructure.Services
{
    public static class IoCExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
        }
    }
}
