using AutoMapper;
using Hotelss.Application.Users;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Commands.CreateHotel;

public class CreateHotelCommandHandler (ILogger<CreateHotelCommandHandler> logger,
    IMapper mapper,
    IHotelsRepository hotelsRepository,
    IUserContext userContext) : IRequestHandler<CreateHotelCommand, int>
{
    public async Task<int> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("{UserEmail} [{UserId}] is Creating a new Hotel {@Request}",
            currentUser.Email,
            currentUser.Id,
            request);

        var hotel = mapper.Map<Hotel>(request);
        hotel.OwnerId = currentUser.Id;

        int id = await hotelsRepository.Create(hotel); 
        return id;
    }
}
