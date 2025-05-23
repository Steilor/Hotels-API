using Hotelss.Application.Rooms.Dtos;
using MediatR;

namespace Hotelss.Application.Rooms.Queries.GetAllRooms;

public class GetAllRoomsQuery(int hotelId) : IRequest<IEnumerable<RoomDto>>
{
    public int HotelId { get; } = hotelId;
}
