using Renetdux.Infrastructure.Commands.Users.Interfaces;
using Renetdux.Infrastructure.Domain.Users;
using Renetdux.Infrastructure.Repositories.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Commands.Users.Commands
{
    public class GetUsersCommand : IGetUsersCommand
    {
        private readonly IUserRepository _userRepository;

        public GetUsersCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICommandResult<IEnumerable<IUserReadOnly>>> ExecuteAsync()
        {
            var users = await _userRepository.GetAllAsync(orderBy: u => u.OrderBy(x => x.Email));

            return new CommandResult<IEnumerable<IUserReadOnly>>(users);
        }
    }
}
