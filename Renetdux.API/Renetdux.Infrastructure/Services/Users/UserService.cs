using Renetdux.Infrastructure.ExtensionMethods;
using System.Linq;

namespace Renetdux.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        public IResultModel<bool> ValidateUser(string email, string firstName, string lastName, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ResultModel<bool>(false, "The email cannot be null or empty");

            if (string.IsNullOrWhiteSpace(firstName))
                return new ResultModel<bool>(false, "The first name cannot be null or empty");

            if (string.IsNullOrWhiteSpace(lastName))
                return new ResultModel<bool>(false, "The last name cannot be null or empty");

            if (string.IsNullOrWhiteSpace(password))
                return new ResultModel<bool>(false, "The password cannot be null or empty");

            if (!email.IsValidEmail())
                return new ResultModel<bool>(false, "The provided email address is not valid");

            if (!IsValidPassword(password, out var errorMessage))
                return new ResultModel<bool>(false, errorMessage);

            return new ResultModel<bool>(true);
        }

        public bool IsValidPassword(string password, out string errorMessage)
        {
            // Valid password = It must be between 6 and 200 characters long. It must contain any letter and numbers.

            //char[] special_characters = {
            //    ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+',
            //    ',' , '-', '.', '/', ':' ,';', '<', '=', '>', '?', '@', '[',
            //    '\\', ']', '^', '_', '`', '{', '|', '}', '~' };

            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Password is null or empty";
                return false;
            }

            if (password.Length < 6)
            {
                errorMessage = "Invalid password: Too short";
                return false;
            }

            if (password.Length > 200)
            {
                errorMessage = "Invalid password: Too long";
                return false;
            }

            if (!password.Any(x => char.IsDigit(x)))
            {
                errorMessage = "Invalid password: No numbers";
                return false;
            }

            if (!password.Any(x => char.IsLetter(x)))
            {
                errorMessage = "Invalid password: No letters";
                return false;
            }

            //if (!password.Any(x => special_characters.Any(s => s == x)))
            //{
            //    errorMessage = "Invalid password: No special character";
            //    return false;
            //}

            errorMessage = string.Empty;
            return true;
        }
    }
}
