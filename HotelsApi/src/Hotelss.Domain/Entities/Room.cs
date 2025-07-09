namespace Hotelss.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public int? Beds { get; set; }

    }
}
