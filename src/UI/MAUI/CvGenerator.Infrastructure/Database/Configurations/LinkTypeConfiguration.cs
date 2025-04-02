using CvGenerator.Domain.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CvGenerator.Infrastructure.Database.Configurations
{
    public class LinkTypeConfiguration : IEntityTypeConfiguration<LinkType>
    {
        public void Configure(EntityTypeBuilder<LinkType> builder)
        {
            builder.HasKey(lt => lt.Id);
            builder.Property(lt => lt.Name).IsRequired().HasMaxLength(50);
            builder.Property(lt => lt.Icon).HasMaxLength(50);

            builder.HasData(
                new LinkType { Id = 1, Name = "LinkedIn", Icon = "fab fa-linkedin" },
                new LinkType { Id = 2, Name = "Github", Icon = "fab fa-github" }
            );
        }
    }
}
