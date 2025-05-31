using Hotelss.Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Hotelss.Infrastructure.Authorization.Requirements;

internal class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        MinimumAgeRequirement requirement)
    {
        var currentUser = context.User;
        logger.LogInformation("")
    }
}
