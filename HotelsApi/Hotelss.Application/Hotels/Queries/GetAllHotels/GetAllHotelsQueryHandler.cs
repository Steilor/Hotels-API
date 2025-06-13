using AutoMapper;
using Hotelss.Application.Common;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Queries.GetAllHotels;

public class GetAllHotelsQueryHandler (ILogger<GetAllHotelsQueryHandler> logger,
    IMapper mapper,
    IHotelsRepository hotelsRepository) : IRequestHandler<GetAllHotelsQuery, PagedResult<HotelsDto>>
{
    public async Task<IEnumerable<HotelsDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all the hotels");
        var hotels = await hotelsRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageNumber);

        var hotelsDtos = mapper.Map<IEnumerable<HotelsDto>>(hotels);

        var results = new PagedResult<HotelsDto>(hotelsDtos, x, request.PageSize, request.PageNumber);
        return hotelsDtos!;
    }
}
