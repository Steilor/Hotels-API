using AutoMapper;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Commands.CreateHotel;

public class CreateHotelCommandHandler (ILogger<CreateHotelCommandHandler> logger,
    IMapper mapper,
    IHotelsRepository hotelsRepository) : IRequestHandler<CreateHotelCommand, int>
{
    public async Task<int> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new Hotel {@Request}", request);

        var hotel = mapper.Map<Hotel>(request);

        int id = await hotelsRepository.Create(hotel); 


        return id;
    }
}
