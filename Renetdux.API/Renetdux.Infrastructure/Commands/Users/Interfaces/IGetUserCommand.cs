using Renetdux.Infrastructure.Domain.Users;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Commands.Users.Interfaces
{
    public interface IGetUserCommand
    {
        Task<ICommandResult<IUserReadOnly>> ExecuteAsync(int userId);
    }
}
