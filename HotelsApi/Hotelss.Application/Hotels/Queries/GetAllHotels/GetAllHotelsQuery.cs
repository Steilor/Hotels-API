using Hotelss.Application.Hotels.Dtos;
using MediatR;

namespace Hotelss.Application.Hotels.Queries.GetAllHotels;

public class GetAllHotelsQuery() : IRequest<IEnumerable<HotelsDto>>
{
    public string? SearchPhrase { get; set; }
}
