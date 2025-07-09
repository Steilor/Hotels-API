using AutoMapper;
using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Interfaces;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler(ILogger<CreateRoomCommandHandler> logger,
    IHotelsRepository hotelsRepository,
    IRoomsRepository roomsRepository,
    IMapper mapper,
     IHotelAuthorizationService hotelAuthorizationService) : IRequestHandler<CreateRoomCommand, int>
{
    public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new room: {@RoomRequest}", request);
        var hotel = await hotelsRepository.GetByIdAsync(request.HotelId);
        if (hotel == null) throw new NotFoundException(nameof(Room), request.HotelId.ToString());

        if (!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Update))
            throw new ForbidException();

        var room = mapper.Map<Room>(request);
        return await roomsRepository.CreateRoom(room);
    }
}
