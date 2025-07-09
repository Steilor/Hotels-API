using Hotelss.Application.Rooms.Dtos;
using MediatR;

namespace Hotelss.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdForHotelQuery(int hotelId, int roomId) : IRequest<RoomDto>
{
    public int HotelId { get;} = hotelId;
    public int RoomId { get; } = roomId;
}
