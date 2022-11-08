using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.EntitiesConfigure;

namespace Persistence
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Liked> Likeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfigure());
            modelBuilder.ApplyConfiguration(new InvoiceConfigure());
            modelBuilder.ApplyConfiguration(new LikedConfigure());
        }
    }
}
