using System.Text.Json.Serialization;

namespace Entities.PlaceDBModels;

public class State
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public int CountryId { get; set; }

    // Navigational Properties
    [JsonIgnore]
    public Country? Country { get; set; }
    public ICollection<City>? Cities { get; set; }
}
