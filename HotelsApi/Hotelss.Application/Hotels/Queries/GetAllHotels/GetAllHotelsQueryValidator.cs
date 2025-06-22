using FluentValidation;
using Hotelss.Application.Hotels.Dtos;

namespace Hotelss.Application.Hotels.Queries.GetAllHotels;

public class GetAllHotelsQueryValidator : AbstractValidator<GetAllHotelsQuery>
{
    private int[] allowPageSizes = [5, 10, 15, 30];
    private string[] allowedSortByColumnNames = [nameof(HotelsDto.Nombre), 
         nameof(HotelsDto.Description),
         nameof(HotelsDto.Category)];
    public GetAllHotelsQueryValidator()
    {
        RuleFor(dto => dto.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(dto => dto.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

        RuleFor(dto => dto.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}
