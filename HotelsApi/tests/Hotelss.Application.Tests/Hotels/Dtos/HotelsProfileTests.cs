using AutoMapper;
using FluentAssertions;
using Hotelss.Application.Hotels.Commands.CreateHotel;
using Hotelss.Application.Hotels.Commands.UpdateHotel;
using Hotelss.Domain.Entities;
using Xunit;

namespace Hotelss.Application.Hotels.Dtos.Tests;

public class HotelsProfileTests
{
    private IMapper _mapper;

    public HotelsProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<HotelsProfile>();
        });
        
        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForHotelToHotelsDto_MapsCorrectly()
    { 
       // arrange

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

        var hotelsDto = _mapper.Map<HotelsDto>(hotel);


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

    [Fact()]
    public void CreateMap_ForCreateHotelCommandToHotel_MapsCorrectly()
    {
        // arrange

        var command = new CreateHotelCommand
        {
            Nombre = "Test Hotel",
            Description = "Test Description",
            Category = "test Ccategory",
            IsAvailable = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123456789",
            City = "Test City",
            Street = "Test Street",
            PostalCode = "12345",

        };

        // act

        var hotel = _mapper.Map<Hotel>(command);


        // assert

        hotel.Should().NotBeNull();
        hotel.Nombre.Should().Be(command.Nombre);
        hotel.Description.Should().Be(command.Description);
        hotel.Category.Should().Be(command.Category);
        hotel.IsAvailable.Should().Be(hotel.IsAvailable);
        hotel.ContactEmail.Should().Be(command.ContactEmail);
        hotel.ContactNumber.Should().Be(command.ContactNumber);
        hotel.Address.Should().NotBeNull();
        hotel.Address.City.Should().Be(command.City);
        hotel.Address.Street.Should().Be(command.Street);
        hotel.Address.PostalCode.Should().Be(command.PostalCode);
    }


    [Fact()]
    public void CreateMap_ForUpdateHotelCommandToHotel_MapsCorrectly()
    {
        // arrange

        var command = new UpdateHotelCommand
        {
            Id = 1,
            Nombre = "Updated Hotel",
            Description = "Updated Description",
            IsAvailable = false,
         
        };

        // act

        var hotel = _mapper.Map<Hotel>(command);


        // assert

        hotel.Should().NotBeNull();
        hotel.Id.Should().Be(command.Id);
        hotel.Nombre.Should().Be(command.Nombre);
        hotel.Description.Should().Be(command.Description);
        hotel.IsAvailable.Should().Be(hotel.IsAvailable);
 
    }
}