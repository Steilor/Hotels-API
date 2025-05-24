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
    public async Task<ActionResult<IEnumerable<RoomDto?>>> GetAllForHotell([FromRoute] int hotelId)
    {
        var rooms = await mediator.Send(new GetRoomsForHotelQuery(hotelId));
        return Ok(rooms);
    }

    [HttpGet("{roomId}")]
    public async Task<ActionResult<RoomDto>> GetByIdForHotel(int hotelId,[FromRoute] int roomId)
    {
        var room = await mediator.Send(new GetRoomByIdForHotelQuery(hotelId, roomId));
        return Ok(room);
    }
}
