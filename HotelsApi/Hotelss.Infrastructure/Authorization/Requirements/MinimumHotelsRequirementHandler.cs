using Hotelss.Application.Users;
using Hotelss.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hotelss.Infrastructure.Authorization.Requirements
{
    internal class MinimumHotelsRequirementHandler(ILogger<MinimumHotelsRequirementHandler> logger,
        IUserContext userContext,
        HotelsDbContext dbContext) : AuthorizationHandler<MinimumHotelsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MinimumHotelsRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("User: {Email} - Handling MinimumHotelsRequirement", 
                currentUser.Email);

            var userHotels =  dbContext.Hotels.Where(d => d.OwnerId == currentUser.Id).ToList();

            if(userHotels == null)
            {
                logger.LogWarning("UserHotels null");
            }
            if (userHotels.Count() >= 2)
            {
                logger.LogInformation("Authorization succeded");
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
                

        }
    }
}
    