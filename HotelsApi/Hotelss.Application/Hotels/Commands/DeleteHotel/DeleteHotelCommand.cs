using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Hotelss.Application.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommand(int id) : IRequest<bool>
{
    public int Id { get;} = id; 
}
