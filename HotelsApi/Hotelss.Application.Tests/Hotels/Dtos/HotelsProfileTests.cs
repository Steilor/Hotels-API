using AutoMapper;
using FluentAssertions;
using Hotelss.Domain.Entities;
using Xunit;

namespace Hotelss.Application.Hotels.Dtos.Tests;

public class HotelsProfileTests
{
    [Fact()]
    public void CreateMap_ForHotelToHotelsDto_MapsCorrectly()
    { 
       // arrange

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<HotelsProfile>();
        });

        var mapper = configuration.CreateMapper();

        var hotel = new Hotel()
        {
            Id = 1,
            Nombre = "Test hotel",
            Description = "Test Description",
            Category = "Test Category",
            IsAvailable = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123456789",
            Address = new Address
            {
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12345"
            }
        };

        // act

        var hotelsDto = mapper.Map<HotelsDto>(hotel);


        // assert

        hotelsDto.Should().NotBeNull();
        hotelsDto.Id.Should().Be(hotel.Id);
        hotelsDto.Nombre.Should().Be(hotel.Nombre);
        hotelsDto.Description.Should().Be(hotel.Description);
        hotelsDto.Category.Should().Be(hotel.Category);
        hotelsDto.IsAvailable.Should().Be(hotel.IsAvailable);
        hotelsDto.City.Should().Be(hotel.Address.City);
        hotelsDto.Street.Should().Be(hotel.Address.Street);
        hotelsDto.PostalCode.Should().Be(hotel.Address.PostalCode);
    }
}