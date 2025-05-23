using Hotelss.Application.Rooms.Dtos;
using MediatR;

namespace Hotelss.Application.Rooms.Queries.GetAllRooms;

public class GetAllRoomsQuery : IRequest<IEnumerable<RoomDto>>
{

}
