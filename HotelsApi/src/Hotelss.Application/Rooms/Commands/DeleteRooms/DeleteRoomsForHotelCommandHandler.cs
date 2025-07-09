using AutoMapper;
using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Interfaces;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Rooms.Commands.DeleteRooms;

public class DeleteRoomsForHotelCommandHandler(ILogger<DeleteRoomsForHotelCommandHandler> logger,
    IRoomsRepository roomsRepository,
    IHotelsRepository hotelsRepository,
    IHotelAuthorizationService hotelAuthorizationService) : IRequestHandler<DeleteRoomsForHotelCommand>
{
    public async Task Handle(DeleteRoomsForHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing all rooms from hotel: {HotelId}", request.HotelId);

        var hotel = await hotelsRepository.GetByIdAsync(request.HotelId);
        if (hotel == null) throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        if (!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Update))
            throw new ForbidException();

        await roomsRepository.Delete(hotel.Rooms);

    }
}
