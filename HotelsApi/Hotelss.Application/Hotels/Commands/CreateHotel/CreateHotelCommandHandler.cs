using MediatR;

namespace Hotelss.Application.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, int>
    {
        public Task<int> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
 