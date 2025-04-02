using CvGenerator.Domain.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CvGenerator.Infrastructure.Database.Configurations
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Url).IsRequired().HasMaxLength(200);
        }
    }
}
