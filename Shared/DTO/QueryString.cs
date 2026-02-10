namespace Shared.DTO;

public abstract record class QueryString
{
    public int PageNumber { get; set; } = 1;

    private const int _maxPageSize = 50;
    private int _pageSize = 10;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            if (value > _maxPageSize)
                _pageSize = _maxPageSize;
            else if (value < 1)
                _pageSize = 1;
            else
                _pageSize = value;
        }
    }
}
