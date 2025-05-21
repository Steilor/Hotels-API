using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommandHandler(ILogger<DeleteHotelCommandHandler> logger,
    IHotelsRepository hotelsRepository) : IRequestHandler<DeleteHotelCommand, bool>
{
    public async Task<bool> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Hotel with id : {HotelId}", request.Id);
        var hotel = await hotelsRepository.GetByIdAsync(request.Id);

        if ( hotel == null )
            throw new NotFoundException($"Hotel with {request.Id} doesn't exist");

        await hotelsRepository.Delete(hotel);
        return true;
    }
}
