using Hotelss.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Hotelss.Application.Users.Commands;

public class UpdateUserDetailsCommandHanlder(ILogger<UpdateUserDetailsCommandHanlder> logger,
    IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

        var dbUser = userStore.FindByIdAsync(user.Id!, cancellationToken);
    }
}
