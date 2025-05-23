using AutoMapper;
using Hotelss.Application.Rooms.Dtos;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryHandler(ILogger<GetRoomByIdQueryHandler> logger,
    IHotelsRepository hotelsRepository,
    IRoomsRepository roomsRepository,
    IMapper mapper) : IRequestHandler<GetRoomByIdQuery, RoomDto>
{
    public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var hotel = await hotelsRepository.GetByIdAsync(request.HotelId);
        if (hotel == null) throw new NotFoundException(nameof(Room), request.RoomId.ToString());

        var room = await roomsRepository.GetByIdAsync(request.HotelId,request.RoomId);
        var roomDto = mapper.Map<RoomDto>(room);

        return roomDto;
    } 
}
