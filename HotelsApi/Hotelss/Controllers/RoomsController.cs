using Hotelss.Application.Rooms.Commands.CreateRoom;
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
}
