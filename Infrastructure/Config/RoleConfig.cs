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
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role")
                .HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(30);


            // navigation properties

            // create and update by
            //builder.HasOne(r => r.CreatedByUser)
            //    .WithMany()
            //    .HasForeignKey(r => r.CreatedBy);

            //builder.HasOne(r => r.UpdatedByUser)
            //    .WithMany()
            //    .HasForeignKey(r => r.UpdatedBy);
        }
    }
}
