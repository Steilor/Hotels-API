using MediatR;

namespace Hotelss.Application.Rooms.Commands.DeleteRooms;

public class DeleteRoomsForHotelCommand(int hotelId) : IRequest
{
    public int HotelId { get;} = hotelId;
}
