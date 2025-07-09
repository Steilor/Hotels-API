using AutoMapper;
using FluentAssertions;
using Hotelss.Application.Users;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Hotelss.Application.Hotels.Commands.CreateHotel.Tests;

public class CreateHotelCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedHotelId()
    {
        // arrange
        var loggerMock = new Mock<ILogger<CreateHotelCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new CreateHotelCommand();
        var hotel = new Hotel();

        mapperMock.Setup(m => m.Map<Hotel>(command)).Returns(hotel);

        var hotelRepositoryMock = new Mock<IHotelsRepository>();
        hotelRepositoryMock
            .Setup(repo => repo.Create(It.IsAny<Hotel>()))
            .ReturnsAsync(1);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null); 
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);
 
        var commandHanlder = new CreateHotelCommandHandler(loggerMock.Object, 
            mapperMock.Object, 
            hotelRepositoryMock.Object,
            userContextMock.Object);

        // act
        var result = await commandHanlder.Handle(command, CancellationToken.None);

        // assert
        result.Should().Be(1);
        hotel.OwnerId.Should().Be("owner-id");
        hotelRepositoryMock .Verify(r => r.Create(hotel), Times.Once);
    }
}