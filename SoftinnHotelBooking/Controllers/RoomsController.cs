using HotelBookingAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RoomsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetRooms()
    {
        return Ok(_context.Rooms.ToList());
    }
}
