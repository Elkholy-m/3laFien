using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.DTO;

public record class PlaceQueryString : QueryString
{
    public decimal MinPrice { get; set; } = 0;
    public decimal MaxPrice { get; set; } = int.MaxValue;

    [BindNever]
    public bool ValidPriceRange => MinPrice < MaxPrice;

    public float MinRate { get; set; } = 0;

    [BindNever]
    public bool ValidMinRate => MinRate >= 0 && MinRate <= 5;

    public bool? OpenOnly { get; set; }
    public bool? DiscountOnly { get; set; }

    public double? UserLat { get; set; }
    public double? UserLon { get; set; }

    public int? CategoryId { get; set; } = null;
    public int? CountryId { get; set; } = null;
    public int? StateId { get; set; } = null;
    public int? CityId { get; set; } = null;

    public string? SearchTerm { get; set; } = null;
}
