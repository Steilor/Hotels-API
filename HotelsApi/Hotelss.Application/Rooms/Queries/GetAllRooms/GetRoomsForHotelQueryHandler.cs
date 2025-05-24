using AutoMapper;
using Hotelss.Application.Rooms.Dtos;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Rooms.Queries.GetAllRooms;

public class GetRoomsForHotelQueryHandler(ILogger<GetRoomsForHotelQueryHandler> logger,
    IHotelsRepository hotelsRepository,
    IMapper mapper) : IRequestHandler<GetRoomsForHotelQuery, IEnumerable<RoomDto>>
{
    public async Task<IEnumerable<RoomDto>> Handle(GetRoomsForHotelQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving rooms for hotel with id: {HotelId}", request.HotelId);
        var hotel = await hotelsRepository.GetByIdAsync(request.HotelId);
       
        if (hotel == null) throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var roomsDto = mapper.Map<IEnumerable<RoomDto>>(hotel.Rooms);

        return roomsDto;

    }
}
