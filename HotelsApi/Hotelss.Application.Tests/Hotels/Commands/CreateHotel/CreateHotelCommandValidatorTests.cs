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

        result.ShouldHaveValidationErrorFor(c => c.Nombre);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

    [Theory()]
    [InlineData("Luxury")]
    [InlineData("Boutique")]
    [InlineData("Budget")]
    [InlineData("Resort")]
    [InlineData("Business")]
    [InlineData("All-Inclusive")]
    [InlineData("Hostel")]
    [InlineData("Bed & Breakfast")]
    [InlineData("Aparthotel")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    { 
        // arrange
        var validator = new CreateHotelCommandValidator();
        var command = new CreateHotelCommand() { Category = category };


        // act
        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    
    }

    [Theory()]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        // arrange
        var validator = new CreateHotelCommandValidator();
        var command = new CreateHotelCommand() { PostalCode = postalCode };


        // act
        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);

    }
}