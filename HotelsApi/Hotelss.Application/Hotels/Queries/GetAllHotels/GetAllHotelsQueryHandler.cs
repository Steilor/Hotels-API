using AutoMapper;
using Hotelss.Application.Common;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Queries.GetAllHotels;

public class GetAllHotelsQueryHandler (ILogger<GetAllHotelsQueryHandler> logger,
    IMapper mapper,
    IHotelsRepository hotelsRepository) : IRequestHandler<GetAllHotelsQuery, PagedResult<HotelsDto>>
{
    public async Task<PagedResult<HotelsDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all the hotels");
        var (hotels, totalCount) = await hotelsRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageNumber);

        var hotelsDtos = mapper.Map<IEnumerable<HotelsDto>>(hotels);

        var result = new PagedResult<HotelsDto>(hotelsDtos, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
