using Renetdux.Infrastructure.DataModel;
using Renetdux.Infrastructure.Domain.Users;

namespace Renetdux.Infrastructure.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly RenetduxDatabaseContext _context;

        public UserRepository(RenetduxDatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
