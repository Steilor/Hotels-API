namespace Hotelss.Domain.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool IsAvailable { get; set; }

        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }

        public Address? Address { get; set; }
        public List<Room> Rooms { get; set; } = new();

        public User Owner { get; set; } = default!;
        public string OwnerId { get; set; } = default!;
    }
}
