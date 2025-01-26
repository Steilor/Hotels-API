using Hotelss.Application.Hotels;
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
        public async Task<IActionResult> GetHotel(int id)
        {
           var hotel = await hotelsService.GetHotelsById(id);

            if (hotel is null)
                return NotFound();
            return Ok(hotel);
        }
    }
}
