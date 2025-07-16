using Hotelss.Application.Rooms.Dtos;

namespace Hotelss.Application.Hotels.Dtos
{
    public class HotelsDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool IsAvailable { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? LogoSasUrl { get; set; }

        public List<RoomDto> Rooms { get; set; } = [];

    }
}
