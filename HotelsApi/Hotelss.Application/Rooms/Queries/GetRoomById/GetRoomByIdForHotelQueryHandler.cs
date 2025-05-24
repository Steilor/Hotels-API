using AutoMapper;
using Hotelss.Application.Rooms.Dtos;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdForHotelQueryHandler(ILogger<GetRoomByIdForHotelQueryHandler> logger,
    IHotelsRepository hotelsRepository,
    IMapper mapper) : IRequestHandler<GetRoomByIdForHotelQuery, RoomDto>
{
    public async Task<RoomDto> Handle(GetRoomByIdForHotelQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving room: {RoomId}, for hotel with id: {HotelId}",request.RoomId, request.HotelId);

        var hotel = await hotelsRepository.GetByIdAsync(request.HotelId);
        if (hotel == null) throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        var room = hotel.Rooms.FirstOrDefault(d => d.Id == request.RoomId);
        if (room == null) throw new NotFoundException(nameof(Room), request.RoomId.ToString());

        var roomDto = mapper.Map<RoomDto>(room);

        return roomDto;
    } 
}
