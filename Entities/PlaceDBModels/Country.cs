using System.Text.Json.Serialization;

namespace Entities.PlaceDBModels;

public class Country
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    [JsonPropertyName("phone_code")]
    public required string PhoneCode { get; set; }
    public required string Currency { get; set; }
    [JsonPropertyName("timezone")]
    public required string TimeZone { get; set; }

    // Navigational Properties
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ICollection<State>? States {get; set;}
}
