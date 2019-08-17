using Renetdux.Infrastructure.Commands.Users.Interfaces;
using Renetdux.Infrastructure.Domain.Users;
using Renetdux.Infrastructure.Repositories.Users;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Commands.Users.Commands
{
    public class GetUserCommand : IGetUserCommand
    {
        private readonly IUserRepository _userRepository;

        public GetUserCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICommandResult<IUserReadOnly>> ExecuteAsync(int userId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return new CommandResult<IUserReadOnly>(ErrorCodes.User_Get_User_Doesnt_Exists.ToString(), "The requested user doesn't exists");
            }

            return new CommandResult<IUserReadOnly>(user);
        }
    }
}
