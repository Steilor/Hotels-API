using Hotelss.Application.Hotels.Commands.CreateHotel;
using Hotelss.Application.Hotels.Commands.DeleteHotel;
using Hotelss.Application.Hotels.Commands.UpdateHotel;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Application.Hotels.Queries.GetAllHotels;
using Hotelss.Application.Hotels.Queries.GetHotelById;
using Hotelss.Domain.Constants;
using Hotelss.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotelss.API.Controllers;

[ApiController]
[Route("api/hotels")]
[Authorize]
public class HotelsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [Authorize(Policy = PolicyNames.AtLeast2Hotels)]
    public async Task<ActionResult<IEnumerable<HotelsDto>>> GetHotels()
    {
      var hotels = await mediator.Send(new GetAllHotelsQuery());
      return Ok(hotels);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<ActionResult<HotelsDto?>> GetById(int id) 
    {
        var hotel = await mediator.Send(new GetHotelByIdQuery(id));
      
        return Ok(hotel);
    }
     
    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateHotel(CreateHotelCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new {id}, null);
    }


    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHotel(int id, UpdateHotelCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

         return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        await mediator.Send(new DeleteHotelCommand(id));

         return NoContent();
                
    }
    

}
