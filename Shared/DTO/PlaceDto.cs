namespace Shared.DTO
{
    public record PlaceDto
    {
        public Guid PlaceId { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public int? CountryId { get; init; }
        public int? CityId { get; init; }
        public int? StateId { get; init; }
        public int? CategoryId { get; init; }
        public string? MainImageUrl { get; set; }
        public bool IsOpened { get; set; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public decimal Price { get; init; }
        public decimal DiscountPercentage { get; init; }
        public decimal DiscountedPrice { get; init; }
        public int TotalReviews { get; set; }
        public float Rate { get; set; }
    }
}
