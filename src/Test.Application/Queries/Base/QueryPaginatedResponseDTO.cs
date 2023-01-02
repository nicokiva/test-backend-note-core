using Test.Application.Queries.Base;

namespace Test.Application.Queries.Base;

public class QueryPaginatedResponseDTO<T>: IQueryResponse where T: IQueryDataResponse
{
    private readonly T _list;
    
    private QueryPaginatedResponseDTO()
    {
        
    }

    public QueryPaginatedResponseDTO(T data,  long totalItems, long pageSize, long pageNumber)
    {
        if (pageSize == 0)
            throw new ArgumentException("PageSize must be greater than zero");
        Code = 200;
        Status = "OK";
        _list = data;
        TotalItems = totalItems;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
    
    public static QueryPaginatedResponseDTO<T> AsNotFound() 
    {
        return new QueryPaginatedResponseDTO<T>()
        {
            Status = "NOTFOUND",
            Code = 404,
        };
    }
    
    public static QueryPaginatedResponseDTO<T> Error() 
    {
        return new QueryPaginatedResponseDTO<T>()
        {
            Status = "ERROR",
            Code = 400,
        };
    }
    
    public string Status { get; set;  }
    public int Code { get; set;  }

    public object? Data
    {
        get
        {
            if (Code == 200)
                return new PaginatedDataResponse<T>
                {
                    TotalItems = TotalItems,
                    PageSize = PageSize,
                    PageNumber = PageNumber,
                    TotalPages = (int)Math.Ceiling((decimal)TotalItems / (decimal)(PageSize)),
                    Items = _list
                };
            
            return new PaginatedDataResponse<T>
            {
                TotalItems = 0,
                PageSize = 0,
                PageNumber = 0,
                TotalPages = 0,
                Items = default(T)
            };
        }
    }
    private long TotalItems { get; set; }
    private long PageSize { get; set; }
    private long PageNumber { get; set; }

    
}