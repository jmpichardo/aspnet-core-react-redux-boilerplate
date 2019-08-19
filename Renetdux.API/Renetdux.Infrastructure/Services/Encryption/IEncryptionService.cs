namespace Renetdux.Infrastructure.Services.Encryption
{
    public interface IEncryptionService
    {
        bool ValidatePassword(string password1, string password2);

        string HashPassword(string password);
    }
}
