using Hotelss.Domain.Entities;

namespace Hotelss.Domain.Repositories;

public interface IHotelsRepository
{
    Task<IEnumerable<Hotel>> GetAllAsync();
    Task<Hotel?> GetByIdAsync(int id);

    Task<int> Create(Hotel hotel);

    Task Delete(Hotel hotel);

    Task SaveChanges();

    Task<IEnumerable<Hotel>> GetAllMatchingAsync(string? searchPhrase);
}
