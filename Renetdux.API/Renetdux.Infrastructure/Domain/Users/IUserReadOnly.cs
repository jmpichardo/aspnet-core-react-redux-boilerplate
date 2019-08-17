using Renetdux.Infrastructure.Domain.Photos;
using System.Collections.Generic;

namespace Renetdux.Infrastructure.Domain.Users
{
    public interface IUserReadOnly
    {
        int UserId { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        string Password { get; }

        IList<Photo> Photos { get; }
    }
}
