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
        
        var existEntity = await hotelsRepository.GetByIdAsync(request.Id);
        if (existEntity == null)
            return false;

        var entity = mapper.Map<Hotel>(request);


         await hotelsRepository.Update(entity);
        return true;
       
    }
}
