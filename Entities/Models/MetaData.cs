namespace Entities.Models;

public class MetaData
{
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public bool HasPrevious => PageNumber > 1 && PageNumber < TotalPages + 2;
    public bool HasNext => PageNumber < TotalPages && PageNumber > 0;
}
