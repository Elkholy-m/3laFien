using System.Net;
using System.Text.Json;

namespace Entities.PlaceDBModels;
public class GeoParser
{
    // Pass the httpClient in instead of creating a new one
    public static async Task<bool> ProcessGeoData(HttpClient httpClient,
            string url, Country country, JsonSerializerOptions options)
    {
        try {
            string jsonString = await httpClient.GetStringAsync(url);

            var data = JsonSerializer.Deserialize<LocationResponse>(jsonString, options);

            if (data?.States == null) return false;

            var states = new List<State>();
            foreach (var stateEntry in data.States)
            {
                var stateDetails = stateEntry.Value;
                int stateId = ((JsonElement)stateDetails[0]).GetInt32();
                var stateName = stateDetails[1].ToString();

                var cities = new List<City>();
                var citiesJson = (JsonElement)stateDetails[2];
                var citiesDict = JsonSerializer.Deserialize<Dictionary<string, string>>(citiesJson.GetRawText());

                if (citiesDict != null)
                {
                    // Optimization: Use KeyValuePair to avoid double lookup
                    foreach (var kvp in citiesDict) 
                    {
                        int cityId = int.Parse(kvp.Key);
                        cities.Add(new City {
                                Id = cityId,
                                Name = kvp.Value,
                                StateId = stateId,
                                CountryId = country.Id });
                    }
                }

                states.Add(new State {
                        Id = stateId,
                        Code = stateEntry.Key,
                        Name = stateName,
                        CountryId = country.Id,
                        Cities = cities });
            }
            country.States = states;
            return true;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }

    }
}
