using Renetdux.Infrastructure.Domain.Photos;
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

        public IList<Photo> Photos { get; set; }

        public User(string email, string firstName, string lastName, string password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;    // TODO: Encrypt password
        }
    }
}
