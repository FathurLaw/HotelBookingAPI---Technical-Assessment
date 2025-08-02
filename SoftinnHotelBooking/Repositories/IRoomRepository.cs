using HotelBookingAPI.Models;

namespace HotelBookingAPI.Repositories
{
    public interface IRoomRepository
    {
        Task<Room?> GetByIdAsync(int id);
        Task UpdateAsync(Room room);
    }
}
