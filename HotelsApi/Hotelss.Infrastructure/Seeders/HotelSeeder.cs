using Hotelss.Domain.Entities;
using Hotelss.Infrastructure.Persistence;

namespace Hotelss.Infrastructure.Seeders
{
    internal class HotelSeeder(HotelsDbContext dbContext) : IHotelSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Hotels.Any())
                {
                    var hotels = GetHotels();
                    dbContext.Hotels.AddRange(hotels);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Hotel> GetHotels()
        {
            List<Hotel> hotels = [
                new()
                {
                    Nombre = "Bella vista",
                    Description = "Lujoso hotel ubicado en la playa.",
                    Category = "5 estrellas",
                    IsAvailable = true,
                    ContactEmail = "info@hotelparaiso.com",
                    ContactNumber = "+124141234",
                    Rooms =
                    [
                        new()
                        {

                            Name = "Suite Presencial",
                            Description = "Habitacion de lujo con vista al mal.",
                            Price = 500.00M,

                        },

                        new()
                        {
                            Name = "Habitacion Doble",
                            Description = "Habitación cómoda para familias.",
                            Price = 200.00M,

                        }

                    ],
                    Address = new()
                    {
                        City = "Babaro",
                        Street = "Av. VistaBella, No. 123",
                        PostalCode = "77500"
                    }
                },
                new Hotel()
                {
                    Nombre = "Hotel Montaña",
                    Description = "Un acogedor hotel en la sierra.",
                    Category = "4 estrellas",
                    IsAvailable = false,
                    ContactEmail = "reservas@hotelmontana.com",
                    ContactNumber = "+987654321",

                    Address = new Address()
                    {
                        City = "Bariloche",
                        Street = "Calle de la Montaña, No. 456",
                        PostalCode = "8400"
                    }

                }
            ];

            return hotels;
        }
    }
}
