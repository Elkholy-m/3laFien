using System.Text.Json.Serialization;

namespace Entities.PlaceDBModels;

public class City
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CountryId { get; set; }
    public int StateId { get; set; }

    [JsonIgnore]
    public State? State { get; set; }
}
