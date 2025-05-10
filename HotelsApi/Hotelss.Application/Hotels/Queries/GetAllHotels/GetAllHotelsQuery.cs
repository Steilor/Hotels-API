using Hotelss.Application.Hotels.Dtos;
using MediatR;

namespace Hotelss.Application.Hotels.Queries.GetAllHotels;

public class GetAllHotelsQuery : IRequest<IEnumerable<HotelsDto>>
{

}
