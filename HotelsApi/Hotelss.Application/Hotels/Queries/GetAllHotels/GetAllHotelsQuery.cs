using Hotelss.Application.Common;
using Hotelss.Application.Hotels.Dtos;
using MediatR;

namespace Hotelss.Application.Hotels.Queries.GetAllHotels;

public class GetAllHotelsQuery() : IRequest<PagedResult<HotelsDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
