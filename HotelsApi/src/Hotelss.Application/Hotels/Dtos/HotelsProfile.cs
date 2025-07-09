using AutoMapper;
using Hotelss.Application.Hotels.Commands.CreateHotel;
using Hotelss.Application.Hotels.Commands.UpdateHotel;
using Hotelss.Domain.Entities;

namespace Hotelss.Application.Hotels.Dtos;

public class HotelsProfile : Profile
{
    public HotelsProfile()
    {
        CreateMap<CreateHotelCommand, Hotel>()
            .ForMember(d => d.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode,
                }));

        CreateMap<UpdateHotelCommand, Hotel>();



        CreateMap<Hotel, HotelsDto>()
            .ForMember(d => d.City, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(d => d.Street, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(d => d.PostalCode, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(d => d.Rooms, opt => opt.MapFrom(src => src.Rooms));
    }
}
