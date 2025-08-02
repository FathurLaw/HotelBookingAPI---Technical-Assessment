using HotelBookingAPI.Models;

namespace HotelBookingAPI.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking> AddAsync(Booking booking);

        //  method to prevent overlapping bookings (Same date)
        Task<bool> HasOverlappingBookingAsync(int roomId, DateTime checkIn, DateTime checkOut);
    }
}
