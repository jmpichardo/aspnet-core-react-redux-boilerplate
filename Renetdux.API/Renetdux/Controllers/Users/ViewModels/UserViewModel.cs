using Renetdux.Infrastructure.Domain.Photos;
using Renetdux.Infrastructure.Domain.Users;
using System.Collections.Generic;

namespace Renetdux.Controllers.Users.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public List<PhotoViewModel> Photos { get; private set; }

        public UserViewModel(IUserReadOnly user)
        {
            UserId = user.UserId;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;

            if (user.Photos != null)
            {
                Photos = new List<PhotoViewModel>();
                foreach (var photo in user.Photos)
                {
                    Photos.Add(new PhotoViewModel(photo));
                }
            }
        }

        public class PhotoViewModel
        {
            public int PhotoId { get; private set; }
            public string ImageUrl { get; private set; }

            public PhotoViewModel(Photo photo)
            {
                PhotoId = photo.PhotoId;
                ImageUrl = photo.ImageUrl;
            }
        }
    }
}
