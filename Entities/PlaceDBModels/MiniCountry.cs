using System.Text.Json.Serialization;

namespace Entities.PlaceDBModels;
public class MiniCountry
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("phone_code")]
    public string PhoneCode { get; set; }
}
