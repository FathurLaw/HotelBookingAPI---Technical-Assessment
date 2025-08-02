using HotelBookingAPI.Models;
using HotelBookingAPI.Repositories;

namespace HotelBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IRoomRepository _roomRepo;

        public BookingService(IBookingRepository bookingRepo, IRoomRepository roomRepo)
        {
            _bookingRepo = bookingRepo;
            _roomRepo = roomRepo;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepo.GetAllAsync();
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            var room = await _roomRepo.GetByIdAsync(booking.RoomId);
            if (room == null || !room.IsAvailable)
                throw new Exception("Room is not available.");

            // this to Prevent overlapping bookings
            bool isOverlapping = await _bookingRepo.HasOverlappingBookingAsync(
                booking.RoomId, booking.CheckInDate, booking.CheckOutDate);

            if (isOverlapping)
                throw new Exception("This room is already booked for the selected dates.");

            room.IsAvailable = false;
            await _roomRepo.UpdateAsync(room);
            return await _bookingRepo.AddAsync(booking);
        }

    }
}
