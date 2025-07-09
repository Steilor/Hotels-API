using Hotelss.Domain.Constants;
using Hotelss.Domain.Entities;

namespace Hotelss.Domain.Interfaces
{
    public interface IHotelAuthorizationService
    {
        bool Authorize(Hotel hotel, ResourceOperation resourceOperation);
    }
}