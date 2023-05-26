using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Bookings;
using Web_API.ViewModels.Employees;
using Web_API.ViewModels.Login;
using Web_API.Others;

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

    [HttpGet]
    public IActionResult GetAll()
    {
        var bookings = _bookingRepository.GetAll();
        if (!bookings.Any())
        {
            return NotFound(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Booking data was not found"
            });
        }

        var data = bookings.Select(_bkmapper.Map).ToList();

        return Ok(new ResponseVM<List<BookingVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Showing the data of booking: ",
            Data = data

        });
    }
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid id)
    {
        var booking = _bookingRepository.GetByGuid(id);
        if (booking is null)
        {
            return NotFound(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid not found"
            });
        }

        var data = _bkmapper.Map(booking);

        return Ok(new ResponseVM<BookingVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success to find the Guid, showing data:",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(BookingVM bookingVM)
    {
        var bkconverted = _bkmapper.Map(bookingVM);
        var result = _bookingRepository.Create(bkconverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed Create Booking"
            });
        }

        return Ok(new ResponseVM<BookingVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Create Booking"
        });
    }
    [HttpPut]
    public IActionResult Update(BookingVM bookingVM)
    {
        var bkconverted = (_bkmapper.Map(bookingVM));
        var IsUpdate = _bookingRepository.Update(bkconverted);
        if (IsUpdate)
        {
            return BadRequest(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Failed Update Booking"
            });
        }

        return Ok(new ResponseVM<BookingVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Update Booking"
        });
    }
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _bookingRepository.Delete(guid);
        if (isDeleted)
        {
            return BadRequest(new ResponseVM<Guid>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Failed Delete Booking"
            });
        }
        return Ok(new ResponseVM<Guid>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success delete booking by Guid"
        });
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



