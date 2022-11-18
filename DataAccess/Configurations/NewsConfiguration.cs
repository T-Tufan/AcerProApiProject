using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    internal class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(50);
            builder.Property(x => x.Title).IsRequired();

            builder.Property(x => x.ImagePath).HasMaxLength(1000);

            builder.Property(x => x.CreateTime).IsRequired();

            builder.HasOne(a => a.AppUser).WithMany(n => n.News).HasForeignKey(n => n.ReleasedPerson);

        }
    }
}
