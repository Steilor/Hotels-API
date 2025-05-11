using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hotelss.Infrastructure.Repositories;

internal class HotelsRepository(HotelsDbContext dbContext) : IHotelsRepository
{
    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
        var hotels = await dbContext.Hotels.ToListAsync();
        return hotels;
    }
    
    public async Task<Hotel?> GetByIdAsync(int id)
    {
        var hotel = await dbContext.Hotels
            .Include(r => r.Rooms)
            .FirstOrDefaultAsync(c=> c.Id == id);
        return hotel;
    }

    public async Task<int> Create(Hotel hotel)
    {
        dbContext.Hotels.Add(hotel);
        await dbContext.SaveChangesAsync();
        return hotel.Id;
    }

    public async Task Delete(Hotel hotel)
    {
         dbContext.Remove(hotel);
        await dbContext.SaveChangesAsync();

    }

    public async Task Update(Hotel hotel)
    {
       var hotelToUpadate = await dbContext.Hotels.FirstOrDefaultAsync(r => r.Id == hotel.Id);

        hotelToUpadate.Nombre = hotel.Nombre;
        hotelToUpadate.Description = hotel.Description;
        hotelToUpadate.IsAvailable = hotel.IsAvailable;
        await dbContext.SaveChangesAsync();
        
    }
}
