using System.Text.Json.Serialization;

namespace Entities.PlaceDBModels;

public class LocationResponse
{
    [JsonPropertyName("country")]
    public MiniCountry Country { get; set; }

    // Key is state code (e.g., "AL"), Value is a list: [id, name, cities_dict]
    [JsonPropertyName("states")]
    public Dictionary<string, List<object>> States { get; set; }
}

