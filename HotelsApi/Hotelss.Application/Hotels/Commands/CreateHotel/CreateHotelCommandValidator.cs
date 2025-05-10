using FluentValidation;
using Hotelss.Application.Hotels.Dtos;

namespace Hotelss.Application.Hotels.Commands.CreateHotel;

public class CreateHotelCommandValidator: AbstractValidator<CreateHotelCommand>
{
    private readonly List<string> validCategories = ["Luxury", "Boutique", "Budget", "Resort", "Business", "All-Inclusive", 
        "Hostel", "Bed & Breakfast", "Aparthotel"];
    public CreateHotelCommandValidator()
    {
        RuleFor(dto => dto.Nombre)
              .Length(3, 100);

        RuleFor(dto => dto.Category)
            .Must(validCategories.Contains)
            .WithMessage("Invalid category. Please choose from the valid categories.");
         //.Custom((value, context) =>
         //{
         //    var isValidCategory = validCategories.Contains(value);

         //    if (!isValidCategory)
         //    {
         //        context.AddFailure("Category", "Invalid category. Please choose from the valid categories.");
         //    }
         //});

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email.");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (XX-XXX).");

    }
}