using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
using Hotelss.Domain.Interfaces;
using Hotelss.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Hotels.Commands.UploadHotelLogo;

internal class UploadHotelLogoCommandHandler(ILogger<UploadHotelLogoCommandHandler> logger,
    IHotelsRepository hotelsRepository,
    IHotelAuthorizationService hotelAuthorizationService
    ) : IRequestHandler<UploadHotelLogoCommand>
{
    public async Task Handle(UploadHotelLogoCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Uploading hotel logo for id: {HotelId}", request.HotelId);
        var hotel = await hotelsRepository.GetByIdAsync(request.HotelId);
        if (hotel is null)
            throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        if (!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Update))
            throw new ForbidException();

        blobStorageService.UploadToBlobAsync(request.FileName, request.File);
    }
}
