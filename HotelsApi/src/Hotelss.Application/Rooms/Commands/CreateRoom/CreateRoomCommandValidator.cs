using FluentValidation;

namespace Hotelss.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(room => room.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a non-negative number. ");

        RuleFor(room => room.Beds)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Beds must be a non-negative number. ");
    }

}
