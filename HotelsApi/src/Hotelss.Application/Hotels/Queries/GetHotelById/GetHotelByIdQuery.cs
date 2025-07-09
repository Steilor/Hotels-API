using Hotelss.Application.Hotels.Dtos;
using MediatR;

namespace Hotelss.Application.Hotels.Queries.GetHotelById;

public class GetHotelByIdQuery (int id) : IRequest<HotelsDto>
{
    public int Id { get;} = id;
}
