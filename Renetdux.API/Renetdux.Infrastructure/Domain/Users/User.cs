using Microsoft.Extensions.DependencyInjection;
using Renetdux.Infrastructure.Domain.Photos;
using Renetdux.Infrastructure.Services.Encryption;
using System.Collections.Generic;

namespace Renetdux.Infrastructure.Domain.Users
{
    public class User : IUser
    {
        public int UserId { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Password { get; private set; }

        public string RefreshToken { get; private set; }

        public IList<Photo> Photos { get; set; }


        private IEncryptionService _encryptionService;
        public IEncryptionService EncryptionService
        {
            get => _encryptionService = _encryptionService ?? IoCContainerServiceProvider.ServiceProvider.GetService<IEncryptionService>();
            set => _encryptionService = value;
        }

        public User(string email, string firstName, string lastName, string password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = EncryptionService.HashPassword(password);
        }
    }
}
