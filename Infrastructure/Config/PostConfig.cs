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
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post")
                .HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .UseIdentityColumn();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Content)
                .IsRequired();

            builder.Property(p => p.Summary)
                .IsRequired();

            // navigation properties

            // create and update by
            builder.HasOne(p => p.CreatedByUser)
               .WithMany(user => user.CreatedPosts)
               .HasForeignKey(p => p.CreatedBy);

            builder.HasOne(p => p.UpdatedByUser)
                .WithMany(user => user.UpdatedPosts)
                .HasForeignKey(p => p.UpdatedBy);

            // category
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Posts) 
                .HasForeignKey("CategoryId");
        }
    }
}
