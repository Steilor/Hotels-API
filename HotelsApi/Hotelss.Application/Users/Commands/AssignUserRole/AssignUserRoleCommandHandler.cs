using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger) : IRequestHandler<AssignUserRoleCommand>
{
    public Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning user role: {@Request}", request);
    }
}
