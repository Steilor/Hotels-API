using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hotelss.Infrastructure.Repositories
{
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
    }
}
