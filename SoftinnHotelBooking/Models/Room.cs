namespace HotelBookingAPI.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // e.g., "101", "102"
        public string Type { get; set; } = string.Empty; // e.g., "Single", "Double"
        public bool IsAvailable { get; set; }
    }
}