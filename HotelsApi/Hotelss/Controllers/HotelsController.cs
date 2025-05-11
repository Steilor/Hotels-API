using Hotelss.Application.Hotels;
using Hotelss.Application.Hotels.Commands.CreateHotel;
using Hotelss.Application.Hotels.Commands.DeleteHotel;
using Hotelss.Application.Hotels.Commands.UpdateHotel;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Application.Hotels.Queries.GetAllHotels;
using Hotelss.Application.Hotels.Queries.GetHotelById;
using Hotelss.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotelss.API.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
          var hotels = await mediator.Send(new GetAllHotelsQuery());
          return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hotel = await mediator.Send(new GetHotelByIdQuery(id));
          
            if (hotel is null)
                return NotFound();
            return Ok(hotel);
        }
         
        [HttpPost]
        public async Task<IActionResult> CreateHotel(CreateHotelCommand command)
        {
            int id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new {id}, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var isDeleted = await mediator.Send(new DeleteHotelCommand(id));

            if (isDeleted)
                return NoContent();
            
            return NotFound();  
            
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateHotel(UpdateHotelCommand command)
        {
            var isUpdate = await mediator.Send(command);

            if (isUpdate)
                return NoContent();
            return NotFound();
        }
        

    }
}
