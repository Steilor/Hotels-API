using FluentAssertions;
using Hotelss.API.Tests;
using Hotelss.Application.Hotels.Dtos;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Hotelss.API.Controllers.Tests;

public class HotelsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IHotelsRepository> _hotelsRepositoryMock = new();
    private readonly Mock<IHotelSeeder> _hotelsSeederMock = new();

    public HotelsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IHotelsRepository),
                                            _ => _hotelsRepositoryMock.Object));


                services.Replace(ServiceDescriptor.Scoped(typeof(IHotelSeeder),
                                            _ => _hotelsSeederMock.Object));
            });
        });
    }


    [Fact]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        // arrange

        var id = 1123;

        _hotelsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync((Hotel?)null);

        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync($"/api/hotels/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        // arrange

        var id = 99;

        var hotel = new Hotel()
        {
            Id = id,
            Nombre = "Test",
            Description = "Test description"
        };

        _hotelsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(hotel);

        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync($"/api/hotels/{id}");
        var hotelDto = await response.Content.ReadFromJsonAsync<HotelsDto>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        hotelDto.Should().NotBeNull();
        hotelDto.Nombre.Should().Be("Test");
        hotelDto.Description.Should().Be("Test description");
    }

    [Fact]
    public async Task GetAll_ForValidRequest_Returns200Ok()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var result = await client.GetAsync("/api/hotels?pageNumber=1&pageSize=10");

        // assert

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

    }

    [Fact]
    public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var result = await client.GetAsync("/api/hotels");

        // assert

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

    }
}