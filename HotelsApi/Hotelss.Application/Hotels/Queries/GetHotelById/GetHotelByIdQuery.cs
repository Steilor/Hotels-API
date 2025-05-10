using Hotelss.Application.Hotels.Dtos;
using MediatR;

namespace Hotelss.Application.Hotels.Queries.GetHotelById;

public class GetHotelByIdQuery : IRequest<HotelsDto?>
{
}
