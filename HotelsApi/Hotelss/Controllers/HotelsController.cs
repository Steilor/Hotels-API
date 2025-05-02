using Hotelss.Application.Hotels;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hotelss.API.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelsController(IHotelsService hotelsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
          var hotels = await hotelsService.GetAllHotels();
          return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var hotel = await hotelsService.GetHotelsById(id);

            if (hotel is null)
                return NotFound();
            return Ok(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto createHotelDto)
        {
            int id = await hotelsService.CreateHotel(createHotelDto);

            return CreatedAtAction(nameof(GetById), new {id}, null);
        }

    }
}
