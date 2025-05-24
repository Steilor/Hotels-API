using MediatR;

namespace Hotelss.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? Beds { get; set; }

    public int HotelId { get; set; }

}
