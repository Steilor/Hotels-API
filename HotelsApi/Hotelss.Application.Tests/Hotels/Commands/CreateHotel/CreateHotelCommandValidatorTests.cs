using FluentValidation.TestHelper;
using Xunit;

namespace Hotelss.Application.Hotels.Commands.CreateHotel.Tests;

public class CreateHotelCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange

        var command = new CreateHotelCommand()
        {
            Nombre = "Test",
            Category = "Boutique",
            ContactEmail = "test@test.com",
            PostalCode = "12-345",
        };

        var validator = new CreateHotelCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        // arrange

        var command = new CreateHotelCommand()
        {
            Nombre = "Te",
            Category = "None",
            ContactEmail = "test@",
            PostalCode = "12345",
        };

        var validator = new CreateHotelCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c=> c.Nombre);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}