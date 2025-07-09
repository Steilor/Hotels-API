using Hotelss.Domain.Entities;

namespace Hotelss.Domain.Repositories;

public interface IRoomsRepository
{
    Task<int> CreateRoom(Room entity);
    Task Delete(IEnumerable<Room> entities);
    
} 
