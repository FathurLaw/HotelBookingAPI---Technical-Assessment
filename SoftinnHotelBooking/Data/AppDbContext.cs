using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "101", Type = "Single", IsAvailable = true },
                new Room { Id = 2, Name = "102", Type = "Double", IsAvailable = true },
                new Room { Id = 3, Name = "103", Type = "Suite", IsAvailable = true }
            );
        }
    }
}
