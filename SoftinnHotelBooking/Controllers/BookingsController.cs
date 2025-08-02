using HotelBookingAPI.Dtos;
using HotelBookingAPI.Models;
using HotelBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET /api/bookings
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // POST /api/bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] BookingCreateDto bookingDto)
        {
            var booking = new Booking
            {
                GuestName = bookingDto.GuestName,
                RoomId = bookingDto.RoomId,
                CheckInDate = bookingDto.CheckInDate,
                CheckOutDate = bookingDto.CheckOutDate
            };

            var created = await _bookingService.CreateBookingAsync(booking);
            return CreatedAtAction(nameof(GetBookings), new { id = created.Id }, created);
        }
    }
}
