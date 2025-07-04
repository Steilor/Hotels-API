﻿using FluentAssertions;
using Hotelss.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace Hotelss.Application.Users.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticadedUser_ShouldRerturnCurrentUser()
    {
        // arrange
        var dateOfBirth = new DateOnly(1990, 1, 1);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "test@test.com"),
            new(ClaimTypes.Role, UserRoles.Admin),
            new(ClaimTypes.Role, UserRoles.User),
            new("Nationality", "German"),
            new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);
        // act
        var currentUser = userContext.GetCurrentUser();


        // asset
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        currentUser.Nationality.Should().Be("German");
        currentUser.DateOfBirth.Should().Be(dateOfBirth);
    }

    [Fact]
    public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
    {
        // Arrange
        var httContextAccessorMock = new Mock<IHttpContextAccessor>();
        httContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(httContextAccessorMock.Object);

        // act

        Action action = () => userContext.GetCurrentUser();

        // assert

        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("User context is not present");
    }
}