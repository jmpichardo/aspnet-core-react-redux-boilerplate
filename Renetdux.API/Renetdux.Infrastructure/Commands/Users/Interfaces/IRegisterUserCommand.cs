using Renetdux.Infrastructure.Commands.Users.Models;
using Renetdux.Infrastructure.Domain.Users;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Commands.Users.Interfaces
{
    public interface IRegisterUserCommand
    {
        Task<ICommandResult<IUserReadOnly>> ExecuteAsync(UserDTO userInput);
    }
}
