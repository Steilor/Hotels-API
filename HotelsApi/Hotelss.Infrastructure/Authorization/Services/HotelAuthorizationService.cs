using Hotelss.Application.Users;
using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hotelss.Infrastructure.Authorization.Services;

public class HotelAuthorizationService(ILogger<HotelAuthorizationService> logger,
    IUserContext userContext) : IHotelAuthorizationService
{
    public bool Authorize(Hotel hotel, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for hotel {HotelName}",
            user.Email,
            resourceOperation,
            hotel.Nombre);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - seccessful authorization");
            return true;

        }

        if (resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update
            && user.Id == hotel.OwnerId)
        {
            logger.LogInformation("Hotel owner - seccessful authorization");
            return true;

        }
        return false;
    }
}
