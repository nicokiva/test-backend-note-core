namespace Test.Application.Queries.Base;
public class PaginatedDataResponse<T>: IQueryDataResponse 
{
    public T Items {  get; set;  }
    public long TotalItems { get; set; }
    public long PageSize { get; set; }
    public long PageNumber { get; set; }
    public long TotalPages { get; set; }
        
    public long PageStart
    {
        get
        {
            return (PageNumber - 1) * PageSize + 1;
        }
    }
    public long PageEnd
    {
        get
        {
            return Math.Min(PageNumber * PageSize, TotalItems);
        }
    }
}