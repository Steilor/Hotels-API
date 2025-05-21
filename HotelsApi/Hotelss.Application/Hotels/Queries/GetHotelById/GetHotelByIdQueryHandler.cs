using AutoMapper;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Queries.GetHotelById;

public class GetHotelByIdQueryHandler(ILogger<GetHotelByIdQueryHandler> logger,
    IMapper mapper,
    IHotelsRepository hotelsRepository) : IRequestHandler<GetHotelByIdQuery, HotelsDto>
{
    public async Task<HotelsDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting hotel {HotelId}", request.Id);
        var hotel = await hotelsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Hotel with {request.Id} doesn't exist"); 

        var hotelDto = mapper.Map<HotelsDto>(hotel);
        //var hotelDto = HotelsDto.FromEntity(hotel);

        return hotelDto;
    }
}
