using Hotelss.Application.Hotels.Dtos;

namespace Hotelss.Application.Hotels;

public interface IHotelsService
{
    Task<IEnumerable<HotelsDto>> GetAllHotels();
    Task<HotelsDto?> GetHotelsById(int id);
    Task<int> CreateHotel(CreateHotelDto hotel);
}