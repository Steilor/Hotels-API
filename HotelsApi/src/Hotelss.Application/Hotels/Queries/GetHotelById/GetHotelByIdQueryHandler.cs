﻿using AutoMapper;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Interfaces;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Queries.GetHotelById;

public class GetHotelByIdQueryHandler(ILogger<GetHotelByIdQueryHandler> logger,
    IMapper mapper,
    IHotelsRepository hotelsRepository,
    IBlobStorageService blobStorageService) : IRequestHandler<GetHotelByIdQuery, HotelsDto>
{
    public async Task<HotelsDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting hotel {HotelId}", request.Id);
        var hotel = await hotelsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Hotel), request.Id.ToString());

        var hotelDto = mapper.Map<HotelsDto>(hotel);
        //var hotelDto = HotelsDto.FromEntity(hotel);

        hotelDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(hotel.LogoUrl);

        return hotelDto;
    }
}
