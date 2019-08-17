using Renetdux.Infrastructure.Commands.Users.Interfaces;
using Renetdux.Infrastructure.Commands.Users.Models;
using Renetdux.Infrastructure.Domain.Users;
using Renetdux.Infrastructure.Repositories;
using Renetdux.Infrastructure.Repositories.Users;
using Renetdux.Infrastructure.Services.Users;
using System;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Commands.Users.Commands
{
    public class RegisterUserCommand : IRegisterUserCommand
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommand(
            IUserService userService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult<IUserReadOnly>> ExecuteAsync(UserDTO userInput)
        {
            var result = _userService.ValidateUser(userInput.Email, userInput.FirstName, userInput.LastName, userInput.Password);
            if (!result.IsSuccess)
                return new CommandResult<IUserReadOnly>(ErrorCodes.User_Registration_Invalid_Data.ToString(), result.ErrorMessage);

            var existingUser = await _userRepository.FirstOrDefaultAsync(u => u.Email.Equals(userInput.Email, StringComparison.InvariantCultureIgnoreCase));
            if (existingUser != null)
                return new CommandResult<IUserReadOnly>(ErrorCodes.User_Registration_Existing_Email.ToString(), $"User with email {userInput.Email} already exists.");

            var user = new User(
                userInput.Email,
                userInput.FirstName,
                userInput.LastName,
                userInput.Password);

            await _userRepository.AddAsync(user);

            await _unitOfWork.CommitAsync();

            return new CommandResult<IUserReadOnly>(user);
        }
    }
}
