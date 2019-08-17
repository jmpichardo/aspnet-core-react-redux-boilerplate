using Renetdux.Infrastructure.Domain.Users;

namespace Renetdux.Infrastructure.Domain.Photos
{
    public class Photo
    {
        public int PhotoId { get; private set; }
        public string ImageUrl { get; private set; }

        public int UserId { get; private set; }
        public User User { get; private set; }

        public Photo(string imageUrl, int userId)
        {
            ImageUrl = imageUrl;
            UserId = userId;
        }
    }
}
