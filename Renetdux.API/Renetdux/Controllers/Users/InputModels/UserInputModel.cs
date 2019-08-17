using Renetdux.Infrastructure.Commands.Users.Models;

namespace Renetdux.Controllers.Users.InputModels
{
    public class UserInputModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public UserDTO ToDTO()
        {
            return new UserDTO()
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password
            };
        }
    }
}
