using Hotelss.Domain.Constants;
using Xunit;

namespace Hotelss.Application.Users.Tests;

public class CurrentUserTests
{
    [Fact()]
    public void IsInRole_WithMachingRole_ShouldReturnTrue()
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act

        currentUser.IsInRole(UserRoles.Admin);
        // assert

    }
}