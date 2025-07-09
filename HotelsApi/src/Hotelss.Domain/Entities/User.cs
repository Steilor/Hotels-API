using Microsoft.AspNetCore.Identity;

namespace Hotelss.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? DateOfBirth  { get; set; }
    public string? Nationality { get; set; }

    public List<Hotel> OwnedHotels { get; set; } = [];
}
