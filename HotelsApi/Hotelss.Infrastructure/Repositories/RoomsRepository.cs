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


    public async Task<Room> GetByIdAsync(int hotelId, int roomId)
    {
        var room = await dbContext.Rooms.Where(d => d.HotelId==hotelId).FirstOrDefaultAsync(d=> d.Id ==roomId);
        return room;
    }
}
