namespace HotelBookingAPI.Dtos
{
    public class BookingCreateDto
    {
        public required string GuestName { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
