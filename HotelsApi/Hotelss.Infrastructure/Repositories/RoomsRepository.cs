using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Persistence;

namespace Hotelss.Infrastructure.Repositories;

internal class RoomsRepository(HotelsDbContext dbContext) : IRoomsRepository
{
    public async Task<int> CreateRoom(Room entity)
    {
        dbContext.Rooms.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
