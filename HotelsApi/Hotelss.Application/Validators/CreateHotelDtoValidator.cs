using FluentValidation;
using Hotelss.Application.Hotels.Dtos;

namespace Hotelss.Application.Validators;

public class CreateHotelDtoValidator: AbstractValidator<CreateHotelDto>
{
    public CreateHotelDtoValidator()
    {
        RuleFor(dto => dto.Nombre)
            .Length(3, 100);
    }
}