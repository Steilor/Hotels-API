using Hotelss.Application.Rooms.Dtos;
using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;

namespace Hotelss.Application.Hotels.Dtos
{
    public class HotelsDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool IsAvailable { get; set; }
        //public string? ContactEmail { get; set; }
        //public string? ContactNumber { get; set; }

        //public Address? Address { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public List<RoomDto> Rooms { get; set; } = [];

        public static HotelsDto? FromEntity(Hotel? hotel)
        {
            if (hotel == null) return null;
            return new HotelsDto()
            {

                Id = hotel.Id,
                Nombre = hotel.Nombre,
                Description = hotel.Description,
                Category = hotel.Category,
                IsAvailable = hotel.IsAvailable,
                City = hotel.Address?.City,
                Street = hotel.Address?.Street,
                PostalCode = hotel.Address?.PostalCode,
                Rooms = hotel.Rooms.Select(RoomDto.FromEntity).ToList(),


            };
        }
    }
}
