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
    public class PostRateConfig : IEntityTypeConfiguration<PostRate>
    {
        public void Configure(EntityTypeBuilder<PostRate> builder)
        {
            builder.ToTable("PostRate")
               .HasKey(pr => new {pr.PostId, pr.UserId});

            builder.Property(pr => pr.Point)
                .IsRequired()
                .HasPrecision(3, 1);

            builder.ToTable(t => t.HasCheckConstraint("CK_PostRate_Point", "Point >= 0 AND Point <= 5"));

            // navigation properties

            builder.HasOne(pr => pr.Post)
                .WithMany(p => p.PostRates)
                .HasForeignKey(pr => pr.PostId);

            builder.HasOne(pr => pr.User)
                 .WithMany(user => user.PostRates)
                 .HasForeignKey(pr => pr.UserId);
        }
    }
}
