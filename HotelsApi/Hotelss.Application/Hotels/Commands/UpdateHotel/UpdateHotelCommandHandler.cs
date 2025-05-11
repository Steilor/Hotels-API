using AutoMapper;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Commands.UpdateHotel;

public class UpdateHotelCommandHandler(ILogger<UpdateHotelCommandHandler> logger,
    IHotelsRepository hotelsRepository,
    IMapper mapper) : IRequestHandler<UpdateHotelCommand, bool>
{
    public async Task<bool> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating Hotel with id : {request.Id}");
        
        var hotel = await hotelsRepository.GetByIdAsync(request.Id);
        if (hotel is null)
            return false;

        mapper.Map(request, hotel);

        //hotel.Nombre = request.Nombre;
        //hotel.Description = request.Description;
        //hotel.IsAvailable = request.IsAvailable;
       
        await hotelsRepository.SaveChanges();

        return true;
       
    }
}
