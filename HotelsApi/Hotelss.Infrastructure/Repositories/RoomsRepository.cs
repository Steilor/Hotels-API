using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hotelss.Infrastructure.Repositories;

internal class RoomsRepository(HotelsDbContext dbContext) : IRoomsRepository
{
    public async Task<int> CreateRoom(Room entity)
    {
        dbContext.Rooms.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IEnumerable<Room>> GetAllAsync(int hotelId)
    {
       var rooms = await dbContext.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();

        return rooms;
            
    }
}
