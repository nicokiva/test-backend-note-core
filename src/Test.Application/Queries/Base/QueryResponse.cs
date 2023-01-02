namespace Test.Application.Queries.Base;

public interface IQueryResponse
{
    string Status { get; }
    int Code { get; }
    object? Data { get; }
}

public record QueryResponse<T>: IQueryResponse where T: IQueryDataResponse
{
    public static QueryResponse<T> Success(T data)
    {
        return new QueryResponse<T>()
        {
            Status = "OK",
            Code = 200,
            Result = data
        };
    }
    
    public static QueryResponse<T> NotFound()
    {
        return new QueryResponse<T>()
        {
            Status = "NOTFOUND",
            Code = 404,
        };
    }
    
    public static QueryResponse<T> Error()
    {
        return new QueryResponse<T>()
        {
            Status = "ERROR",
            Code = 400
        };
    }
    
    private QueryResponse() { }

    public string Status { get; private init; } = "OK";
    public int Code { get; private init; } = 200;
    public T? Result { get; private init; } 
    public object? Data => Result;
}