using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Renetdux.Infrastructure.Domain.Photos;

namespace Renetdux.Infrastructure.DataModel.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(d => d.PhotoId);
            builder.Property(d => d.ImageUrl).HasMaxLength(512).IsRequired();

            builder.HasOne(u => u.User).WithMany(d => d.Photos);
        }
    }
}
