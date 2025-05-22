using MediatR;

namespace Hotelss.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; } 
    public decimal Price { get; set; }
    public int HotelId { get; set; }
    public int? Beds { get; set; }

}
