using Hotelss.Application.Users;
using Hotelss.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Hotelss.Infrastructure.Authorization.Requirements
{
    internal class CreatedMultipleHotelsRequirementHandler(IHotelsRepository hotelsRepository,
        IUserContext userContext ) : AuthorizationHandler<CreatedMultipleHotelsRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CreatedMultipleHotelsRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            var hotels = await hotelsRepository.GetAllAsync();
                
            var userHotelsCreated = hotels.Count(r => r.OwnerId == currentUser!.Id);

            if(userHotelsCreated >= requirement.MinimumHotelsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

        }
    }
}
    