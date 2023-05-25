using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Bookings;

namespace Web_API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper<Booking, BookingVM> _bkmapper;
    public BookingController(IBookingRepository bookingRepository, IMapper<Booking, BookingVM> bkmapper)
    {
        _bookingRepository = bookingRepository;
        _bkmapper = bkmapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var bookings = _bookingRepository.GetAll();
        if (!bookings.Any())
        {
            return NotFound();
        }

        var data = bookings.Select(_bkmapper.Map).ToList();

        return Ok(data);
    }
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid id)
    {
        var booking = _bookingRepository.GetByGuid(id);
        if (booking is null)
        {
            return NotFound();
        }

        var data = _bkmapper.Map(booking);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(BookingVM bookingVM)
    {
        var bkconverted = _bkmapper.Map(bookingVM);
        var result = _bookingRepository.Create(bkconverted);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
    [HttpPut]
    public IActionResult Update(BookingVM bookingVM)
    {
        var bkconverted = (_bkmapper.Map(bookingVM));
        var IsUpdate = _bookingRepository.Update(bkconverted);
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

    [HttpGet("bookingduration")]
    public IActionResult GetDuration()
    {
        var bookingLengths = _bookingRepository.GetBookingDuration();
        if (!bookingLengths.Any())
        {
            return NotFound();
        }

        return Ok(bookingLengths);
    }


    /*[HttpGet("length/{guid}")]
    public IActionResult GetBookingLength(Guid guid)
    {
        var booking = _bookingRepository.GetByGuid(guid);
        if (booking == null)
        {
            return NotFound();
        }

        var bookingLength = _bookingRepository.calculateBooking(DateTime startDate, DateTime endDate);

        var data = new
        {
            RoomName = booking.Room?.RoomName,
            BookingLength = bookingLength
        };

        return Ok(data);*/
}



