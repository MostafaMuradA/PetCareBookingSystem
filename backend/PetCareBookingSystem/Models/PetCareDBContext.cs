using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PetCareBookingSystem.Models
{
    public class PetCareDBContext : IdentityDbContext<User>
    {
        public PetCareDBContext(DbContextOptions<PetCareDBContext> options): base(options)
        {
        }
        public DbSet<PetService> PetServices { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PetService>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
                 .HasOne(b => b.PetService)
                 .WithMany(ps => ps.Bookings)
                 .HasForeignKey(b => b.PetServiceId);
        }
    }
}
