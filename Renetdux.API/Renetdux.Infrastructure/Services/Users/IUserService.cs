namespace Renetdux.Infrastructure.Services.Users
{
    public interface IUserService
    {
        IResultModel<bool> ValidateUser(string email, string firstName, string lastName, string password);

        bool IsValidPassword(string password, out string errorMessage);
    }
}
