using AutoMapper;
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
    public void Handle_WithValidRequest_ShouldUpdateHotels()
    {
        // arrange


        
    }
}