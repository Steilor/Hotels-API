using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;
using System.ComponentModel;

namespace Hotelss.Domain.Repositories;

public interface IHotelsRepository
{
    Task<IEnumerable<Hotel>> GetAllAsync();
    Task<Hotel?> GetByIdAsync(int id);

    Task<int> Create(Hotel hotel);

    Task Delete(Hotel hotel);

    Task SaveChanges();

    Task<(IEnumerable<Hotel>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber,string? sortBy, SortDirection sortDirection);
}
