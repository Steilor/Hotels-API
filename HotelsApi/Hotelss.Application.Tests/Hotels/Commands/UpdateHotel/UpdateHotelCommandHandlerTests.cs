using AutoMapper;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Interfaces;
using Hotelss.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Hotelss.Application.Hotels.Commands.UpdateHotel.Tests;

public class UpdateHotelCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateHotelCommandHandler>> _loggerMock;
    private readonly Mock<IHotelsRepository> _hotelsRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IHotelAuthorizationService> _hotelAuthorizationServiceMock;

    private readonly UpdateHotelCommandHandler _handler;

    public UpdateHotelCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateHotelCommandHandler>>();
        _hotelsRepositoryMock = new Mock<IHotelsRepository>();
        _mapperMock = new Mock<IMapper>();
        _hotelAuthorizationServiceMock = new Mock<IHotelAuthorizationService>();

        _handler = new UpdateHotelCommandHandler(
            _loggerMock.Object,
            _hotelsRepositoryMock.Object,
            _mapperMock.Object,
            _hotelAuthorizationServiceMock.Object);
    }

    [Fact()]
    public async void Handle_WithValidRequest_ShouldUpdateHotels()
    {
        // arrange
        var hotelId = 1;
        var command = new UpdateHotelCommand()
        {
            Id = hotelId,
            Nombre = "New Test",
            Description = "New Description",
            IsAvailable = true,
        };

        var hotel = new Hotel()
        {
            Id = hotelId,
            Nombre = "Test",
            Description = "Test",
        };

        _hotelsRepositoryMock.Setup(r => r.GetByIdAsync(hotelId))
            .ReturnsAsync(hotel);

        _hotelAuthorizationServiceMock.Setup(m => m.Authorize(hotel, Domain.Constants.ResourceOperation.Update))
            .Returns(true);


        // act
        await _handler.Handle(command, CancellationToken.None);

        // assert

        _hotelsRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, hotel), Times.Once);



    }
}