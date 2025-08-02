using HotelBookingAPI.Models;

namespace HotelBookingAPI.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> CreateBookingAsync(Booking booking);
    }
}
