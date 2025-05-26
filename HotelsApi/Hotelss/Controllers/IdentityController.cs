using Microsoft.AspNetCore.Mvc;

namespace Hotelss.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController
{
    [HttpPatch("user")]
    public async Task<IActionResult> UpdateUserDetails()
    {

    }

}
