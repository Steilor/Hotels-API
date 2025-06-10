using Microsoft.AspNetCore.Authorization;

namespace Hotelss.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleHotelsRequirement(int minimumHotelsCreated) : IAuthorizationRequirement
    {
        public int MinimumHotelsCreated { get;} = minimumHotelsCreated;
    }
}
