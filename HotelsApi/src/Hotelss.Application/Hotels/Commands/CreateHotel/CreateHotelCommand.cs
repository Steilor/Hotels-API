using MediatR;

namespace Hotelss.Application.Hotels.Commands.CreateHotel;

public class CreateHotelCommand : IRequest<int>
{
    public string Nombre { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool IsAvailable { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}
