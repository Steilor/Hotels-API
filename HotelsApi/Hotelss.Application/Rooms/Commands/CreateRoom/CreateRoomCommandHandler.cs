using AutoMapper;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler(ILogger<CreateRoomCommandHandler> logger,
    IHotelsRepository hotelsRepository,
    IRoomsRepository roomsRepository,
    IMapper mapper) : IRequestHandler<CreateRoomCommand>
{
    public async Task Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new room: {@RoomRequest}", request);
        var hotel = await hotelsRepository.GetByIdAsync(request.HotelId);
        if (hotel == null) throw new NotFoundException(nameof(Room), request.HotelId.ToString());

        var room = mapper.Map<Room>(request);
        await roomsRepository.CreateRoom(room);
    }
}
