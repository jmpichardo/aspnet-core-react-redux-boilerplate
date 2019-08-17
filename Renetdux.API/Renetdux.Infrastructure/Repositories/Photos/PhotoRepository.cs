using Renetdux.Infrastructure.DataModel;
using Renetdux.Infrastructure.Domain.Photos;

namespace Renetdux.Infrastructure.Repositories.Photos
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        private readonly RenetduxDatabaseContext _context;

        public PhotoRepository(RenetduxDatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
