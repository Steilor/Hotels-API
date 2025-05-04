﻿using System.ComponentModel.DataAnnotations;

namespace Hotelss.Application.Hotels.Dtos;

public class CreateHotelDto
{
    [StringLength(100, MinimumLength = 3)]
    public string Nombre { get; set; } = default!;
    public string Description { get; set; } = default!;

    [Required(ErrorMessage ="Insert a valid category")]
    public string Category { get; set; } = default!;
    public bool IsAvailable { get; set; }

    [EmailAddress(ErrorMessage ="Please provide a valid email address")]
    public string? ContactEmail { get; set; }

    [Phone(ErrorMessage ="Please provide a valid phone number")]
    public string? ContactNumber { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }

    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Please provide a valid postal code (XX-XXX).")]
    public string? PostalCode { get; set; }
}
