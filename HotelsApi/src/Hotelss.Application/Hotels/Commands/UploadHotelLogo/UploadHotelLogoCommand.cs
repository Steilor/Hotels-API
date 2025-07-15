using MediatR;

namespace Hotelss.Application.Hotels.Commands.UploadHotelLogo;

public class UploadHotelLogoCommand : IRequest
{
    public int HotelId { get; set; }
    public string FileName { get; set; } = default!;
    public Stream File { get; set; } = default!;
}
