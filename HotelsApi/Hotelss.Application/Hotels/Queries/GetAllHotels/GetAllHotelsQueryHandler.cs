﻿using AutoMapper;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Queries.GetAllHotels;

public class GetAllHotelsQueryHandler (ILogger<GetAllHotelsQueryHandler> logger,
    IMapper mapper,
    IHotelsRepository hotelsRepository) : IRequestHandler<GetAllHotelsQuery, IEnumerable<HotelsDto>>
{
    public async Task<IEnumerable<HotelsDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all the hotels");
        var hotels = await hotelsRepository.GetAllAsync();

        var hotelsDtos = mapper.Map<IEnumerable<HotelsDto>>(hotels);

        //var hotelsDtos = hotels.Select(HotelsDto.FromEntity);

        return hotelsDtos!;
    }
}
