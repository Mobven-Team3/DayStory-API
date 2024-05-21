namespace DayStory.Domain.Pagination;

public class PagedResponse<T>
{
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public List<T> List { get; set; }

    public PagedResponse(int pageSize, int currentPage, int totalRecords, int totalPages, List<T> list)
    {
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalRecords = totalRecords;
        TotalPages = totalPages;
        List = list;
    }
}
