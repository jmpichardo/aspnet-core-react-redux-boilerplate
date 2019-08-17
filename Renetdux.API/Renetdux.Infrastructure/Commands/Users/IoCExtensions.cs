using Microsoft.Extensions.DependencyInjection;
using Renetdux.Infrastructure.Commands.Users.Commands;
using Renetdux.Infrastructure.Commands.Users.Interfaces;

namespace Renetdux.Infrastructure.Commands.Users
{
    public static class IoCExtensions
    {
        public static void AddUsersCommands(this IServiceCollection services)
        {
            services.AddTransient<IGetUserCommand, GetUserCommand>();
            services.AddTransient<IGetUsersCommand, GetUsersCommand>();
            services.AddTransient<IRegisterUserCommand, RegisterUserCommand>();
        }
    }
}
