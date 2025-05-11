using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommandHandler(ILogger<DeleteHotelCommandHandler> logger,
    IHotelsRepository hotelsRepository) : IRequestHandler<DeleteHotelCommand, int>
{
    public async Task<int> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting Hotel with id : {request.Id}");
        var hotel = await hotelsRepository.GetByIdAsync(request.Id);

        if ( hotel == null)
        {

        }
        int result = await hotelsRepository.DeleteHotelAsync(request.Id);

        return result;

    }
}
