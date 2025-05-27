using Hotelss.Application.Users.Commands.UpdateUserDetails;
using Hotelss.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotelss.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admind)]
    public async Task<IActionResult> AssignUserRole (AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

}
