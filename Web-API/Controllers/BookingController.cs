using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.ViewModels.Bookings;
using Web_API.ViewModels.Employees;
using Web_API.ViewModels.Login;
using Web_API.Others;
using Microsoft.AspNetCore.Authorization;
using Web_API.Models;

namespace Web_API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BookingController : BaseController<Booking, BookingVM>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper<Booking, BookingVM> _mapper;
    public BookingController(IBookingRepository bookingRepository,
        IMapper<Booking, BookingVM> mapper) : base(bookingRepository, mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    // Kelompok 4
    [HttpGet("BookingDetail")]
    [Authorize(Roles = "Manager")]
    public IActionResult GetAllBookingDetail()
    {
        try
        {
            var bookingDetails = _bookingRepository.GetAllBookingDetail();

            return Ok(new ResponseVM<List<BookingDetailVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Display all the booking detail",
                Data = bookingDetails.ToList()
            });

        }
        catch
        {
            return NotFound(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Booking detail data was not found"
            });
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

                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid was not found"
                });
            }

            return Ok(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid found successfully, showing data: ",
                Data = booking
            });
        }
        catch
        {
            return Ok("error");
        }
    }
    // End Kelompok 4

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



