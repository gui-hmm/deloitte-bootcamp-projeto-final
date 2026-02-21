namespace ProjetoFinal.DTOs.Common;

public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; set; } = new List<T>();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

    public PagedResponse(IEnumerable<T> data, int page, int pageSize, int totalRecords)
    {
        Data = data;
        Page = page;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }
}