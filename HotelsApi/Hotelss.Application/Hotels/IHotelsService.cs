using Hotelss.Application.Hotels.Dtos;
using Hotelss.Domain.Entities;

namespace Hotelss.Application.Hotels
{
    public interface IHotelsService
    {
        Task<IEnumerable<HotelsDto>> GetAllHotels();
        Task<HotelsDto?> GetHotelsById(int id);
        Task<Hotel?> CreateHotel(Hotel hotel);
    }
}