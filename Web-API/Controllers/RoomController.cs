using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;
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
            return NotFound();
        }

        var data = room.Select(_roomMapper.Map).ToList();   

        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _roomRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound();
        }

        var data = _roomMapper.Map(room);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(RoomVM roomVM)
    {
        var roomconverted = _roomMapper.Map(roomVM);
        var result = _roomRepository.Create(roomconverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }


    [HttpPut]
    public IActionResult Update(RoomVM roomVM)
    {
        var roomconverted = _roomMapper.Map(roomVM);
        var isUpdated = _roomRepository.Update(roomconverted);
        if (!isUpdated)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roomRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }

    // Kelompok 1
    [HttpGet("CurrentlyUsedRooms")]
    public IActionResult GetCurrentlyUsedRooms()
    {
        var room = _roomRepository.GetCurrentlyUsedRooms();
        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet("CurrentlyUsedRoomsByDate")]
    public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
    {
        var room = _roomRepository.GetByDate(dateTime);
        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
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
                return NotFound();
            }

            return Ok(room);
        }
        catch
        {
            return Ok("Ada error");
        }
    }
    // End Kelompok 4
}
