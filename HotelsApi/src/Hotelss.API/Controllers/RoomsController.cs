using Hotelss.Application.Rooms.Commands.CreateRoom;
using Hotelss.Application.Rooms.Commands.DeleteRooms;
using Hotelss.Application.Rooms.Dtos;
using Hotelss.Application.Rooms.Queries.GetAllRooms;
using Hotelss.Application.Rooms.Queries.GetRoomById;
using Hotelss.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotelss.API.Controllers;

[Route("api/hotel/{hotelId}/rooms")]
[ApiController]
[Authorize]
public class RoomsController(IMediator mediator) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromRoute] int hotelId, CreateRoomCommand command)
    {
        command.HotelId = hotelId;

        var roomId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdForHotel), new { hotelId , roomId }, null);
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.AtLeast20)]
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

    [HttpDelete()]
    public async Task<ActionResult> DeleteRoomsForHotel([FromRoute] int hotelId)
    {
       await mediator.Send(new DeleteRoomsForHotelCommand(hotelId));

        return NoContent();
    }
}
 