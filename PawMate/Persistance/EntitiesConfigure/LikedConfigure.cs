using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntitiesConfigure
{
    internal sealed class LikedConfigure : IEntityTypeConfiguration<Liked>
    {
        public void Configure(EntityTypeBuilder<Liked> builder)
        {
            builder.ToTable("Liked");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            builder.HasOne(l => l.User)
                .WithMany(u => u.Liked)
                .HasForeignKey(l => l.UserId);

            builder.HasOne(l => l.Invoice)
                .WithMany(i=> i.Likes)
                .HasForeignKey(l => l.InvoiceId);
        }
    }
}
