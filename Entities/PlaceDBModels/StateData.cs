namespace Entities.PlaceDBModels;

public class StateData
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public Dictionary<string, string> Cities { get; set; } = new();
}

