using Microsoft.AspNetCore.Authorization;

namespace Hotelss.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleHotelsRequirement(int minimumHotels) : IAuthorizationRequirement
    {
        public int MinimumHotels { get;} = minimumHotels;
    }
}
