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
    }
}