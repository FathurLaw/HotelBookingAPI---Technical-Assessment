using HotelBookingAPI.Models;
using HotelBookingAPI.Repositories;
using HotelBookingAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingAPI.Tests
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepo;
        private readonly Mock<IRoomRepository> _mockRoomRepo;
        private readonly BookingService _bookingService;

        public BookingServiceTests()
        {
            _mockBookingRepo = new Mock<IBookingRepository>();
            _mockRoomRepo = new Mock<IRoomRepository>();
            _bookingService = new BookingService(_mockBookingRepo.Object, _mockRoomRepo.Object);
        }

        [Fact]
        public async Task GetAllBookingsAsync_ReturnsListOfBookings()
        {
            var expectedBookings = new List<Booking>
            {
                new Booking { Id = 1, GuestName = "Test", RoomId = 1 }
            };

            _mockBookingRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedBookings);

            var result = await _bookingService.GetAllBookingsAsync();

            Assert.Single(result);
            Assert.Equal("Test", result.First().GuestName);
        }

        [Fact]
        public async Task CreateBookingAsync_ThrowsException_WhenRoomNotFound()
        {
            var booking = new Booking { RoomId = 99 };

            _mockRoomRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Room?)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _bookingService.CreateBookingAsync(booking));
            Assert.Equal("Room is not available.", exception.Message);
        }

        [Fact]
        public async Task CreateBookingAsync_ThrowsException_WhenRoomNotAvailable()
        {
            var room = new Room { Id = 1, IsAvailable = false };
            var booking = new Booking { RoomId = 1 };

            _mockRoomRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(room);

            var exception = await Assert.ThrowsAsync<Exception>(() => _bookingService.CreateBookingAsync(booking));
            Assert.Equal("Room is not available.", exception.Message);
        }

        [Fact]
        public async Task CreateBookingAsync_Succeeds_WhenRoomIsAvailable()
        {
            var room = new Room { Id = 1, IsAvailable = true };
            var booking = new Booking { RoomId = 1, GuestName = "Alice" };

            _mockRoomRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(room);
            _mockRoomRepo.Setup(r => r.UpdateAsync(It.IsAny<Room>())).Returns(Task.CompletedTask);
            _mockBookingRepo.Setup(r => r.AddAsync(It.IsAny<Booking>())).ReturnsAsync(booking);
            _mockBookingRepo.Setup(r => r.HasOverlappingBookingAsync(1, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(false);

            var result = await _bookingService.CreateBookingAsync(booking);

            Assert.Equal("Alice", result.GuestName);
            _mockRoomRepo.Verify(r => r.UpdateAsync(It.Is<Room>(rm => rm.IsAvailable == false)), Times.Once);
            _mockBookingRepo.Verify(r => r.AddAsync(It.IsAny<Booking>()), Times.Once);
        }

        [Fact]
        public async Task CreateBookingAsync_ShouldThrow_WhenBookingOverlaps()
        {
            var room = new Room { Id = 1, IsAvailable = true };
            var booking = new Booking
            {
                RoomId = 1,
                CheckInDate = new DateTime(2025, 8, 10),
                CheckOutDate = new DateTime(2025, 8, 15)
            };

            _mockRoomRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(room);
            _mockBookingRepo.Setup(r => r.HasOverlappingBookingAsync(1, booking.CheckInDate, booking.CheckOutDate)).ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<Exception>(() => _bookingService.CreateBookingAsync(booking));
            Assert.Equal("This room is already booked for the selected dates.", exception.Message);
        }
    }
}
