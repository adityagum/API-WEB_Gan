using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Bookings;
using Web_API.ViewModels.Employees;
using Web_API.ViewModels.Login;
using Web_API.ViewModels.Response;

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

    // Kelompok 4
    [HttpGet("BookingDetail")]
    public IActionResult GetAllBookingDetail()
    {
        try
        {
            var bookingDetails = _bookingRepository.GetAllBookingDetail();

            return Ok(bookingDetails);

        }
        catch
        {
            return Ok("error");
        }
    }

    [HttpGet("BookingDetailByGuid")]
    public IActionResult GetDetailByGuid(Guid guid)
    {
        try
        {
            var booking = _bookingRepository.GetBookingDetailByGuid(guid);
            if (booking is null)
            {

                return NotFound();
            }

            return Ok(booking);
        }
        catch
        {
            return Ok("error");
        }
    }
    // End Kelompok 4

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

    // Kelompok 3
    [HttpGet("bookingduration")]
    public IActionResult GetDuration()
    {
        var bookingdurations = _bookingRepository.GetBookingDuration();
        if (!bookingdurations.Any())
        {
            return NotFound(new ResponseVM<string>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No one has made a booking yet, so the old booking data is empty"
            });
        }

        return Ok(
            new ResponseVM<IEnumerable<BookingDurationVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Booking duration data found",
                Data = bookingdurations
            });
    }
    // End Kelompok 3
}



