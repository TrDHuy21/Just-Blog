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
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment")
               .HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(cm => cm.Content)
                .IsRequired()
                .HasMaxLength(500);

            // navigation properties
            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);

            // create and update by
            builder.HasOne(u => u.CreatedByUser)
                .WithMany(user => user.CreatedComments)
                .HasForeignKey(u => u.CreatedBy);

            builder.HasOne(u => u.UpdatedByUser)
                .WithMany(user => user.UpdatedComments)
                .HasForeignKey(u => u.UpdatedBy);
        }
    }
}
