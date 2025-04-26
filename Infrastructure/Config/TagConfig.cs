using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag")
                .HasKey(t => new { t.TagName, t.PostId });

            // leng of name < 100
            builder.Property(t => t.TagName)
                .HasMaxLength(100);

            builder.HasOne(t => t.Post)
                .WithMany(p => p.Tags)
                .HasForeignKey(t => t.PostId);
        }
    }
}
