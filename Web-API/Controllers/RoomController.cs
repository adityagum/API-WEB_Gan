using Microsoft.AspNetCore.Mvc;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomController;

    public RoomController(IRoomRepository roomRepository)
    {
        _roomController = roomRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var room = _roomController.GetAll();
        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _roomController.GetByGuid(guid);
        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpPost]
    public IActionResult Create(Room room)
    {
        var result = _roomController.Create(room);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }


    [HttpPut]
    public IActionResult Update(Room room)
    {
        var isUpdated = _roomController.Update(room);
        if (!isUpdated)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roomController.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
