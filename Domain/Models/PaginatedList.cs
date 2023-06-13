namespace Domain.Models;

public class PaginatedList<T>
{
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage
    {
        get
        {
            return PageNumber > TotalPages;
        }
    }
    public bool HasPreviousPage
    {
        get
        {
            return PageNumber > 1;
        }
    }
    public IEnumerable<T> Items { get; set; }
    public PaginatedList(IEnumerable<T> items, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(items.Count() / (double)pageSize);
        Items = items.Skip((pageNumber-1)*pageSize).Take(pageSize);
    }
}
