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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("User")
                .HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            // Self-referencing navigation properties with NO ACTION behavior
            builder.HasOne(u => u.CreatedByUser)
               .WithMany(user => user.CreatedUsers)
               .HasForeignKey(u => u.CreatedBy)
               .OnDelete(DeleteBehavior.ClientSetNull); // Changed from NoAction

            builder.HasOne(u => u.UpdatedByUser)
                .WithMany(user => user.UpdatedUsers)
                .HasForeignKey(u => u.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull); // Changed from NoAction

            // Role relationship
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
