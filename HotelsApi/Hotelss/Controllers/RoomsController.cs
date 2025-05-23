using Hotelss.Application.Rooms.Commands.CreateRoom;
using Hotelss.Application.Rooms.Dtos;
using Hotelss.Application.Rooms.Queries.GetAllRooms;
using Hotelss.Application.Rooms.Queries.GetRoomById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotelss.API.Controllers;

[Route("api/hotel/{hotelId}/rooms")]
[ApiController]
public class RoomsController(IMediator mediator) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromRoute] int hotelId, CreateRoomCommand command)
    {
        command.HotelId = hotelId;
        await mediator.Send(command);
        return Created();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomDto?>>> GetAllRooms(int hotelId)
    {
        var rooms = await mediator.Send(new GetAllRoomsQuery(hotelId));
        return Ok(rooms);
    }

    [HttpGet("{roomId}")]
    public async Task<ActionResult<RoomDto>> GetRoomById(int hotelId, int roomId)
    {
        var room = await mediator.Send(new GetRoomByIdQuery(hotelId, roomId));
        return Ok(room);
    }
}
