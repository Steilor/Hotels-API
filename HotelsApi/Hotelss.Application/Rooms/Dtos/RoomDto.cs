using Hotelss.Domain.Entities;

namespace Hotelss.Application.Rooms.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public int? Beds { get; set; }

        public static RoomDto FromEntity(Room room)
        {
            if (room == null) return null;
            return new RoomDto()
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                Price = room.Price,
                HotelId = room.HotelId,
                Beds = room.Beds,
            };
        }
    }

   
}
