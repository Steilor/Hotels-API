using Hotelss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotelss.Infrastructure.Persistence
{
    internal class HotelsDbContext(DbContextOptions<HotelsDbContext> options) : DbContext(options)
    {
        internal DbSet<Hotel> Hotels {  get; set; } 
        internal DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>() 
                .OwnsOne(m => m.Address);

            modelBuilder.Entity<Room>()
                .Property(p => p.Description)
                .HasMaxLength(100);

            modelBuilder.Entity<Hotel>()
                .HasMany(r => r.Rooms)
                .WithOne()
                .HasForeignKey(d => d.HotelId);
        }
    }
} 
