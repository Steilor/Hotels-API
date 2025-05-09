using AutoMapper;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels
{
    internal class HotelsService(IHotelsRepository hotelsRepository,
        ILogger<HotelsService> logger, 
        IMapper mapper) : IHotelsService
    {
        public async Task<IEnumerable<HotelsDto>> GetAllHotels()
        {
            logger.LogInformation("Getting all the hotels");
            var hotels = await hotelsRepository.GetAllAsync();

            var hotelsDtos = mapper.Map<IEnumerable<HotelsDto>>(hotels);

            //var hotelsDtos = hotels.Select(HotelsDto.FromEntity);
              
            return hotelsDtos!;
        }

        public async Task<HotelsDto?> GetHotelsById(int id)
        {
            logger.LogInformation($"Getting hotel {id}");
            var hotel = await hotelsRepository.GetByIdAsync(id);

            var hotelDto = mapper.Map<HotelsDto?>(hotel);
            //var hotelDto = HotelsDto.FromEntity(hotel);

            return hotelDto;

        }

    }
}
