using Hotelss.Application.Users;
using Hotelss.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Hotelss.Infrastructure.Authorization.Services;

public class HotelAuthorizationService(ILogger<HotelAuthorizationService> logger,
    IUserContext userContext)
{
    public bool Authorize(Hotel hotel, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for hotel {HotelName}",
            user.Email,
            resourceOperation,
            hotel.Nombre);
    }
}
