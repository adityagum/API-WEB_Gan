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
    private readonly IMapper<Room, RoomVM> _roomMapper;

    public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> roomMapper)
    {
        _roomRepository = roomRepository;
        _roomMapper = roomMapper;
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
}
