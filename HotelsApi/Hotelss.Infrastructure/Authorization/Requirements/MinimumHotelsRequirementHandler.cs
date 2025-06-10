using Hotelss.Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Hotelss.Infrastructure.Authorization.Requirements
{
    internal class MinimumHotelsRequirementHandler(ILogger<MinimumHotelsRequirementHandler> logger,
        IUserContext userContext) : AuthorizationHandler<MinimumHotelsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MinimumHotelsRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("User: {Email} - Handling MinimumHotelsRequirement", 
                currentUser.Email);


        }
    }
}
    