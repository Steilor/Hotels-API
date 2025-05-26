using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.User.Commands;

public class UpdateUserDetailsCommandHanlder(ILogger<UpdateUserDetailsCommandHanlder> logger,
    IUserContext userContext) : IRequestHandler<UpdateUserDetailsCommand>
{
    public Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Updating user: {UserId}, with {@Request}", request)
    }
}
