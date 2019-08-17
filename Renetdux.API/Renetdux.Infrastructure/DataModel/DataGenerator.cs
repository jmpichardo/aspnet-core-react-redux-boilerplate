using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Renetdux.Infrastructure.Domain.Photos;
using Renetdux.Infrastructure.Domain.Users;
using System;
using System.Linq;

namespace Renetdux.Infrastructure.DataModel
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RenetduxDatabaseContext(
                serviceProvider.GetRequiredService<DbContextOptions<RenetduxDatabaseContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                context.Users.AddRange(
                    new User("test_1@test.com", "John", "Doe", "asd"),
                    new User("test_2@test.com", "Daniel", "Smith", "zxc")
                );

                context.Photos.AddRange(
                    new Photo("AA0011", 1),
                    new Photo("AA0022", 1),
                    new Photo("BB4444", 2)
                );

                context.SaveChanges();
            }
        }
    }
}
