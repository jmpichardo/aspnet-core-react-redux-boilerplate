using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Renetdux.Infrastructure.Domain.Users;

namespace Renetdux.Infrastructure.DataModel.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(d => d.UserId);
            builder.Property(d => d.Email).HasMaxLength(150).IsRequired();

            builder.Property(d => d.Password).HasMaxLength(512).IsRequired();

            builder.Ignore(d => d.EncryptionService);
        }
    }
}
