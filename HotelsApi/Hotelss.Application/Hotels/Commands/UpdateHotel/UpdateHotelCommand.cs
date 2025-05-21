using MediatR;

namespace Hotelss.Application.Hotels.Commands.UpdateHotel;

public class UpdateHotelCommand() : IRequest
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsAvailable { get; set; }
}
