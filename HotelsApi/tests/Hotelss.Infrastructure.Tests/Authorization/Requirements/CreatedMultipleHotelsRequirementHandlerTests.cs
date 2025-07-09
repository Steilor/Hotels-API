using FluentAssertions;
using Hotelss.Application.Users;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Xunit;

namespace Hotelss.Infrastructure.Tests.Authorization.Requirements
{
    public class CreatedMultipleHotelsRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotCreatedMultipleHotels_ShouldFail()
        {
            // arrange

            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

            var hotels = new List<Hotel>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                },
            };

            var hotelsRepositoryMock = new Mock<IHotelsRepository>();
            hotelsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(hotels);

            var requirement = new CreatedMultipleHotelsRequirement(2);
            var handler = new CreatedMultipleHotelsRequirementHandler(hotelsRepositoryMock.Object,
                userContextMock.Object);
            var context = new AuthorizationHandlerContext([requirement], null, null);

            // act

            await handler.HandleAsync(context);

            // assert

            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();

        }

        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleHotels_ShouldSucceed()
        {
            // arrange

            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

            var hotels = new List<Hotel>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                },
            };

            var hotelsRepositoryMock = new Mock<IHotelsRepository>();
            hotelsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(hotels);

            var requirement = new CreatedMultipleHotelsRequirement(2);
            var handler = new CreatedMultipleHotelsRequirementHandler(hotelsRepositoryMock.Object,
                userContextMock.Object);
            var context = new AuthorizationHandlerContext([requirement], null, null);

            // act

            await handler.HandleAsync(context);

            // assert

            context.HasSucceeded.Should().BeTrue();

        }
    }
}