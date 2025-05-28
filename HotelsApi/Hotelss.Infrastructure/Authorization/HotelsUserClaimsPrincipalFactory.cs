using Hotelss.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hotelss.Infrastructure.Authorization;

public class HotelsUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
{

}
