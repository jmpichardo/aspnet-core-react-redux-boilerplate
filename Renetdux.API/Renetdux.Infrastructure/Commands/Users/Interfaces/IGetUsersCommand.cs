using Renetdux.Infrastructure.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Commands.Users.Interfaces
{
    public interface IGetUsersCommand
    {
        Task<ICommandResult<IEnumerable<IUserReadOnly>>> ExecuteAsync();
    }
}
