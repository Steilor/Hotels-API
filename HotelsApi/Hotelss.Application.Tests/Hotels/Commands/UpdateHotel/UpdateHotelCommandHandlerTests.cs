using AutoMapper;
using FluentAssertions;
using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Exceptions;
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
    public async Task Handle_WithValidRequest_ShouldUpdateHotels()
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

    [Fact]
    public async Task Handle_WithNonExistingHotel_ShouldThrowNotFoundException()
    {
        // Arrange
        var hotelId = 2;
        var request = new UpdateHotelCommand
        {
            Id = hotelId
        };

        _hotelsRepositoryMock.Setup(r => r.GetByIdAsync(hotelId))
                .ReturnsAsync((Hotel?)null);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert
        await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Hotel with id: {hotelId} doesn't exist");
    }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // / Arrange
        var hotelId = 3;
        var request = new UpdateHotelCommand
        {
            Id = hotelId
        };

        var existingHotel = new Hotel
        {
            Id = hotelId
        };

        _hotelsRepositoryMock
            .Setup(r => r.GetByIdAsync(hotelId))
                .ReturnsAsync(existingHotel);

        _hotelAuthorizationServiceMock
            .Setup(a => a.Authorize(existingHotel, ResourceOperation.Update))
                .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbidException>();
    }
}