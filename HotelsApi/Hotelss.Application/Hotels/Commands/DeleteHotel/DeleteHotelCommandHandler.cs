using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Interfaces;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommandHandler(ILogger<DeleteHotelCommandHandler> logger,
    IHotelsRepository hotelsRepository,
    IHotelAuthorizationService hotelAuthorizationService) : IRequestHandler<DeleteHotelCommand>
{
    public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Hotel with id : {HotelId}", request.Id);
        var hotel = await hotelsRepository.GetByIdAsync(request.Id);

        if ( hotel == null )
            throw new NotFoundException(nameof(Hotel), request.Id.ToString());

        if (!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Delete))
            throw new ForbidException();

        await hotelsRepository.Delete(hotel);
    }
}
