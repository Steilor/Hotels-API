﻿using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using Hotelss.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hotelss.Infrastructure.Seeders
{
    internal class HotelSeeder(HotelsDbContext dbContext) : IHotelSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Hotels.Any())
                {
                    var hotels = GetHotels();
                    dbContext.Hotels.AddRange(hotels);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }

           
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                    new (UserRoles.User)
                    {
                        NormalizedName = UserRoles.User.ToUpper()

                    },
                    new (UserRoles.Owner)
                    {
                        NormalizedName = UserRoles.Owner.ToUpper()
                    },
                    new (UserRoles.Admin)
                    {
                        NormalizedName = UserRoles.Admin.ToUpper() 
                    },
                ]; 
            return roles;
        }
        private IEnumerable<Hotel> GetHotels()
        {
            User owner = new User()
            {
                Email = "seed-user@test.com"
            };
            List<Hotel> hotels = [
                new()
                {
                    Owner = owner,
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
                    Owner = owner,
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
