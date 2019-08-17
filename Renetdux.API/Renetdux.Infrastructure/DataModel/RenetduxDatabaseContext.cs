using Microsoft.EntityFrameworkCore;
using Renetdux.Infrastructure.DataModel.Configurations;
using Renetdux.Infrastructure.Domain.Photos;
using Renetdux.Infrastructure.Domain.Users;

namespace Renetdux.Infrastructure.DataModel
{
    public class RenetduxDatabaseContext : DbContext
    {
        public RenetduxDatabaseContext(DbContextOptions<RenetduxDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoConfiguration());
        }
    }
}
