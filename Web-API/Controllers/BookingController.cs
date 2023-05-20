using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IGenericRepository<Booking> _bookingRepository;
    public BookingController(IGenericRepository<Booking> bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var bookings = _bookingRepository.GetAll();
        if (!bookings.Any())
        {
            return NotFound();
        }
        return Ok(bookings);
    }
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid id)
    {
        var booking = _bookingRepository.GetByGuid(id);
        if (booking is null)
        {
            return NotFound();
        }

        return Ok(booking);
    }

    [HttpPost]
    public IActionResult Create(Booking booking)
    {
        var result = _bookingRepository.Create(booking);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpPut]
    public IActionResult Update(Booking booking)
    {
        var IsUpdate = _bookingRepository.Update(booking);
        if (IsUpdate)
        {
            return BadRequest();
        }
        return Ok();
    }
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _bookingRepository.Delete(guid);
        if (isDeleted)
        {
            return BadRequest();
        }
        return Ok();
    }
}
