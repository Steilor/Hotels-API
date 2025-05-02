using Hotelss.Domain.Entities;

namespace Hotelss.Domain.Repositories
{
    public interface IHotelsRepository
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel?> GetByIdAsync(int id);

        Task<int> CreateHotelAsync(Hotel hotel);
    }
}
