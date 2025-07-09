using AutoMapper;
using Hotelss.Application.Rooms.Commands.CreateRoom;
using Hotelss.Domain.Entities;

namespace Hotelss.Application.Rooms.Dtos;

public class RoomsProfile : Profile
{
    public RoomsProfile()
    {
        CreateMap<CreateRoomCommand, Room>();
        CreateMap<Room, RoomDto>();
    }
}
