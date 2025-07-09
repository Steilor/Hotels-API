using AutoMapper;
using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Interfaces;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Commands.UpdateHotel;

public class UpdateHotelCommandHandler(ILogger<UpdateHotelCommandHandler> logger,
    IHotelsRepository hotelsRepository,
    IMapper mapper,
    IHotelAuthorizationService hotelAuthorizationService) : IRequestHandler<UpdateHotelCommand>
{
    public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Hotel with id : {HotelId} with {@UpdatedHotel}", request.Id, request);
        
        var hotel = await hotelsRepository.GetByIdAsync(request.Id);
        if (hotel is null)
            throw new NotFoundException(nameof(Hotel), request.Id.ToString());

        if (!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Update))
            throw new ForbidException();

        mapper.Map(request, hotel);

        //hotel.Nombre = request.Nombre;
        //hotel.Description = request.Description;
        //hotel.IsAvailable = request.IsAvailable;
       
        await hotelsRepository.SaveChanges();

       
    }
}
