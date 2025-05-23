using AutoMapper;
using Hotelss.Application.Rooms.Dtos;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Rooms.Queries.GetAllRooms;

public class GetAllRoomsQueryHandler(ILogger<GetAllRoomsQueryHandler> logger,
    IRoomsRepository roomsRepository,
    IHotelsRepository hotelsRepository,
    IMapper mapper) : IRequestHandler<GetAllRoomsQuery, IEnumerable<RoomDto>>
{
    public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
       var hotel = await hotelsRepository.GetByIdAsync(request.HotelId);
       
        if (hotel == null) throw new NotFoundException(nameof(Room), request.HotelId.ToString());

        var rooms = await roomsRepository.GetAllAsync(request.HotelId);

        var roomsDto = mapper.Map<IEnumerable<RoomDto>>(rooms);

        return roomsDto;

    }
}
