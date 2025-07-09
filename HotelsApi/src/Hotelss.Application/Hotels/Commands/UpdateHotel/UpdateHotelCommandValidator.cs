using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotelss.Application.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(dto => dto.Nombre)
                  .Length(3, 100);


        }
    }
}
