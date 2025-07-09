using FluentAssertions;
using Hotelss.Domain.Constants;
using Xunit;

namespace Hotelss.Application.Users.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMachingRole_ShouldReturnTrue(string roleName)
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act

        var isInRole = currentUser.IsInRole(roleName);
        // assert

        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMachingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act

        var isInRole = currentUser.IsInRole(UserRoles.Owner);
        // assert

        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMachingRoleCase_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act

        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());
        // assert

        isInRole.Should().BeFalse();
    }
}