using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Others;
using Web_API.ViewModels.Rooms;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Room, RoomVM> _roomMapper;

    public RoomController(IRoomRepository roomRepository, 
        IMapper<Room, RoomVM> roomMapper,
        IBookingRepository bookingRepository,
        IEmployeeRepository employeeRepository)
    {
        _roomRepository = roomRepository;
        _roomMapper = roomMapper;
        _bookingRepository = bookingRepository;
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var room = _roomRepository.GetAll();
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }

        var data = room.Select(_roomMapper.Map).ToList();

        return Ok(new ResponseVM<List<RoomVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Data Room",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _roomRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found Data Room"
            });
        }

        var data = _roomMapper.Map(room);

        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found By Guid",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(RoomVM roomVM)
    {
        var roomconverted = _roomMapper.Map(roomVM);
        var result = _roomRepository.Create(roomconverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create Room Failed"
            });
        }

        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create Room Success"
        });
    }


    [HttpPut]
    public IActionResult Update(RoomVM roomVM)
    {
        var roomconverted = _roomMapper.Map(roomVM);
        var isUpdated = _roomRepository.Update(roomconverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update Room Failed"
            });
        }
        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update Room Success"
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roomRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete Room Failed"
            });
        }

        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete Room Success"
        });
    }

    // Kelompok 1
    [HttpGet("CurrentlyUsedRooms")]
    public IActionResult GetCurrentlyUsedRooms()
    {
        var room = _roomRepository.GetCurrentlyUsedRooms();
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomUsedVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found Used Room"
            });
        }

        return Ok(new ResponseVM<IEnumerable<RoomUsedVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Used Room",
            Data = room
        });
    }

    [HttpGet("CurrentlyUsedRoomsByDate")]
    public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
    {
        var room = _roomRepository.GetByDate(dateTime);
        if (room is null)
        {
            return NotFound(new ResponseVM<MasterRoomVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found By Date"
            });
        }

        return Ok(new ResponseVM<IEnumerable<MasterRoomVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found By Date",
            Data = room
        });
    }

    // End Kelompok 1

    // Kelompok 4
    [HttpGet("AvailableRoom")]
    public IActionResult GetAvailableRoom()
    {
        try
        {
            var room = _roomRepository.GetAvailableRoom();
            if (room is null)
            {
                return NotFound(new ResponseVM<RoomBookedTodayVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "AvailableRoom Not Found"
                });
            }

            return Ok(new ResponseVM<IEnumerable<RoomBookedTodayVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "AvailableRoom Found",
                Data = room
            });
        }
        catch
        {
            return Ok(new ResponseVM<RoomBookedTodayVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Terjadi Error"
            });
        }
    }
    // End Kelompok 4
}
