namespace Entities.Models;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set;}

    public PagedList(List<T> items, int count, int pageSize, int pageNumber) 
    {
        MetaData = new MetaData {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = count,
        };
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(List<T> items,
            int count,
            int pageSize,
            int pageNumber) => new(items, count, pageSize, pageNumber);

}
