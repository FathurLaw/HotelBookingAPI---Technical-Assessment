using HotelBookingAPI.Data;
using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.Include(b => b.Room).ToListAsync();
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        // ✅ New method implementation
        public async Task<bool> HasOverlappingBookingAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            return await _context.Bookings.AnyAsync(b =>
                b.RoomId == roomId &&
                (
                    (checkIn < b.CheckOutDate && checkOut > b.CheckInDate)
                )
            );
        }
    }
}
